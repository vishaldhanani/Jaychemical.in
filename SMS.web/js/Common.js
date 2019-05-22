$(document).ready(function () {
    $('.numeric').keypress(function (event) {
        var Key = (event.keyCode ? event.keyCode : event.which);
        //alert(Key);

        if (Key != 9 && Key != 8 && Key != 37 && Key != 39 && Key != 46 && (Key < 48 || Key > 57)) {
            event.preventDefault();
        } // prevent if not number/dot

        if (Key == 46 && $(this).val().indexOf('.') != -1) {
            event.preventDefault();
        } // prevent if already dot
    });

    //$('#rpt_Cart input[id*=tb_Qty]').change(function () {
    //    alert('change');
    //    try {
    //        var tb_Price = $(this).attr('id').toString().replace('tb_Qty', 'tb_Price');
    //        var tb_SellPrice = $(this).attr('id').toString().replace('tb_SellPrice', 'tb_Price');
    //        var Price = $('#' + tb_Price).val();
    //        var Qty = $(this).val();
    //        var SellPrice = 0;
    //        if(Price != '' && Qty != '')
    //        {
    //            SellPrice = parseFloat(Qty) * parseFloat(Price);
    //            $('#tb_SellPrice').val(parseFloat(SellPrice).toString().toFixed(2));
    //        }
    //        else
    //        {
    //            $('#tb_SellPrice').val(parseFloat(SellPrice).toString().toFixed(2));
    //        }
    //    } catch (e) {
    //        alert(e);
    //    }

    //});
});


function OrederReplacementCart() {
    try {

        var IsInvalidVCode = false;
        $('select[id*=dd_Variant]').each(function () {
            if ($(this).val() == '0') {
                IsInvalidVCode = true;
                $(this).focus();
            }
        });
        if (IsInvalidVCode == true) {
            alert("Please select Product Variant.");
            return false;
        }

        var isZeroQty = false;
        $('input[id*=tb_Qty]').each(function () {
            if ($(this).val() <= 0) {
                isZeroQty = true;
                $(this).focus();
            }
            if ($(this).val() == '') {
                isZeroQty = true;
                $(this).focus();
            }
        });
        if (isZeroQty == true) {
            alert('Please enter No. of Packs.');
            return false;
        }

        var IsPriceNull = false;
        $('input[id*=tb_Price]').each(function () {
            if ($(this).val() == '') {
                IsPriceNull = true;
                $(this).focus();
            }
        });
        if (IsPriceNull == true) {
            alert('Please enter price.');
            return false;
        }

        var IsCPriceNull = false;
        $('input[id*=txt_Customerprice]').each(function () {
            if ($(this).val() == '') {
                IsCPriceNull = true;
                $(this).focus();
            }
        });
        if (IsCPriceNull == true) {
            alert('Please enter price.');
            return false;
        }

       

        var IsInvalidUOM = false;
        $('select[id*=dd_UOM]').each(function () {
            if ($(this).val() == '0') {
                IsInvalidUOM = true;
                $(this).focus();
            }
        });
        if (IsInvalidUOM == true) {
            alert("Please select UOM Code");
            return false;
        }



        //$(document).ready(function () {
        //    //called when key is pressed in textbox
        //    $("#txt_Customerprice").keypress(function (e) {
        //        //if the letter is not digit then display error and don't type anything
        //        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        //            //display error message
        //            $("#errmsg").html("Digits Only").show().fadeOut("slow");
        //            return false;
        //        }
        //    });
        //});

        //var isRemarkNull = false;
        //$('input[id*=txtCustomerReferenceNo]').each(function () {
        //    if ($(this).val() <= 0) {
        //        isRemarkNull = true;
        //        $(this).focus();
        //    }
        //    if ($(this).val() == '') {
        //        isRemarkNull = true;
        //        $(this).focus();
        //    }
        //});
        //if (isRemarkNull == true) {
        //    alert('Customer Reference No should not be empty.');
        //    return false;
        //}

    } catch (e) {
        alert(e);
    }
}

function OrederReplacementCartForDyes() {
    try {
        debugger;
        var isZeroQty = false;
        $('input[id*=tb_Qty]').each(function () {
            if ($(this).val() <= 0) {
                isZeroQty = true;
                $(this).focus();
            }
            if ($(this).val() == '') {
                isZeroQty = true;
                $(this).focus();
            }
        });
        if (isZeroQty == true) {
            alert('Please enter Qty.');
            return false;
        }

        var IsPriceNull = false;
        $('input[id*=txt_billPrice]').each(function () {
            if ($(this).val() == '') {
                IsPriceNull = true;
                $(this).focus();
            }
        });
        if (IsPriceNull == true) {
            alert('Please enter Bill Price.');
            return false;
        }

        var IsCPriceNull = false;
        $('input[id*=txt_Customerprice]').each(function () {
            if ($(this).val() == '') {
                IsCPriceNull = true;
                $(this).focus();
            }
        });
        if (IsCPriceNull == true) {
            alert('Please enter Agent price.');
            return false;
        }

        var IsCPriceNull = false;
        $('input[id*=txt_discount_per]').each(function () {
            if ($(this).val() == '') {
                IsCPriceNull = true;
                $(this).focus();
            }
        });
        if (IsCPriceNull == true) {
            alert('Please enter Discount(%).');
            return false;
        }

        var IsCPriceNull = false;
        $('input[id*=txt_disc_amt]').each(function () {
            if ($(this).val() == '') {
                IsCPriceNull = true;
                $(this).focus();
            }
        });
        if (IsCPriceNull == true) {
            alert('Please enter Discount (Rs.)');
            return false;
        }
        
        var IsInvalidVCode = false;
        $('select[id*=dd_Variant]').each(function () {
            if ($(this).val() == '0') {
                IsInvalidVCode = true;
                $(this).focus();
            }
        });
        if (IsInvalidVCode == true) {
            alert("Please select Pack Size.");
            return false;
        }

        var IsInvalidUOM = false;
        $('select[id*=dd_UOM]').each(function () {
            if ($(this).val() == '0') {
                IsInvalidUOM = true;
                $(this).focus();
            }
        });
        if (IsInvalidUOM == true) {
            alert("Please select UOM Code");
            return false;
        }



        //$(document).ready(function () {
        //    //called when key is pressed in textbox
        //    $("#txt_Customerprice").keypress(function (e) {
        //        //if the letter is not digit then display error and don't type anything
        //        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        //            //display error message
        //            $("#errmsg").html("Digits Only").show().fadeOut("slow");
        //            return false;
        //        }
        //    });
        //});

        //var isRemarkNull = false;
        //$('input[id*=txtCustomerReferenceNo]').each(function () {
        //    if ($(this).val() <= 0) {
        //        isRemarkNull = true;
        //        $(this).focus();
        //    }
        //    if ($(this).val() == '') {
        //        isRemarkNull = true;
        //        $(this).focus();
        //    }
        //});
        //if (isRemarkNull == true) {
        //    alert('Customer Reference No should not be empty.');
        //    return false;
        //}

    } catch (e) {
        alert(e);
    }
}




