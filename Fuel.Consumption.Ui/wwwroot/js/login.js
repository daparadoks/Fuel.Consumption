function Login(){
    let username = $('#username').val();
    let password = $('#password').val();
    if(!username || !password)
        return;
    
    $.get(BASE_URL + 'authentication?username='+username+'&password='+password, function(response){
        if(!response.success){
            ShowAlert(response.message);
        }
        const token = response.data.token;
        if(!token)
        {
            ShowAlert("Giriş başarısız");
            return;
        }
        $.post('/login?token=' + token, function(response){
            if(!response.success)
            {
                ShowAlert("Giriş başarısız");
                return;
            }
            
            document.location.href = "/";
        });
    });
}