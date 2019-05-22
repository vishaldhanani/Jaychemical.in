$(document).ready(function () {
    SearchText2();
    SearchText3();
});
function SearchText2() {
    $("#tb_Search").autocomplete({
        open: function () {
            // After menu has been opened, set width to 100px
            $('.ui-menu').css("margin-top", "7px");
            $('.ui-menu').css("margin-left", "-11px");
            $('.ui-menu').css("width", "80%");
        },
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "AgentCustomers.aspx/GetCustomers",
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

function SearchText3() {

    $("#tb_CustInfo").autocomplete({
        open: function () {
            // After menu has been opened, set width to 100px
            $('.ui-menu').css("margin-top", "7px");
            $('.ui-menu').css("margin-left", "-11px");
            $('.ui-menu').css("width", "80%");
        },
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "AgentCustomers.aspx/GetCustomers",
                data: "{'SearchedTxt':'" + document.getElementById('tb_CustInfo').value + "'}",
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

    $("#tb_ConsigneeInfo").autocomplete({
        open: function () {
            // After menu has been opened, set width to 100px
            $('.ui-menu').css("margin-top", "7px");
            $('.ui-menu').css("margin-left", "-11px");
            $('.ui-menu').css("width", "80%");
        },
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "AgentCustomers.aspx/GetCustomers",
                data: "{'SearchedTxt':'" + document.getElementById('tb_ConsigneeInfo').value + "'}",
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