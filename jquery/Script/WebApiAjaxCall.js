function serverCall(url,type,data,successp,errorp){
    $.ajax({
        type: type,
        async: false,  
        url: url,
        headers: {
            'Authorization':  ReturnAuthHeader()
         },
        data:data,
        datatype:'json',
        //contentType: 'application/json; charset=UTF-8',  //send type of data to sever
       // traditional: true,
        
        success:successp,
      
        error:errorp
     });  
}
function ReturnAuthHeader()
{
  return  sessionStorage.getItem('accessToken')==null?null:'Bearer '+ sessionStorage.getItem('accessToken');
}