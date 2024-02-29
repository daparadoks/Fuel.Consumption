const BASE_URL = 'https://localhost:7235/api/v1/';
const LOCAL_URL = 'https://localhost:7137/';
let USER_TOKEN = '';

Date.prototype.addDays = function(days) {
    let date = new Date(this.valueOf());
    date.setDate(date.getDate() + days);
    return date;
}

Date.prototype.toDateInputValue = (function() {
    let local = new Date(this);
    local.setMinutes(this.getMinutes() - this.getTimezoneOffset());
    return local.toJSON().slice(0,10);
});

function ShowAlert(message){
    alert(message);
}

function ValidateToken(){
    if(!USER_TOKEN)
        USER_TOKEN = GetToken();
    
    if(!USER_TOKEN)
        document.location.href = "/login";
}

function GetToken(){
    return $('#userToken').val();
}

function Logout(){
    $.post('/logout');
    location.reload();
}