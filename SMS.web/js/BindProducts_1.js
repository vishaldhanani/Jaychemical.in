var pageIndex = 1;
var pageCount;
$(window).scroll(function () {
    if ($(window).scrollTop() == $(document).height() - $(window).height()) {
        GetProducts();
    }
});

function GetProducts() {
    pageIndex++;
    debugger;
    // alert("I" + pageIndex + "C" + pageCount);
    //if (pageIndex == 2 || pageIndex <= pageCount) {
    $("#loader").show();
    $.ajax({
        type: "POST",
        url: "ProductItemNew.aspx/LoadProducts",
        data: '{pageIndex: ' + pageIndex + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
        failure: function (response) {
            alert(response.d);
        },
        error: function (response) {
            alert(response.d);
        }
    });
    //}
}
function OnSuccess(response) {
    debugger;
    var jsonData = response.d;
    $("#loader").hide();
    for (var i = 0; i < jsonData.length; i++) {
        var prodcutsData = "<li><div class='blk_1'>" + jsonData[i].ItemNo + "</div><div class='blk_2'><span id='s_Description'>" + jsonData[i].Description + "</span><a id='lb_AddToCart' onclick='AddToCart(\"" + jsonData[i].ItemNo + "\");' class='addtocart'>Add to Cart</a></div></li>";
        $('#div_Products ul').append(prodcutsData);
        //pageCount = jsonData[i].PageCount;
    }
}
function AddToCart(ItemNo) {
    if (ItemNo != '') {      
        $.ajax({
            type: "POST",
            url: "ProductItemNew.aspx/AddToCart",
            data: "{ItemNo: '" + ItemNo + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var obj = response.d;

                if (isNaN(obj)) {
                    alert(obj);
                }
                else {
                    $('#l_CartCount').html(obj);
                    // as per new change of alpesh sir
                    window.location.href = 'OrderReplacementCartNew.aspx?ProductCode= '+ ItemNo +'';
                    //alert("Selected Product Successfully Added To Cart")
                  //  alert("Selected Product Successfully Added To Cart");

                }
            },
            failure: function (response) {
                alert(response.d);
            },
            error: function (response) {
                alert(response.d);
            }
        });
    }
}

function AddToCart_ByItemCategory(ItemNo) {
    if (ItemNo != '') {
        $.ajax({
            type: "POST",
            url: "ProductItemNew.aspx/AddToCart_ByItemCategory",
            data: "{ItemNo: '" + ItemNo + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var obj = response.d;

                if (isNaN(obj)) {
                    alert(obj);
                }
                else {
                    $('#l_CartCount').html(obj);
                    // as per new change of alpesh sir  - For Dyes and Dis.Dyes
                    window.location.href = 'OrderReplacementCart_ItemCategoryBased.aspx?ProductCode= ' + ItemNo + '';
                    //alert("Selected Product Successfully Added To Cart");
                }
            },
            failure: function (response) {
                alert(response.d);
            },
            error: function (response) {
                alert(response.d);
            }
        });
    }
}


//function PrintPDF(OrderNo) {
//    if (OrderNo != '') {
//        $.ajax({
//            type: "POST",
//            url: "OrderConfirmation.aspx/PrintPDF",
//            data: "{OrderNo: '" + OrderNo + "'}",
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function (response) {
//                var obj = response.d;

//                if (isNaN(obj)) {
//                    alert(obj);
//                }
//                else {
//                    //$('#l_CartCount').html(obj);
//                    alert("Order Confirmation PDF generated Successfully.")
//                }
//            },
//            failure: function (response) {
//                alert(response.d);
//            },
//            error: function (response) {
//                alert(response.d);
//            }
//        });
//    }
//}


function validationAddtoCart() {
    try
    {
        if($('#l_CartCount').text() == "0")
        {
            alert('Please add at least one item in cart for Order Placement!');
        }
        else
        {
            // changed as per new requirement  - Alpesh Sir - 23/07/2015
            window.location.href = 'OrderReplacementCartNew.aspx';
        }
    }
    catch (e)
    {
        alert(e);
    }
}


// AutoComplete for Products

$(document).ready(function () {

    SearchText1();

    $("a.menu_res").click(function (e) {
        e.preventDefault();

        if ($("#nav").find('.activemenu').length > 0) {
            $(".nav_inner").css("left", "-300px");
            $(this).removeClass("activemenu");
            $("body").removeClass("bigsize");
        } else {
            $(".nav_inner").css("left", "0");
            $(this).addClass("activemenu");
            $("body").addClass("bigsize");
        }
        $(".nav_inner").addClass("done");
    });
});


function SearchText1() {
    debugger;
    $("#tb_Search").autocomplete({
        open: function () {
            // After menu has been opened, set width to 100px
            //$('.ui-menu').css("margin-top", "7px");
            //$('.ui-menu').css("margin-left", "-11px");
            //$('.ui-menu').css("width", "80%");
        },
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "ProductItemNew.aspx/GetProducts",
                data: "{'SearchedTxt':'" + document.getElementById('tb_Search').value + "'}",
                dataType: "json",
                success: function (data) {
                    response(data.d);
                },
                error: function (result) {
                    alert("Error");
                }
            });
        }
    });
}

$(document).ready(function () 
    {$('#btn_Submit').click(function () {
        if (!$('#cb_Terms').is(':checked')) {
            alert('Please agree to terms and conditions.');
        }
    });


//(function ($) {
//    $(document).ready(function () {
//        $('#cssmenu > ul > li > a').click(function () {
//            $('#cssmenu li').removeClass('active');
//            $(this).closest('li').addClass('active');
//            var checkElement = $(this).next();
//            if ((checkElement.is('ul')) && (checkElement.is(':visible'))) {
//                $(this).closest('li').removeClass('active');
//                checkElement.slideUp('normal');
//            }
//            if ((checkElement.is('ul')) && (!checkElement.is(':visible'))) {
//                $('#cssmenu ul ul:visible').slideUp('normal');
//                checkElement.slideDown('normal');
//            }
//            if ($(this).closest('li').find('ul').children().length == 0) {
//                return true;
//            } else {
//                return false;
//            }
//        });
//    });
//})(jQuery);



    /*
     *  Simple image gallery. Uses default settings
     */
    //$j('.fancybox').fancybox();
    $('.fancybox').fancybox({
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