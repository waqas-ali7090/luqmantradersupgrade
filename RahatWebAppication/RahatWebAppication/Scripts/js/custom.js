(function () {

    //block screen on ajax call
    $(document).ajaxStart(function () {
        $.blockUI({ message: '<i style="color:#18ba9b; font-size: 85px" class="fa fa-certificate fa-spin"></i>' });
    }).ajaxStop(function () {
        $.unblockUI();
    });

    $(window).on("load", function () {
        $.unblockUI();
    });

    $(window).on("beforeunload", function () {
        $.blockUI({ message: '<i style="color:#18ba9b; font-size: 85px" class="fa fa-certificate fa-spin"></i>' });
    });

    //search from items table
    $('#tableSearch').keyup(function () {
        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

        var $rows = $('#items tr');
        $rows.show().filter(function () {
            var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
            return !~text.indexOf(val);
        }).hide();
    });

    //search item by price    //$(".slider-snap-value-lower, .slider-snap-value-upper").on('DOMSubtreeModified', function () {
    //    searchItemByCategory(null);
    //});

    //search item on scroll
    //$(window).scroll(function () {
    //    //on page end, load projects
    //    if ($(window).scrollTop() + $(window).height() === $(document).height()) {
    //        itemSearchRequest.PageNo += 1;
    //        isScrolled = true;
    //        searchItemByCategory(null);
    //    }
    //});

})();

//search item by category
var idsArray = [];
var isScrolled = false;
var itemSearchRequest = {
    PageNo: 1,
    PageSize: 9
};
var priceRange = {};
function searchItemByCategory(id, pageSize) {
    if (id) {
        isScrolled = true;
        if ($('#checkbox_' + id)[0].checked)
            idsArray.push(id);
        else
            idsArray.pop(id);
    }
    //var lower = $('.slider-snap-value-lower')[0].innerText;
    //var upper = $('.slider-snap-value-upper')[0].innerText;
    //priceRange = {
    //    lower: parseInt(lower),    //    upper: parseInt(upper)
    //}    itemSearchRequest.CategoryIds = idsArray;
    itemSearchRequest.PageSize = pageSize;
    //itemSearchRequest.PriceRange = priceRange;
    $.ajax({
        type: "POST",
        url: '/Home/GetProduct/',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(itemSearchRequest),
        success: function (response) {
            //if (!isScrolled) {
                $('#displayProducts').empty();
                $('#displayProducts').append(response);
            //} else {
            //    $('#displayProducts').append(response);
            //    isScrolled = false;
            //}
        },
        error: function (response) {
            alert('Error!');
        }
    });
}

$(document).ready(function () {
    $('.fancy').fancybox({
        titleShow: true,
        titlePosition: 'outside',
        titleFormat: null,
        'transitionIn': 'elastic',
        'transitionOut': 'elastic',
        'speedIn': 600,
        'speedOut': 200,
        'overlayShow': false
    });
});
//bootstrap fileinput update scenario
function setInitialPreviewForImage(img) {
    $.fn.fileinput.defaults.initialPreview = ['<img src="' + img + '" class="file-preview-image" style="height:60px">'];
};
