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