$(document).ready(function () {
       

    //$('input[id*=tb_Qty]').change(function () {
    //    try {
          
    //        var tb_Price = $(this).attr('id').toString().replace('tb_Qty', 'tb_Price');
    //        var tb_SellPrice = $(this).attr('id').toString().replace('tb_Qty', 'tb_SellPrice');
    //        var Price = $('#' + tb_Price).val();
    //        var hf_Price = $(this).attr('id').toString().replace('tb_Qty', 'hf_Price');
    //        var Qty = $(this).val();        
    //        var SellPrice = 0;
    //        if (Price != '' && Qty != '') {
    //            if (parseFloat(Price) < parseFloat($('#' + hf_Price).val())) {
    //                alert('Please enter price above base price.');
    //                $('#' + tb_Price).val(parseFloat($('#' + hf_Price).val()).toFixed(2));
    //            }
    //            else {
    //                SellPrice = parseFloat(Qty) * parseFloat(Price);
                    
    //                if (SellPrice != NaN)
    //                    $('#' + tb_SellPrice).val(parseFloat(SellPrice).toFixed(2));
    //                else {
    //                    $('#' + tb_SellPrice).val('0.00');
    //                }
    //            }
    //            //if (Qty == '0') {
    //            //    alert('Please enter valid Quantity');
    //            //}                
    //        }
    //        else {
    //            $('#' + tb_SellPrice).val(parseFloat(SellPrice).toFixed(2));
    //        }
    //    } catch (e) {
    //        alert(e);
    //    }

    //});

    $('input[id*=tb_Price]').change(function () {
        try {

            var tb_Price = $(this).attr('id').toString().replace('tb_Price', 'tb_Price');
            var tb_SellPrice = $(this).attr('id').toString().replace('tb_Price', 'tb_SellPrice');
            var tb_Qty = $(this).attr('id').toString().replace('tb_Price', 'tb_Qty');
            var hf_Price = $(this).attr('id').toString().replace('tb_Price', 'hf_Price');
            var Qty = $('#' + tb_Qty).val();
            var Price = $('#' + tb_Price).val();
            var SellPrice = 0;
            if (Price != '' && Qty != '') {
                if (parseFloat(Price) < parseFloat($('#' + hf_Price).val())) {
                    alert('Please enter price above base price.');
                    $('#' + tb_Price).val(parseFloat($('#' + hf_Price).val()).toFixed(2));
                }
                else {
                    SellPrice = parseFloat(Qty) * parseFloat(Price);
                    
                    if (SellPrice != NaN)
                        $('#' + tb_SellPrice).val(parseFloat(SellPrice).toFixed(2));
                    else {
                        $('#' + tb_SellPrice).val('0.00');
                    }
                }
            }
            else {
                $('#' + tb_SellPrice).val(parseFloat(SellPrice).toFixed(2));
            }
        } catch (e) {
            alert(e);
        }

    });
    $('input[id*=txt_Customerprice]').change(function () {
        try {
            var tb_Price = $(this).attr('id').toString().replace('txt_Customerprice', 'txt_Customerprice');
            var tb_SellPrice = $(this).attr('id').toString().replace('txt_Customerprice', 'tb_SellPrice');
            var tb_Qty = $(this).attr('id').toString().replace('txt_Customerprice', 'tb_Qty');
            var hf_Price = $(this).attr('id').toString().replace('txt_Customerprice', 'hf_Price');
            var Qty = $('#' + tb_Qty).val();
            var Price = $('#' + tb_Price).val();
            var SellPrice = 0;
            if (Price != '' && Qty != '') {
                if (parseFloat(Price) < parseFloat($('#' + hf_Price).val())) {
                    alert('Please enter price above base price.');
                    $('#' + tb_Price).val(parseFloat($('#' + hf_Price).val()).toFixed(2));
                }
                else {
                    SellPrice = parseFloat(Qty) * parseFloat(Price);
                    
                    if (SellPrice != NaN)
                        $('#' + tb_SellPrice).val(parseFloat(SellPrice).toFixed(2));
                    else {
                        $('#' + tb_SellPrice).val('0.00');
                    }
                }
            }
            else {
                $('#' + tb_SellPrice).val(parseFloat(SellPrice).toFixed(2));
            }
        } catch (e) {
            alert(e);

        }
    });
    
});


// To validate terms and conditions at last module


