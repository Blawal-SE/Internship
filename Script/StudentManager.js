function addStudent() {
    var name = document.getElementById("Name").value;
    var fName = document.getElementById("FName").value;
    var email = document.getElementById("email").value;
    var phone = document.getElementById("phone").value;
    var password = document.getElementById("Password").value;
    var confirmPassword = document.getElementById("ConfrimPassword").value;
    var dob = document.getElementById("DOB").value;

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

    } else {
        return false;
    }
}

function studentList() {
    debugger;
    var stdList = JSON.parse(localStorage.getItem("student"));
    if (stdList == null) {
        return false;

    }

    var tBody = document.getElementById("stdList");

    stdList.forEach(function(value, index) {

        var appendRow = "<tr><td>" + value.name + "</td>" + "<td>" + value.fName + "</td>" + "<td>" + value.email + "</td>" + "<td>" + value.Phone + "</td>" + "<td>" + value.dob + "</td>" + "<td>" + value.password + "</td>";
        appendRow += "<td><a href='EditStudent.html' onclick='editStudent(" + value.Id + ")'>Edit</a>";
        appendRow += "<a href='StudentList.html'  onclick='deleteStudent(" + value.Id + ")'>Delete</a></td></tr>";
        tBody.innerHTML += appendRow;

    });
}

function validateStudentForm(name, fname, email, phone, password, confirmPassword) {


    var check = true;
    //straightway to validate Name
    if (!fieldRequired(name)) {
        document.getElementById("Namelb").innerHTML = "Name is required";
        check = false;
    } else if (!isAplhabet(name)) {
        document.getElementById("Namelb").innerHTML = "Name cannot be Number";
        check = false;
    } else {
        document.getElementById("Namelb").innerHTML = "";
    }
    //straightway to validate Email
    if (!fieldRequired(email)) {
        document.getElementById("emaillb").innerHTML = "Email is required";
        check = false;
    } else if (!isEmail(email)) {
        document.getElementById("emaillb").innerHTML = "Email is incorrect";
        check = false;
    } else {
        document.getElementById("emaillb").innerHTML = "";
    }
    //straightway to validate passwords
    if (!fieldRequired(password)) {
        document.getElementById("Passwordlb").innerHTML = "password is required";
        check = false;
    } else {
        document.getElementById("Passwordlb").innerHTML = "";
    }

    if (!fieldRequired(confirmPassword)) {
        document.getElementById("ConfrimPasswordlb").innerHTML = "password is required";
        check = false;
    } else {
        document.getElementById("ConfrimPasswordlb").innerHTML = "";

    }
    if (!isEqual(password, confirmPassword)) {
        document.getElementById("ConfrimPasswordlb").innerHTML = "Password does not Match";
        check = false;
    } else {
        document.getElementById("ConfrimPasswordlb").innerHTML = "";

    }

    return check;
}

function deleteStudent(id) {

    var student = JSON.parse(localStorage.getItem("student"));
    student.forEach(function(value, indx) {
        if (value.Id == id) {
            student.splice(indx, 1);
            localStorage.setItem("student", JSON.stringify(student));
            //  window.location.href = 'C:\Users\Administrator\Desktop\Internship\StudentList.html';

        }
    });


}



function editStudent(id) {
    debugger;
    var foundIndex = 0;
    var student = JSON.parse(localStorage.getItem("student"));
    student.forEach(function(value, indx) {
        if (value.Id == id) {
            foundIndex = indx
        }
    });
    var editStudent = student[foundIndex];
    localStorage.setItem("editStudent", JSON.stringify(editStudent));
    window.location.href = 'C:\Users\Administrator\Desktop\Internship\Script\editStudent.html';


}

function editStudentGet() {
    debugger;
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
    debugger;
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
                debugger;
                students[index] = student;
                localStorage.setItem("student", JSON.stringify(students));
                window.location.href = 'C:\Users\Administrator\Desktop\Internship\StudentList.html';
            }
        })

    } else {
        return false;
    }
}