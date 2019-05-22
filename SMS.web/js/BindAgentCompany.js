$(document).ready(function(){
    try
    {
        $.ajax({
            type: "POST",
            url: "WebMethodPage.aspx/GetAgentCompany",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (data) {
                alert(data.length);
                //if (data.d == 'Success') {
                //    window.location.href = "dashboard.html";
                //}
                //else {
                //    alert('Please enter correct credentials.');
                //}
            },
            error: function (data) {
                alert(data.responseText);
                //alert('Error');
            }
        });
    }
    catch(e)
    {
        alert(e);
    }
});
