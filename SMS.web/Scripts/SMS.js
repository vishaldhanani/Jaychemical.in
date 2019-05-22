function Login() {
    try {
        if ($('#tb_Username').val() == '') {
            alert('Please enter user name');
            return false;
        }
        else if ($('#tb_Password').val() == '') {
            alert('Please enter user name');
            return false;
        }
        else {
            try {
                $.post("http://localhost/SMS.web/api/Login", { Username: $('#tb_Username').val(), Password: $('#tb_Password').val() },
                        function (data, status) {
                            alert("Data: " + data + "\nStatus: " + status);
                        });
            }
            catch (e) {
                alert(e);
            }
        }
    }
    catch (e) {
        alert(e);
    }
}

$(document).ready(function () {
    try {
        // Login function

    }
    catch (e) {
        alert(e);
    }
});