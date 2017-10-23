using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Web.Mvc;
using Cstieg.Geography;
using Cstieg.ControllerHelper;
using Cstieg.ShoppingCart;
using Cstieg.ShoppingCart.PayPal;
using ShotgunAdapters.Models;
using System.Threading.Tasks;
using System.Linq;

namespace ShotgunAdapters.Controllers
{
    /// <summary>
    /// Controller to provide shopping cart view
    /// </summary>
    public class PayPalController : BaseController
    {
        private PayPalApiClient _paypalClient = new PayPalApiClient();

        public string GetOrderJson()
        {
            //string country = Request.Params.Get("country");
            ShoppingCart shoppingCart = ShoppingCart.GetFromSession(HttpContext);

            /*
            if (country == "US")
            {
                shoppingCart.Order.ShipToAddress.Country = country;
                shoppingCart.RemoveAllShippingCharges();
            }
            */
            shoppingCart.SaveToSession(HttpContext);
            return _paypalClient.CreateOrder(shoppingCart);
        }
    

        /// <summary>
        /// Verifies and saves the shopping cart
        /// </summary>
        /// <returns>Json success code</returns>
        [HttpPost]
        public async Task<JsonResult> VerifyAndSave()
        {
            string paymentDetailsJson = Request.Params.Get("paymentDetails");
            PaymentDetails paymentDetails = JsonConvert.DeserializeObject<PaymentDetails>(paymentDetailsJson);
            ShoppingCart shoppingCart = ShoppingCart.GetFromSession(HttpContext);

            // get address and add to shopping cart
            AddressBase shippingAddress = paymentDetails.Payer.PayerInfo.ShippingAddress;
            shippingAddress.CopyTo(shoppingCart.Order.ShipToAddress);

            paymentDetails.VerifyShoppingCart(shoppingCart);
            CustomVerification(shoppingCart, paymentDetails);

            await SaveShoppingCartToDbAsync(shoppingCart, paymentDetails.Payer.PayerInfo);

            // clear shopping cart
            shoppingCart = new ShoppingCart();
            shoppingCart.SaveToSession(HttpContext);

            // return success
            return this.JOk();
        }

        /// <summary>
        /// Saves the shopping cart to the database.  Must implement by copying this code to main project, and overriding base
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <param name="payerInfo"></param>
        protected virtual async Task SaveShoppingCartToDbAsync(ShoppingCart shoppingCart, PayerInfo payerInfo)
        {
            var customersDb = db.Customers;
            var addressesDb = db.Addresses;
            var ordersDb = db.Orders;

            // update customer
            Customer customer = await customersDb.SingleOrDefaultAsync(c => c.EmailAddress == payerInfo.Email);
            bool isNewCustomer = customer == null;
            if (isNewCustomer)
            {
                customer = new Customer()
                {
                    Registered = DateTime.Now,
                    EmailAddress = payerInfo.Email,
                    CustomerName = payerInfo.FirstName + " " +
                                    payerInfo.MiddleName + " " +
                                    payerInfo.LastName
                };
            }
            else
            {
                shoppingCart.Order.Customer = customer;
                shoppingCart.Order.CustomerId = customer.Id;
            }


            customer.LastVisited = DateTime.Now;
            if (isNewCustomer)
            {
                customersDb.Add(customer);
            }
            else
            {
                db.Entry(customer).State = EntityState.Modified;
            }

            // update address
            bool isNewAddress = true;
            if (!isNewCustomer)
            {
                AddressBase newAddress = payerInfo.ShippingAddress;
                newAddress.SetNullStringsToEmpty();
                ShipToAddress addressOnFile = await addressesDb.Where(a => a.Address1 == newAddress.Address1
                                                            && a.Address2 == newAddress.Address2
                                                            && a.City == newAddress.City
                                                            && a.State == newAddress.State
                                                            && a.PostalCode == newAddress.PostalCode
                                                            && a.Phone == newAddress.Phone
                                                            && a.Recipient == newAddress.Recipient
                                                            && a.CustomerId == customer.Id).FirstOrDefaultAsync();
                isNewAddress = addressOnFile == null;

                // don't add new address if already in database
                if (!isNewAddress)
                {
                    shoppingCart.Order.ShipToAddressId = addressOnFile.Id;
                }
            }

            shoppingCart.Order.ShipToAddress.Customer = customer;
            shoppingCart.Order.ShipToAddress.CustomerId = customer.Id;
            shoppingCart.Order.ShipToAddress.SetNullStringsToEmpty();

            // Add new address to database
            if (isNewAddress)
            {
                addressesDb.Add(shoppingCart.Order.ShipToAddress);
            }

            // bill to address the same as shipping address
            if (shoppingCart.Order.BillToAddress == null || shoppingCart.Order.BillToAddress.Address1 == null)
            {
                shoppingCart.Order.BillToAddress = shoppingCart.Order.ShipToAddress;
                shoppingCart.Order.BillToAddressId = shoppingCart.Order.ShipToAddressId;
            }

            await db.SaveChangesAsync();

            // don't add duplicate of product
            for (int i = 0; i < shoppingCart.Order.OrderDetails.Count; i++)
            {
                var orderDetail = shoppingCart.Order.OrderDetails[i];
                orderDetail.ProductId = orderDetail.Product.Id;
                db.Entry(orderDetail.Product).State = EntityState.Unchanged;
            }

            // add order to database
            shoppingCart.Order.DateOrdered = DateTime.Now;
            ordersDb.Add(shoppingCart.Order);

            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Custom verification of shopping cart, can be overriden by implementation
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <param name="paymentDetails"></param>
        protected virtual void CustomVerification(ShoppingCart shoppingCart, PaymentDetails paymentDetails)
        {
            AddressBase shippingAddress = paymentDetails.Payer.PayerInfo.ShippingAddress;
            if ((shoppingCart.Order.ShipToAddress.Country == "US" && shippingAddress.Country != "US") ||
                (shoppingCart.Order.ShipToAddress.Country != "US" && shippingAddress.Country == "US"))
            {
                // change to JSON error
                throw new ArgumentException("Your country does not match the country selected!");
            }
        }
    }
}
