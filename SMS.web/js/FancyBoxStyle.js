//jQuery(document).ready(function ($) {
$(document).ready(function () {
    
     $('.fancybox').fancybox({
    //$("[class=fancybox]").fancybox({
        'type': 'iframe',
        'padding': '0px',
        'width': '70%',
        'height': '70%',
        'autoScale': true,
        'transitionIn': 'elastic',
        'transitionOut': 'fade',
        'autoDimensions': 'true'
    });
    /*
     *  Different effects
     */
    // Change title type, overlay closing speed

  //  $("[class=fancybox-effects-a]").fancybox({

   $(".fancybox-effects-a").fancybox({
        helpers: {
            title: {
                type: 'outside'
                
            },
            overlay: {
                speedOut: 0
            }
        }
    });
});