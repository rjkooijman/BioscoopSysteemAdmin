jQuery(document).Ready(function () {

    jQuery('#more').change(function () {
        if (this.checked) {

            jQuery('#aantalkaarten').fadeIn('fast').css("display", "inline-block");
            jQuery('#soortkaarten').fadeIn('fast').css("display", "inline-block");
        } else {
            jQuery('#aantalkaarten').fadeOut('fast');
            jQuery('#soortkaarten').fadeOut('fast');

        }
    })
});