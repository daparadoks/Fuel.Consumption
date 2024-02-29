function Register(){
    let username = $('#username').val();
    let password = $('#password').val();
    let password2 = $('#password2').val();
    if(!username || !password || !password2 || password !== password2)
        return;

    const request = {
        Username : username,
        Password: password,
        PasswordValidation: password2
    }
    
    $.ajax({
        url: BASE_URL + 'authentication/register',
        type: 'POST',
        data: JSON.stringify(request),
        contentType: 'application/json',
        success: function (response) {
            if(response.success)
            {
                ShowAlert("Kayıt işlemi tamamlandı");
                window.location = '/';
                return;
            }

            ShowAlert(response.message);
        },
        error: function (response){
            ShowAlert(response.message);
        }
    });
}