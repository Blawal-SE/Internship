var baseUrl='https://localhost:44330/api/';
function getAllCourse(url){
   
    serverCall(url,"GET",null,appendCourse,fail);
    function appendCourse(result){
       
        var options="";
      result.forEach(function(value,index){
       
         options+=`<option class="select2" value=${value.CourseId}>${value.Name}</option>`;
        });
        $('#courseslct').append(options);
    }
    function fail(){
        alert("ajax failed");
    }
    
}