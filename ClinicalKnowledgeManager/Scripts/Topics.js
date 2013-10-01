//$(document).ready(function () {
//    $('.accordion .subTopicName').click(function () {
//        var id = $(this).attr("data-subtopic-id");
//        var $context = $("#subTopicContent-" + id);

//        if ($context.is(':hidden')) {
//            $context.slideDown();
//        } else {
//            $context.slideUp();
//        }

//        return false;
//    });

//});

$(document).ready(function() {
    new jQueryCollapse($(".accordion"), {
        open: function() {
            this.slideDown(150);
        },
        close: function() {
            this.slideUp(150);
        }
    });
});