function scrollIntoView(item) {
    if (item.length > 0) {
        $("html,body").animate({
            scrollTop: item.offset().top
        }, 'slow');
    }
}

function expandAllDescendants(item) {
    item.slideDown();
    item.find(".subTopicContent").each(function () { $(this).slideDown(); });
}

$(document).ready(function () {
    $('.accordion .subTopicName').click(function () {
        var id = $(this).attr("data-subtopic-id");
        var $context = $("#subTopicContent-" + id);

        if ($context.is(':hidden')) {
            $context.slideDown();
        } else {
            $context.slideUp();
        }

        return false;
    });

    $(".tableOfContents a").click(function (e) {
        var item = $($(this).attr("href"));
        expandAllDescendants(item);
        scrollIntoView(item);
        e.stopPropagation();
        return false;
    });

    // Scroll to the first visible context item on the page (if one exists)
    scrollIntoView($(".contextItem"));

});


//$(document).ready(function() {
//    new jQueryCollapse($(".accordion"), {
//        open: function() {
//            this.slideDown(150);
//        },
//        close: function() {
//            this.slideUp(150);
//        }
//    });
//});