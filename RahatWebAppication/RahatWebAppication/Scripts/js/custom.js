
//block screen on ajax call
$(document).ajaxStart(function () {
    $.blockUI({ message: '<h1 style="color:white">Loading..</h1>' });
}).ajaxStop(function () {
    $.unblockUI();
});

//bootstrap fileinput update scenario
function setInitialPreviewForImage(img) {
    $.fn.fileinput.defaults.initialPreview = ['<img src="' + img + '" class="file-preview-image" style="height:60px">'];
};

//search from items table
$('#tableSearch').keyup(function () {
    var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

    var $rows = $('#items tr');
    $rows.show().filter(function () {
        var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
        return !~text.indexOf(val);
    }).hide();
});

function makeImageFancy() {
    $(".fancybox").fancybox();
}

//search item by category
var idsArray = [];
var isScrolled = false;
var itemSearchRequest = {
    PageNo: 1,
    PageSize: 10
};
var priceRange = {};
function searchItemByCategory(id) {
    if (id) {
        if ($('#checkbox_' + id)[0].checked)
            idsArray.push(id);
        else
            idsArray.pop(id);
    }
    var lower = $('.slider-snap-value-lower')[0].innerText;
    var upper = $('.slider-snap-value-upper')[0].innerText;
    priceRange = {
        lower: parseInt(lower),        upper: parseInt(upper)
    }    itemSearchRequest.CategoryIds = idsArray;
    itemSearchRequest.PriceRange = priceRange;
    if (lower.length && upper.length) {
        $.ajax({
            type: "POST",
            url: '/Home/GetProduct/',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(itemSearchRequest),
            success: function (response) {
                if (!isScrolled) {
                    $('#displayProducts').empty();
                    $('#displayProducts').append(response);
                } else {
                    $('#displayProducts').append(response);
                    isScrolled = false;
                }
            },
            error: function (response) {
                alert('Error!');
            }
        });
    }
}

//search item by price$(".slider-snap-value-lower, .slider-snap-value-upper").on('DOMSubtreeModified', function () {
    searchItemByCategory(null);
});

//search item on scroll
$(window).scroll(function () {
    //on page end, load projects
    if ($(window).scrollTop() + $(window).height() === $(document).height()) {
        itemSearchRequest.PageNo += 1;
        isScrolled = true;
        searchItemByCategory(null);
    }
});

//get selected number of items
function getProductsOnSelect(selected) {
    itemSearchRequest.PageSize = selected.value;
    searchItemByCategory(null);
}