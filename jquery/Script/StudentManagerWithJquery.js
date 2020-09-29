function addStudent(isruntime) {
   
    var name = $('#Name').val(); //document.getElementById("Name").value;
    var fName = $('#FName').val(); // document.getElementById("FName").value;
    var email = $('#email').val(); //document.getElementById("FName").value;
    var phone = $('#phone').val(); //document.getElementById("phone").value;
    var password = $('#Password').val(); //document.getElementById("Password").value;
    if (isruntime) {
        var confirmPassword = password;

    }
    //document.getElementById("ConfrimPassword").value;
    var dob = $('#DOB').val(); //document.getElementById("DOB").value;

    if (validateStudentForm(name, fName, email, phone, password, confirmPassword)) {

        var stdArr = [];
        //var stdDobStr = stdDob.toString();
        var student = {
            "Id": 1,
            "name": name,
            "fName": fName,
            "email": email,
            "Phone": phone,
            "dob": dob.toString(),
            "password": password,
            "ConfrimPassword": confirmPassword
        }
        var getStdArr = JSON.parse(localStorage.getItem("student"));

        if (getStdArr != null) {
            var lastStudent = getStdArr[getStdArr.length - 1];

            stdArr = getStdArr;
            student.Id = lastStudent.Id + 1;
        }
        stdArr.push(student);
        localStorage.setItem("student", JSON.stringify(stdArr));
        if (isruntime) {

            studentList();

        }
    } else {
        return false;
    }
}

