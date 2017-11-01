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

(function checkCountry() {
    if ($('#checkCountryIP').length === 0) return;
    $.getJSON('https://freegeoip.net/json/', function (data) {
        if (country !== 'US') {
            alert("You seem to be outside of the United States.  Please be advised that we cannot ship internationally.");
        }
    });
})();