
function Init(){
    ValidateToken();
    $('#startDate').val(new Date().addDays(-7).toDateInputValue());
    $('#endDate').val(new Date().toDateInputValue());
    Search();
}
function Search(){
    const request = {
        Filter: {
            StartDate: $('#startDate').val(),
            EndDate: $('#endDate').val(),
            VehicleId: ''
        },
        Page: 1,
        Take: 50
    };
    
    $.ajax({
        type: 'POST',
        url: BASE_URL + 'FuelUp/search',
        data: JSON.stringify(request),
        dataType: 'application/json',
        contentType: 'application/json',
        beforeSend: function (xhr) {
            xhr.setRequestHeader ("Authorization", "Bearer "+ USER_TOKEN);
        },
        success: function(response){
            if(response.data.Success){
                let asd = '';
            }
        }
    });
}

function GetVehicles(){
    $.get(BASE_URL + 'vehicle', function(response){
        
    });
}

Init();