function editStudentGet() {
   
    var student = JSON.parse(localStorage.getItem("editStudent"));
    if (student == null) {
        return false;
    }
    document.getElementById("id").value = student.Id;
    document.getElementById("Name").value = student.name;
    document.getElementById("FName").value = student.fName;
    document.getElementById("email").value = student.email;
    document.getElementById("phone").value = student.Phone;
    document.getElementById("Password").value = student.password;
    document.getElementById("ConfrimPassword").value = student.ConfrimPassword;
    document.getElementById("DOB").value = student.dob;
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

//            Single page CRud operation in table

function addStudentDynamic(student) {
   
    
        var stdArr = [];
        //var stdDobStr = stdDob.toString();
     
        var getStdArr = JSON.parse(localStorage.getItem("student"));
        student.Id=1;
        if (getStdArr != null) {
            var lastStudent = getStdArr[getStdArr.length - 1];

            stdArr = getStdArr;
            student.Id = lastStudent.Id + 1;
        }
        stdArr.push(student);
        localStorage.setItem("student", JSON.stringify(stdArr));
      studentList();
    
}


function studentList() {

    var stdList = JSON.parse(localStorage.getItem("student"));
    if (stdList == null) {
        return false;

    }

    // var tBody = $('#stdList').val();//document.getElementById("stdList");
    var appendRow = "";
    stdList.forEach(function(value, index) {
       
        appendRow += "<tr><td>" + value.name + "</td>" + "<td>" + value.fName + "</td>" + "<td>" + value.email + "</td>" + "<td>" + value.Phone + "</td>" + "<td>" + value.dob + "</td>" + "<td>" + value.password + "</td>";
        appendRow += "<td><button class='editBtn'  data-id=" + value.Id + ">Edit</button>";
        appendRow += "<button class='deleteBtn'  data-id=" + value.Id + ">Delete</a></td></tr>";
        //tBody.innerHTML += appendRow;

    });
    $('#stdList').html('').append(appendRow);
  

    $('.deleteBtn').click(function() {
       
        var id = parseInt($(this).attr('data-id'));
        deleteStudent(id);
    });
    $('.editBtn').click(function() {
        var id = parseInt($(this).attr('data-id'));
        var parent = $(this).closest('tr');
        
        var student=getStudent(id);
        var row = "<td>  <input id='Name' type='text'  value='"+student.name+"'/></td>";
        row += "<td>  <input id='FName' type='text'  value='"+student.fName+"'/></td>";
        row += "<td>  <input id='email' type='text'  value='"+student.email+"'/></td>";
        row += "<td>  <input id='phone' type='text'  value='"+student.Phone+"'/></td>";
        row += "<td>  <input id='DOB' type='date' value='"+student.dob+"'/></td>";
        row += "<td>  <input id='Password' type='password' value='"+student.password+"'/></td>";

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
        $('#updateBtn').click(function(){
        
            var parent=$(this).closest('tr');
           var student= getStudent(id);
           student.Id=id;
           student.name=parent.find('#Name').val();
           student.fName=parent.find('#FName').val();
           student.email=parent.find('#email').val();
           student.Phone=parent.find('#phone').val();
           student.dob=parent.find('#DOB').val();
           student.password=parent.find('#Password').val();
           student.confirmPassword=parent.find('#Password').val();
            if(validateStudent(student,parent.next()))
           {
           var students=JSON.parse(localStorage.getItem("student"));
           students.forEach(function(value, index) {
            if (value.Id == student.Id) {
                
                students[index] = student;
                localStorage.setItem("student", JSON.stringify(students));
                studentList();
                 }
             });
           }
        });
    
        $('#updateCancelBtn').click(function(){
            studentList();
        });


    });

}           


function addNewStudentRow() {
 
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
    $('#saveBtn').click(function() {
        var parent=$(this).closest('tr');
        var student= {}
       
        student.name=parent.find('#Name').val();
        student.fName=parent.find('#FName').val();
        student.email=parent.find('#email').val();
        student.Phone=parent.find('#phone').val();
        student.dob=parent.find('#DOB').val();
        student.password=parent.find('#Password').val();
        student.confirmPassword=parent.find('#Password').val();
         if(validateStudent(student,parent.next()))
        {
            addStudentDynamic(student);
        }
    });
    $('#removeBtn').click(function() {
        $('#stdList tr').last().remove();
        $('#stdList tr').last().remove();
    });

}
//commen

function validateStudent(student,parent) {
  
debugger;
    var check = true;
    //straightway to validate Name
    if (!fieldRequired(student.name)) {
        parent.find('#Namelb').html("Name is required"); //document.getElementById("Namelb").innerHTML = "Name is required";
        check = false;
    } else if (!isAplhabet(student.name)) {
        parent.find('#Namelb').html("Name cannot be Number"); // document.getElementById("Namelb").innerHTML = "Name cannot be Number";
        check = false;
    } else {
        parent.find('#Namelb').html(""); //document.getElementById("Namelb").innerHTML = "";
    }
    //straightway to validate Email
    if (!fieldRequired(student.email)) {
        parent.find('#emaillb').html("Email is required"); //document.getElementById("emaillb").innerHTML = "Email is required";
        check = false;
    } else if (!isEmail(student.email)) {
        parent.find('#emaillb').html("Email is incorrect"); //document.getElementById("emaillb").innerHTML = "Email is incorrect";
        check = false;
    } else {
        parent.find('#emaillb').html(""); // document.getElementById("emaillb").innerHTML = "";
    }
    //straightway to validate passwords
    if (!fieldRequired(student.password)) {
        parent.find('#Passwordlb').html("password is required"); //document.getElementById("Passwordlb").innerHTML = "password is required";
        check = false;
    } else {
        parent.find('#Passwordlb').html(""); //document.getElementById("Passwordlb").innerHTML = "";
    }

   

    return check;
}


//           End of single page crud Operation

function addStudent(isruntime) {
  
    var name = $('#Name').val(); //document.getElementById("Name").value;
    var fName = $('#FName').val(); // document.getElementById("FName").value;
    var email = $('#email').val(); //document.getElementById("FName").value;
    var phone = $('#phone').val(); //document.getElementById("phone").value;
    var password = $('#Password').val(); //document.getElementById("Password").value;
    if (isruntime) {
        var confirmPassword = password;

    }
    //document.getElementById("ConfrimPassword").value;
    var dob = $('#DOB').val(); //document.getElementById("DOB").value;

    if (validateStudentForm(name, fName, email, phone, password, confirmPassword)) {

        var stdArr = [];
        //var stdDobStr = stdDob.toString();
        var student = {
            "Id": 1,
            "name": name,
            "fName": fName,
            "email": email,
            "Phone": phone,
            "dob": dob.toString(),
            "password": password,
            "ConfrimPassword": confirmPassword
        }
        var getStdArr = JSON.parse(localStorage.getItem("student"));

        if (getStdArr != null) {
            var lastStudent = getStdArr[getStdArr.length - 1];

            stdArr = getStdArr;
            student.Id = lastStudent.Id + 1;
        }
        stdArr.push(student);
        localStorage.setItem("student", JSON.stringify(stdArr));
        if (isruntime) {

           window.location.reload();

        }
    } else {
        return false;
    }
}

function editStudent(id) {
  
    localStorage.setItem("editStudent", JSON.stringify(getStudent(id)));
    window.location.href = 'C:\Users\Administrator\Desktop\Internship\Script\editStudent.html';


}


function deleteStudent(id) {

    var student = JSON.parse(localStorage.getItem("student"));
    student.forEach(function(value, indx) {
        if (value.Id == id) {
            student.splice(indx, 1);
            localStorage.setItem("student", JSON.stringify(student));
            window.location.reload();

        }
    });


}

function validateStudentForm(name, fname, email, phone, password, confirmPassword) {
  

    var check = true;
    //straightway to validate Name
    if (!fieldRequired(name)) {
        $('#Namelb').html("Name is required"); //document.getElementById("Namelb").innerHTML = "Name is required";
        check = false;
    } else if (!isAplhabet(name)) {
        $('#Namelb').val("Name cannot be Number"); // document.getElementById("Namelb").innerHTML = "Name cannot be Number";
        check = false;
    } else {
        $('#Namelb').html(""); //document.getElementById("Namelb").innerHTML = "";
    }
    //straightway to validate Email
    if (!fieldRequired(email)) {
        $('#emaillb').html("Email is required"); //document.getElementById("emaillb").innerHTML = "Email is required";
        check = false;
    } else if (!isEmail(email)) {
        $('#emaillb').html("Email is incorrect"); //document.getElementById("emaillb").innerHTML = "Email is incorrect";
        check = false;
    } else {
        $('#emaillb').html(""); // document.getElementById("emaillb").innerHTML = "";
    }
    //straightway to validate passwords
    if (!fieldRequired(password)) {
        $('#Passwordlb').html("password is required"); //document.getElementById("Passwordlb").innerHTML = "password is required";
        check = false;
    } else {
        $('#Passwordlb').html(""); //document.getElementById("Passwordlb").innerHTML = "";
    }

    if (!fieldRequired(confirmPassword)) {
        $('#ConfrimPasswordlb').html("password is required"); // document.getElementById("ConfrimPasswordlb").innerHTML = "password is required";
        check = false;
    } else {
        $('#ConfrimPasswordlb').html(""); //  document.getElementById("ConfrimPasswordlb").innerHTML = "";

    }
    if (!isEqual(password, confirmPassword)) {
        $('#ConfrimPasswordlb').html("Password does not Match"); //  document.getElementById("ConfrimPasswordlb").innerHTML = "Password does not Match";
        check = false;
    } else {
        $('#ConfrimPasswordlb').html(""); // document.getElementById("ConfrimPasswordlb").innerHTML = "";

    }

    return check;
}
 function getStudent(id) {
    var foundIndex = 0;
    var student = JSON.parse(localStorage.getItem("student"));
    student.forEach(function(value, indx) {
        if (value.Id == id) {
            foundIndex = indx
        }
    });
    var editStudent = student[foundIndex];
    return editStudent;
}
