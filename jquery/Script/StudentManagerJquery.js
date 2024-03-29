var studentArray = [];
var baseUrl = 'https://localhost:44393/';
var basUrlApiNull='https://localhost:44393/';
var emptyImage='Content/images/emptyimage.jpg';
//#region Generic Ajax  crud operation On student Table 

function StudentListGeneric() {
    serverCall(basUrlApiNull+"/api/Student", "get", null, successg);
    // ajax start to get all student and put records in Table
    function successg(result) {
        debugger;
        var appendRow = "";
        result.forEach(function (value, index) {
     
            appendRow += "<tr><td>" + value.Name + "</td>" + "<td>" + value.FName + "</td>" + "<td>" + value.Email + "</td>" + "<td>" + value.Phone + "</td>" + "<td>" + value.Dob + "</td>" + "<td>" + value.Password + "</td>"+"<td>"+value.CoursesCount+"</td>";
          
            appendRow += "<td><button style='margin-left:5px;' class='editNewWindowBtn btn btn-secondary'  data-id=" + value.Id + ">Edit</button>";
            appendRow += "<button style='margin-left:5px;' class='deleteBtn btn btn-danger'  data-id=" + value.Id + ">Delete</button></td></tr>";
            //tBody.innerHTML += appendRow;

        });
        $('#stdList').html('').append(appendRow);
        $('.editNewWindowBtn').click(function () {
         var id=$(this).attr('data-id');
         localStorage.setItem('StudentId',id);
         window.location.href='EditStudent.html';
        });
        $('.deleteBtn').click(function () {
            debugger;
            var id = parseInt($(this).attr('data-id'));
            var url = baseUrl + '/api/Student?id=' + id;
            serverCall(url, "delete", null, successd);
            function successd() {
                StudentListGeneric();
            }
        });
        $('.editBtn').click(function () {
            var id = parseInt($(this).attr('data-id'));
            var url = baseUrl + 'Student?id=' + id;
            var parent = $(this).closest('tr');
            serverCall(url, "get", null, successGetStudent);
            function successGetStudent(data) {
                var row = "<td>  <input id='Name' type='text'  value='" + data.student.Name + "'/></td>";
                row += "<td>  <input id='FName' type='text'  value='" + data.student.FName + "'/></td>";
                row += "<td>  <input id='email' type='text'  value='" + data.student.Email + "'/></td>";
                row += "<td>  <input id='phone' type='text'  value='" + data.student.Phone + "'/></td>";
                row += "<td>  <input id='DOB' type='date' value='" + data.student.Dob + "'/></td>";
                row += "<td>  <input id='Password' type='password' value='" + data.student.Password + "'/></td>";
                row += "<td></td>";
                row += "<td><button id='updateBtn' type='button'>Update</button><button id='updateCancelBtn' type='button'>Cancel</button>  </td>";
                parent.html('').append(row);
                var row2 = "<tr><td> <label id='Namelb'/></td>";
                row2 += "<td><td>";
                row2 += "<td><label id='emaillb'></label></td>";
                row2 += "<td></td>";
                row2 += "<td></td>";
                row2 += "<td><label id='Passwordlb'></label></td>";
                row2 += "<td></td>";
                $(row2).insertAfter(parent);
            }
            $('#updateBtn').click(function () {
                var parent = $(this).closest('tr');
                var student = {};
                student.Id = id;
                student.Name = parent.find('#Name').val();
                student.FName = parent.find('#FName').val();
                student.Email = parent.find('#email').val();
                student.Phone = parent.find('#phone').val();
                student.Dob = parent.find('#DOB').val();
                student.Password = parent.find('#Password').val();
                student.ConfirmPassword = parent.find('#Password').val();
                if (validateStudent(student, parent.next())) {
                    serverCall(baseUrl+'Student', "put", student, successPutStudent);
                    function successPutStudent(result) {
                        StudentListGeneric();
                    }
                }
            });
            $('#updateCancelBtn').click(function () {
                StudentListGeneric();
            });

        });


    }
    //ajax end 
}

//add new  row 

