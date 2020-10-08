var arr=[];
var bool=true;

function serverCall(url,type,data,successp,errorp){
   
    $.ajax({
        type: type,
        async: false,  
        url: url,
        data:data,
        //contentType: 'application/json; charset=UTF-8',  //send type of data to sever
       // traditional: true,
        
        success:successp,
      
        error:errorp
     });
   

     
}
//used to check parameter ajax call
function serverCallStudent(url,type,data,successp,errorp){
   
    $.ajax({
        type: type,
        async: false,  
        url: url,
        data:{
            student:data
        },
        //contentType: 'application/json; charset=UTF-8',  //send type of data to sever
       // traditional: true,
        
        success:successp,
      
        error:errorp
     });
   

     
}