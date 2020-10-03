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
// function ajaxCall(url,type,data){



//     $.ajax({
//         type: type,
//        async:false, 
//         url: url,
//         data:data,
//         //contentType: 'application/json; charset=UTF-8',  //send type of data to sever
//        // traditional: true,
        
//         success: function (response, statusText, xhr) {
//             if(type.toLowerCase()=='get'){
//             arr=JSON.parse(JSON.stringify(response));
//         }
           

//         },
//         error: function (XMLHttpRequest, textStatus, errorThrown) {                       
//             if(type.toLowerCase()!='get'){  bool=false;}

//         }
//      });
//      debugger;
//      if(type.toLowerCase()=='get'){return arr;}
        
//     if(type.toLowerCase()=='put'){ return bool;}
//     if(type.toLowerCase()=='post'){ return bool;}
//     if(type.toLowerCase()=='delete'){ return bool;}

    

// // if(type.toLowerCase()=='post')
// // {
// //     $.ajax({
// //         type: type,
// //         async: false,  
// //         url: url,
// //         //contentType: 'application/json; charset=UTF-8',  //send type of data to sever
// //        // traditional: true,
// //         data:data,//{
// //         //     Id:data.Id,
// //         //     Name: data.Name,
// //         //     Fname: data.FName,
// //         //     Email: data.Email,
// //         //     Phone: data.Phone,
// //         //     Dob: data.Dob,
// //         //     Password: data.Password,
// //         //     ConfirmPassword: data.ConfirmPassword

// //         // },
// //         success: function (response, statusText, xhr) {
          

// //         },
// //         error: function (XMLHttpRequest, textStatus, errorThrown) {                       
// //             bool=false;

// //         }
// //      }); 
// //      return bool;
// // }
// // if(type.toLowerCase()=='put')
// // {
    
// //     $.ajax({
// //         type: type,
// //         async: false,  
// //         url: url,
// //         data:{
// //             Id:data.Id,
// //             Name: data.Name,
// //             Fname: data.FName,
// //             Email: data.Email,
// //             Phone: data.Phone,
// //             Dob: data.Dob,
// //             Password: data.Password,
// //             ConfirmPassword: data.ConfirmPassword

// //         },
// //         success: function (response, statusText, xhr) {
          

// //         },
// //         error: function (XMLHttpRequest, textStatus, errorThrown) {                       
// //             bool=false;

// //         }
// //      });
// //      return bool;
// // }
// // if(type.toLowerCase()=='delete')
// // {
// //      $.ajax({
// //     type: type,
// //     async: false,  
// //     url: url,
// //     //contentType: 'application/json; charset=UTF-8',  //send type of data to sever
// //    // traditional: true,
   
// //     success: function (response, statusText, xhr) {
// //     },
// //     error: function (XMLHttpRequest, textStatus, errorThrown) {                       
// //         bool=false;

// //     }
// //  });
// //  return bool;
    
// // }

  
// }