function addNewStudentRow() {
    debugger;
    var row = "<tr><td><input id='Name' type='text' name='Name' /></td>";
    row += "<td><input id='FName' type='text' name='FName' /></td>";
    row += "<td><input id='email' type='text' name='Name' /></td>";
    row += "<td><input id='phone' type='text' name='Name' /></td>";
    row += "<td><input id='DOB' type='date' name='Name' /></td>";
    row += "<td><input id='Password' type='password' name='Name' /></td>";
    row += "<td></td>";
    row += "<td><button id='saveBtn' type='button'>Save</button><button id='removeBtn' type='button'>Remove</button>  </td></tr>";
    $('#stdList').append(row);
    var row2 = "<tr><td><label id='Namelb'/></label></td>";
    row2 += "<td></td>";
    row2 += "<td><label id='emaillb'></label></td>";
    row2 += "<td></td>";
    row2 += "<td></td>";
    row2 += "<td><label id='Passwordlb'></label></td>";
    row2 += "<td></td>";
    row2 += "<td></td>";

    $('#stdList').append(row2);
    $('#saveBtn').click(function () {
        var parent = $(this).closest('tr');
        var student = {}

        student.Name = parent.find('#Name').val();
        student.FName = parent.find('#FName').val();
        student.Email = parent.find('#email').val();
        student.Phone = parent.find('#phone').val();
        student.Dob = parent.find('#DOB').val();
        student.Password = parent.find('#Password').val();
        student.ConfirmPassword = parent.find('#Password').val();
        if (validateStudent(student, parent.next())) {
            serverCall(baseUrl+'Student', "post", student, successP, errorP)
            function successP(result) {
                StudentListGeneric();
            }
            function errorP(result) {
                alert("ajax failed to post data");
            }
            // addStudentDynamic(student);
        }
    });
    $('#removeBtn').click(function () {
        $('#stdList tr').last().remove();
        $('#stdList tr').last().remove();
    });

}
//end of add new row
//#endregion

//#region validate Student

function validateStudent(student, parent) {
    var check = true;
    //straightway to validate Name
    if (!fieldRequired(student.Name)) {
        parent.find('#Namelb').html("Name is required").addClass('error text-danger'); //document.getElementById("Namelb").innerHTML = "Name is required";
        check = false;
    } else if (!isAplhabet(student.Name)) {
        parent.find('#Namelb').html("Name cannot be Number").addClass('error text-danger'); // document.getElementById("Namelb").innerHTML = "Name cannot be Number";
        check = false;
    } else {
        parent.find('#Namelb').html(""); //document.getElementById("Namelb").innerHTML = "";
    }
    //straightway to validate Email
    if (!fieldRequired(student.Email)) {
        parent.find('#emaillb').html("Email is required").addClass('error text-danger'); //document.getElementById("emaillb").innerHTML = "Email is required";
        check = false;
    } else if (!isEmail(student.Email)) {
        parent.find('#emaillb').html("Email is incorrect").addClass('error text-danger'); //document.getElementById("emaillb").innerHTML = "Email is incorrect";
        check = false;
    } else {
        parent.find('#emaillb').html(""); // document.getElementById("emaillb").innerHTML = "";
    }
    //straightway to validate passwords
    if (!fieldRequired(student.Password)) {
        parent.find('#Passwordlb').html("password is required").addClass('error text-danger'); //document.getElementById("Passwordlb").innerHTML = "password is required";
        check = false;
    } else {
        parent.find('#Passwordlb').html(""); //document.getElementById("Passwordlb").innerHTML = "";
    }



    return check;
}

function validateStudentForm(student, parent) {


    var check = true;
    //straightway to validate Name
    if (!fieldRequired(student.student.Name)) {
        parent.find('#Namelb').html("Name is required").addClass('error text-danger'); //document.getElementById("Namelb").innerHTML = "Name is required";
        check = false;
    } else if (!isAplhabet(student.student.Name)) {
        parent.find('#Namelb').html("Name cannot be Number").addClass('error text-danger'); // document.getElementById("Namelb").innerHTML = "Name cannot be Number";
        check = false;
    } else {
        parent.find('#Namelb').html(""); //document.getElementById("Namelb").innerHTML = "";
    }
    //straightway to validate Email
    if (!fieldRequired(student.student.Email)) {
        parent.find('#emaillb').html("Email is required").addClass('error text-danger'); //document.getElementById("emaillb").innerHTML = "Email is required";
        check = false;
    } else if (!isEmail(student.student.Email)) {
        parent.find('#emaillb').html("Email is incorrect").addClass('error text-danger'); //document.getElementById("emaillb").innerHTML = "Email is incorrect";
        check = false;
    } else {
        parent.find('#emaillb').html(""); // document.getElementById("emaillb").innerHTML = "";
    }
    //straightway to validate passwords
    if (!fieldRequired(student.student.Password)) {
        parent.find('#Passwordlb').html("password is required").addClass('error text-danger'); //document.getElementById("Passwordlb").innerHTML = "password is required";
        check = false;
    } else {
        parent.find('#Passwordlb').html(""); //document.getElementById("Passwordlb").innerHTML = "";
    }



    return check;
}
//#endregion

