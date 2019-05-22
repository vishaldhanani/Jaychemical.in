function CheckLogIn()
{
    if($('#tb_UserId').val() == '')
    {
        alert('Please enter User Id');
    }
    else if ($('#tb_Password').val() == '') {
        alert('Please enter Pin No.');
    }
    else
    {
        
        var Name = $('#tb_UserId').val();
        var Pass = $('#tb_Password').val();
        try
        {
            $.ajax({
                type: "POST",
                url: "WebMethodPage.aspx/LoginCredential",
                data: JSON.stringify({ 'Username': Name, 'Password': Pass }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    //alert(data.d);
                    if (data.d == 'Success') {
                        //window.location.href = "AgentCompany.aspx";
                        window.location.href = "DashBoard.aspx";
                    }
                    else if (data.d == 'SuccessApp') {
                        //window.location.href = "AgentCompany.aspx";
                        window.location.href = "ChangePassword.aspx";
                    }
                    else {
                        alert('Please enter correct credentials.');
                        $('#tb_Password').val('');
                    }
                },
                error: function (data) {
                   alert(data.responseText);
                   // console.log(data);
                    //alert('Error');
                }
            });
        }
        catch(e)
        {
            alert(e);
        }
    }
}