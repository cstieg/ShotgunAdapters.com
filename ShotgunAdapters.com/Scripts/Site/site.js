// Slidedown on product menu
$('.dropdown').on('mouseover', function () {
    $(this).find('.dropdown-menu').first().stop(true, true).slideDown();
});

$('.dropdown').on('mouseout', function () {
    if ($(this).find('.dropdown-menu:hover').length === 0 && 
        $(this).find('.dropdown:hover').length === 0) {
        $(this).find('.dropdown-menu').first().stop(true, true).slideUp();
    } 
});

$('.dropdown-menu').on('mouseout', function () {
    if ($(this).find('.dropdown-menu:hover').length === 0 &&
        $(this).find('.dropdown:hover').length === 0) {
        $(this).find('.dropdown-menu').first().stop(true, true).slideUp();
    }
});

// Autofill product name after adding calibers
$('select[name=AmmunitionCaliberId]').on('focusout', function (e) {
    var gunId = $('select[name=GunCaliberId]').val();
    var gunName = $('select[name=GunCaliberId] > option[value=' + gunId + ']').text();
    var ammunitionId = $(e.target).val();
    var ammunitionName = $('select[name=AmmunitionCaliberId] > option[value=' + ammunitionId + ']').text();
    var $productName = $('input[name=Name]');
    if ($productName.val() == '') {
        $productName.val(gunName + ' to shoot ' + ammunitionName);
    }
});