//add student Form post method on Table
function addStudentP() {
  debugger;
    var name = $('#Name').val(); //document.getElementById("Name").value;
    var fName = $('#FName').val(); // document.getElementById("FName").value;
    var email = $('#email').val(); //document.getElementById("FName").value;
    var phone = $('#phone').val(); //document.getElementById("phone").value;
    var password = $('#Password').val(); //document.getElementById("Password").value;

    var confirmPassword = $('#ConfrimPassword').val(); //document.getElementById("ConfrimPassword").value;
    var dob = $('#DOB').val().toString(); //document.getElementById("DOB").value;
    var arr=$('#courseslct').val();
    var imageUrl=$('#ImageUrl').val();
    var thumburl=$('#ThumbUrl').val();
    debugger;
    var student = {
        "Name": name,
        "FName": fName,
        "Email": email,
        "Phone": phone,
        "Dob": dob.toString(),
        "Password": password,
        "ConfirmPassword": confirmPassword, 
         "Courses":arr,
         "ImageUrl":imageUrl,
         "ThumbUrl":thumburl
    }
    if (validateStudent(student, $('#parent'))) {
      serverCall(baseUrl+'Student',"POST",student,successP,function(fail){alert(fail)});
      function successP(){
       window.location.reload();
      }
    }
}

//#region  Edit Student On Another Page
function editStudentGet() {
    var studentId = parseInt(JSON.parse(localStorage.getItem("StudentId")));
    if (studentId == null) {
        return false;
    }
    serverCall(basUrlApiNull+'/api/Student?id='+studentId,"GET",null,successg);
    function successg(data,status){
        debugger;
        $('#id').val(data.Id);
        $('#Name').val(data.Name);
        $('#FName').val(data.FName);
        $('#email').val(data.Email);
        $('#phone').val(data.Phone);
        $('#Password').val(data.Password);
        $('#ConfrimPassword').val(data.ConfirmPassword);
        $('#DOB').val(data.Dob);
        if (data.ImageUrl == "" || data.ImageUrl == null) {
            $("#stdImage").attr("src", emptyImage);
            $("#ImageUrl").val(null);
        } else {
            
            $("#stdImage").attr("src", data.ImageUrl);
            $("#ImageUrl").val(data.ImageUrl);
        }
        debugger;
        data.StudentCourses.forEach(function(value,index){
            $('.select2').each(function () {
                var option = $(this);
                debugger;
                if (option.val() == value.CourseId) {
                    $(this).prop('selected', true);
                }
            });
        });
       localStorage.removeItem('StudentId');
    }
}

function editStudentPost() {
   
    var id = $("#id").val();
    var name = $("#Name").val();
    var fName = $("#FName").val();
    var email = $("#email").val();
    var phone = $("#phone").val();
    var password = $("#Password").val();
    var confirmPassword = $("#ConfrimPassword").val();
    var dob = $("#DOB").val();
    var coursesArr=$('#courseslct').val();
    var imageUrl=$('#ImageUrl').val();
    var student = {
        "Id": id,
        "Name": name,
        "FName": fName,
        "Email": email,
        "Phone": phone,
        "Dob": dob.toString(),
        "Password": password,
        "ConfirmPassword": confirmPassword,
        "Courses":coursesArr,
        "ImageUrl":imageUrl
    }
    if (validateStudent(student,$('#parent'))) {
        serverCall(basUrlApiNull+'/api/Student',"put",student,successP);
        function successP(){
         window.location.href='StudentList.html';
        }
    } else {
        return false;
    }
}
//#endregion 
//#region login
function Login()
{
    var data = {

        "username":$("#userName").val(),
        "password":$("#password").val(),
        "grant_type":'password'
    };
  

    serverCall(basUrlApiNull+'Login','POst',data,function(result){
        sessionStorage.setItem('accessToken', result.access_token);
         sessionStorage.setItem('User',JSON.stringify(result));
        // sessionStorage.setItem('username', result.Email);
        window.location.href = "StudentList.html";
    },function(failresult){
      alert(failresult);
    });
}
//#endregion

//#region GetUserDetail
function Getuser()
{
   return JSON.parse(sessionStorage.getItem('User'));
}
//#endregion
//#region imageUpload
function imageUpload(element)
{  
    var formdata = new FormData();
    var totalfiles = element.files.length;
  for (var i = 0; i < totalfiles; i++) {
    var file = element.files[i];
    formdata.append("photo", file);
  }
    $.ajax({
        type: 'post',
        url: basUrlApiNull+'/api/Upload',
        data: formdata,
        dataType: 'json',
        contentType: false,
        processData:false
    })
   .done(function (response) {
       debugger;
       if(response.Message=="Success")
       {
      $("#stdImage").attr("src", response.RealPath);
      $("#ImageUrl").val(response.RealPath);
      $("#ThumbUrl").val(response.ThumbPath);
     }else{
         alert("No file is Choosed")
     }
   })
   .fail(function (XMLHttpRequeat, textStatus, errorThrown) {
       alert("fail")
   });
}
//#endregion
function setEmptyImage(element)
{
element.attr("src", emptyImage);
}