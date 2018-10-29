(function($) {
    $('li.has-child > a').on('click', function() {
        $(this).parent().toggleClass('opened');
    });
})(jQuery);