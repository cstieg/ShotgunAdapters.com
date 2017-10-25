/* ****************************** Image Upload Preview ***************************************** */
function imageUpload(productId, containerId, templateId) {
    var e = window.event;
    var $container = $(containerId);
    var $template = $(templateId);

    for (var i = 0; i < e.srcElement.files.length; i++) {
        var file = e.srcElement.files[i];
        var reader = new FileReader();
        reader.onloadend = function () {
            var data = {
                file: file
            };
            var myFormData = new FormData();
            myFormData.append('file', file);
            $.ajax({
                type: 'POST',
                url: '/Products/AddImage/' + productId,
                data: myFormData,
                processData: false, // important
                contentType: false, // important
                success: function (response) {
                    debugger;
                },
                error: function (response) {
                    debugger;
                }
            }); 

        };
        reader.readAsDataURL(file);
    }
}

function imageDelete(productId, imageId) {
    $.post('/Products/DeleteImage/' + productId, { imageId: imageId }, function (response) {
        debugger;
    });
}