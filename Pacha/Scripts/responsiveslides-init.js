$(document).ready(function () {
    $("#singleProductThumbnails").responsiveSlides({
        auto: false,
        nav: true,
        prevText: '<i class="fa fa-angle-left"></i>',
        nextText: '<i class="fa fa-angle-right"></i>',
        manualControls: '#singleProductThumbnailsPager'
    });
});