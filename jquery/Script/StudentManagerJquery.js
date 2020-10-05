var studentArray = [];
var baseUrl = 'https://localhost:44330/api/';

//#region Generic Ajax  crud operation On student Table 

function StudentListGeneric() {
    debugger;
    // var stdList =GetStudentListApi;//JSON.parse(localStorage.getItem("student"));
    // studentArray=
    serverCall(baseUrl+"Student", "get", null, successg);

    // ajax start to get all student and put records in Table
    function successg(result) {
        var appendRow = "";
        result.forEach(function (value, index) {

            appendRow += "<tr><td>" + value.student.Name + "</td>" + "<td>" + value.student.FName + "</td>" + "<td>" + value.student.Email + "</td>" + "<td>" + value.student.Phone + "</td>" + "<td>" + value.student.Dob + "</td>" + "<td>" + value.student.Password + "</td>"+"<td>"+value.CoursesCount+"</td>";
            appendRow += "<td><button class='editBtn btn btn-primary'  data-id=" + value.Id + ">Edit</button>";
            appendRow += "<button style='margin-left:5px;' class='editNewWindowBtn btn btn-secondary'  data-id=" + value.Id + ">Edit With Courses</button>";
            appendRow += "<button style='margin-left:5px;' class='deleteBtn btn btn-danger'  data-id=" + value.Id + ">Delete</a></td></tr>";
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
            var url = baseUrl + 'Student?id=' + id;
            serverCall(url, "delete", null, successd);
            function successd() {
                StudentListGeneric();
            }
            // deleteStudent(id);
        });
        $('.editBtn').click(function () {
            debugger;
            var id = parseInt($(this).attr('data-id'));
            var url = baseUrl + 'Student?id=' + id;
            var parent = $(this).closest('tr');

            // var student=ajaxCall(url,"get");
            serverCall(url, "get", null, successGetStudent);

            function successGetStudent(student) {
                var row = "<td>  <input id='Name' type='text'  value='" + student.Name + "'/></td>";
                row += "<td>  <input id='FName' type='text'  value='" + student.FName + "'/></td>";
                row += "<td>  <input id='email' type='text'  value='" + student.Email + "'/></td>";
                row += "<td>  <input id='phone' type='text'  value='" + student.Phone + "'/></td>";
                row += "<td>  <input id='DOB' type='date' value='" + student.Dob + "'/></td>";
                row += "<td>  <input id='Password' type='password' value='" + student.Password + "'/></td>";

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
                var student = {};//getStudent(id);
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


    // var tBody = $('#stdList').val();//document.getElementById("stdList");



}

//add new  row 

function addNewStudentRow() {
    debugger;
    var row = "<tr><td>  <input id='Name' type='text' name='Name' /></td>";
    row += "<td>  <input id='FName' type='text' name='FName' /></td>";
    row += "<td>  <input id='email' type='text' name='Name' /></td>";
    row += "<td>  <input id='phone' type='text' name='Name' /></td>";
    row += "<td>  <input id='DOB' type='date' name='Name' /></td>";
    row += "<td>  <input id='Password' type='password' name='Name' /></td>";

    row += "<td><button id='saveBtn' type='button'>Save</button><button id='removeBtn' type='button'>Remove</button>  </td></tr>";
    $('#stdList').append(row);
    var row2 = "<tr><td><label id='Namelb'/></label></td>";
    row2 += "<td></td>";
    row2 += "<td><label id='emaillb'></label></td>";
    row2 += "<td></td>";
    row2 += "<td></td>";
    row2 += "<td><label id='Passwordlb'></label></td>";
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
            if (serverCall(baseUrl+'Student', "post", student, successP, errorP)) { StudentListGeneric(); } else { alert("ajax failed to post data"); }
            function successP(result) {
                return true;
            }
            function errorP(result) {
                return false;
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

//add student Form post method
function addStudent() {
    debugger;
    var name = $('#Name').val(); //document.getElementById("Name").value;
    var fName = $('#FName').val(); // document.getElementById("FName").value;
    var email = $('#email').val(); //document.getElementById("FName").value;
    var phone = $('#phone').val(); //document.getElementById("phone").value;
    var password = $('#Password').val(); //document.getElementById("Password").value;

    var confirmPassword = $('#ConfrimPassword').val(); //document.getElementById("ConfrimPassword").value;
    var dob = $('#DOB').val().toString(); //document.getElementById("DOB").value;
    var arr=$('#courseslct').select2('data');
    var courses="";
    debugger;
    arr.forEach(function (value,index,array) {
        if(array.length<=index+1){
        courses += value.id;
        }else{
            courses += value.id+",";;
        }

    });
    var student = {
      "student":{
        "Name": name,
        "FName": fName,
        "Email": email,
        "Phone": phone,
        "Dob": dob.toString(),
        "Password": password,
        "ConfirmPassword": confirmPassword 
              },
         "Courses":courses
       
    }
    if (validateStudentForm(student, $('#parent'))) {

        var bool = true;
        debugger;
  
      //  serverCallStudent('https://localhost:44330/api/Student', "Post",obj, success, error)
      serverCall(baseUrl+'Student?student='+JSON.stringify(student),"POST",null,successP);
      function successP(){
       window.location.reload();
      }
    }
    //   $.ajax({
    //     type: "post",
    //     async: false,  
    //     url: 'https://localhost:44330/api/Student?student='+JSON.stringify(student),
        
    //     contentType: 'application/json; charset=UTF-8',  //send type of data to sever
    //     traditional: true,
        
    //     success:function(){

    //     },
      
    //     error:function(){

    //     }
    //  });
    //     return bool;
    // }
    // else {
    //     return false;
    // }


}

//#region  Edit Student On Another Page
function editStudentGet() {
  
    var studentId = JSON.parse(localStorage.getItem("StudentId"));
    if (studentId == null) {
        return false;
    }
    serverCall(baseUrl+'Student?id='+studentId,"GET",null,successg);
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
        $('#courseslct').val(data.Courses);
    }
   
}

function editStudentPost() {
    
    var id = document.getElementById("id").value;
    var name = document.getElementById("Name").value;
    var fName = document.getElementById("FName").value;
    var email = document.getElementById("email").value;
    var phone = document.getElementById("phone").value;
    var password = document.getElementById("Password").value;
    var confirmPassword = document.getElementById("ConfrimPassword").value;
    var dob = document.getElementById("DOB").value;

    if (validateStudentForm(name, fName, email, phone, password, confirmPassword)) {

        // var stdArr = [];
        //var stdDobStr = stdDob.toString();
        var student = {
            "Id": id,
            "name": name,
            "fName": fName,
            "email": email,
            "Phone": phone,
            "dob": dob.toString(),
            "password": password,
            "ConfrimPassword": confirmPassword
        }
        var students = JSON.parse(localStorage.getItem("student"));

        students.forEach(function(value, index) {
            if (value.Id == student.Id) {
               
                students[index] = student;
                localStorage.setItem("student", JSON.stringify(students));
                window.location.href = 'C:\Users\Administrator\Desktop\Internship\StudentList.html';
            }
        })

    } else {
        return false;
    }
}
//#endregion 