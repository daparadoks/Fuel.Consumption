function Init(){
    $('#startDate').val(new Date().addDays(-7).toDateInputValue());
    $('#endDate').val(new Date().toDateInputValue());
    Search();
}
function Search(){
    const request = {
        Filter: {
            StartDate: $('#startDate').val(),
            EndDate: $('#endDate').val()
        },
        Page: 1,
        Take: 50
    };
    
    $.ajax({
        type: 'POST',
        url: BASE_URL + 'FuelUp/search',
        data: request,
        dataType: 'application/json',
        success: function(response){
            if(response.data.Success){
                let asd = '';
            }
        }
    });
}

Init();