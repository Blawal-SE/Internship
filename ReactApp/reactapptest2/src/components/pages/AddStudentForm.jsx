import React, { Component } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import $ from "jquery/dist/jquery.js";
import Select from "react-select";
import axios from "axios";

class StudentForm extends Component {
  state = {
    redirect: "",
    courseOptions: [],
    selectedcourses: [],
    Id: "",
    Base64Image: "",
    Name: "",
    FName: "",
    Email: "",
    Phone: "",
    Dob: "2014-02-09",
    Password: "",
    ConfirmPassword: "",
    ImageUrl: "",
    ThumbUrl: "",
  };

  handleCoursesOnChange = (e) => {
    debugger;
    if (e != null) {
      this.setState({ selectedcourses: e });
    } else {
      const Courses = [];
      this.setState({ selectedcourses: Courses});
    }
    //  this.setState({selectedCourses:[...this.state.selectedCourses,e]});
  };
  componentDidMount() {
    const self = this;
    if (self.props.edit) {
      debugger;
      axios
        .get("https://localhost:44393/api/Course", {
          headers: {
            Authorization:
              sessionStorage.getItem("accessToken") == null
                ? null
                : "Bearer " + sessionStorage.getItem("accessToken"),
          },
        })
        .then((resp) => {
          let options = [];
          debugger;
          console.log(resp);
          resp.data.map((course) => {
            const option = { value: course.CourseId, label: course.Name };
            this.setState({
              courseOptions: [...this.state.courseOptions, option],
            });

          });
          
      axios
      .get("https://localhost:44393/api/Student?id=" + self.props.Id, {
        headers: {
          Authorization:
            sessionStorage.getItem("accessToken") == null
              ? null
              : "Bearer " + sessionStorage.getItem("accessToken"),
        },
      })
      .then((resp) => {
        debugger;
        self.setState({ Id: resp.data.Id });
        self.setState({ Name: resp.data.Name });
        self.setState({ FName: resp.data.FName });
        self.setState({ Email: resp.data.Email });
        self.setState({ Phone: resp.data.Phone });
        self.setState({ Password: resp.data.Password });
        self.setState({ ImageUrl: resp.data.ImageUrl });
        self.setState({ ThumbUrl: resp.data.ThumbUrl });
        self.setState({ ConfirmPassword: resp.data.ConfirmPassword });
        self.setState({ Base64Image: resp.data.ImageBase64 });
        resp.data.StudentCourses.map((course) => {
          const Course = { value: course.CourseId, label: course.Name };
          self.setState({
            selectedcourses: [...this.state.selectedcourses, Course],
          });
        });
      }).catch(error=>{
        debugger;
       alert(error);
      });
        });

    } else {
      axios
        .get("https://localhost:44393/api/Course", {
          headers: {
            Authorization:
              sessionStorage.getItem("accessToken") == null
                ? null
                : "Bearer " + sessionStorage.getItem("accessToken"),
          },
        })
        .then((resp) => {
          let options = [];
          debugger;
          console.log(resp);
          resp.data.map((course) => {
            const option = { value: course.CourseId, label: course.Name };
            this.setState({
              courseOptions: [...this.state.courseOptions, option],
            });
          });
        });
    }
  }

  handleOnStudentSave = () => {
    debugger;
    const state = this.state;
    const courseList = [];
    state.selectedcourses.map((course) => {
      courseList.push(course.value);
    });
    const student = {
      Id:state.Id,
      Name: state.Name,
      FName: state.FName,
      Email: state.Email,
      Phone: state.Phone,
      Dob: state.Dob,
      Courses: courseList,
      Password: state.Password,
      ConfirmPassword: state.ConfirmPassword,
      ImageUrl: state.ImageUrl,
      ThumbUrl: state.ThumbUrl,
    };
    if (!this.props.edit) {
      axios
        .post("https://localhost:44393/api/Student", student, {
          headers: {
            Authorization:
              sessionStorage.getItem("accessToken") == null
                ? null
                : "Bearer " + sessionStorage.getItem("accessToken"),
          },
        })
        .then((resp) => {
          alert("Student added successfully");
          this.props.handleRefresh();
          this.RefreshForm();
        });
    } else {
      axios
        .put("https://localhost:44393/api/Student", student, {
          headers: {
            Authorization:
              sessionStorage.getItem("accessToken") == null
                ? null
                : "Bearer " + sessionStorage.getItem("accessToken"),
          },
        })
        .then((resp) => {
          debugger;
          this.props.handleRefresh();
          alert("Student updated successfully");
          this.props.handleClose();
        });
   
    }
  };
  handleChooseImage = (e) => {
    debugger;
    const self = this;
    var element = e.currentTarget;
    var formdata = new FormData();
    var totalfiles = element.files.length;
    for (var i = 0; i < totalfiles; i++) {
      var file = element.files[i];
      formdata.append("photo", file);
    }
    axios
      .post("https://localhost:44393/api/Upload", formdata)
      .then((response) => {
        debugger;
        if (response.data.Message == "Success") {
          self.setState({ ImageUrl: response.data.RealPath });
          self.setState({ ThumbUrl: response.data.ThumbPath });
          self.setState({ Base64Image: response.data.Base64Image });
        } else {
          alert("No file is Choosed");
        }
      });
  };
  RefreshForm = () => {
    const state = this.state;
    this.setState({ Name: "" });
    this.setState({ FName: "" });
    this.setState({ Email: "" });
    this.setState({ Phone: "" });
    this.setState({ courseList: "" });
    this.setState({ Password: "" });
    this.setState({ ConfirmPassword: "" });
    this.setState({ ImageUrl: "" });
    this.setState({ ThumbUrl: "" });
    this.setState({ Dob: "" });
    this.setState({selectedcourses:[]});
  };
  onChangeInput = (e) => this.setState({ [e.target.name]: e.target.value });
  onChangeDob = (e) => {
    debugger;
    const x = e.target.value;
    const { Dob } = this.state;
    this.setState({ Dob: e.target.value });
    //  this.setState(state => ({
    //   Dob: e.target.value
    // }));
  };
  render() {
    return (
      <div className="container">
        <form>
          <div className="row" id="parent" name="myForm">
            <div className="col-md-4">
              <div className="form-group ">
                <label>
                  <b>Name</b>
                </label>
                <input
                  onChange={this.onChangeInput}
                  id="Name"
                  type="text"
                  value={this.state.Name}
                  name="Name"
                  className="form-control"
                />
                <label id="Namelb"></label>
              </div>

              <div className="form-group">
                <label>
                  <b>Father Name</b>
                </label>

                <input
                  onChange={this.onChangeInput}
                  id="FName"
                  type="text"
                  name="FName"
                  value={this.state.FName}
                  className="form-control"
                />
                <label id="FNamelb"></label>
              </div>

              <div className="form-group">
                <label>
                  <b>Email</b>
                </label>

                <input
                  onChange={this.onChangeInput}
                  id="email"
                  type="text"
                  name="Email"
                  value={this.state.Email}
                  className="form-control"
                />
                <label id="emaillb"></label>
              </div>
              <div className="form-group">
                <label>
                  <b>Date of Birth</b>
                </label>

                <input
                  onChange={this.onChangeDob}
                  type="date"
                  value={this.state.Dob}
                  name="Dob"
                  className="form-control"
                />
              </div>
              <div className="form-group">
                <button
                  type="button"
                  onClick={this.handleOnStudentSave}
                  className="form-control btn btn-primary"
                >
                  Save
                </button>
              </div>
            </div>
            <div className="col-md-4">
              <div className="form-group">
                <label>
                  <b>Course</b>
                </label>
                <Select
                  value={this.state.selectedcourses}
                  closeMenuOnSelect={false}
                  options={this.state.courseOptions}
                  isMulti
                  onChange={this.handleCoursesOnChange}
                />
              </div>
              <div className="form-group">
                <label>
                  <b>Phone</b>
                </label>
                <input
                  onChange={this.onChangeInput}
                  id="phone"
                  type="text"
                  name="Phone"
                  value={this.state.Phone}
                  className="form-control"
                />
                <label id="phonelb"></label>
              </div>

              <div className="form-group">
                <label>
                  <b>Password</b>
                </label>

                <input
                  onChange={this.onChangeInput}
                  id="Password"
                  type="password"
                  value={this.state.Password}
                  name="Password"
                  className="form-control"
                />
                <label id="Passwordlb"></label>
              </div>
              <div className="form-group">
                <label>
                  <b>Confrim Password</b>
                </label>

                <input
                  name="ConfirmPassword"
                  id="ConfrimPassword"
                  type="password"
                  value={this.state.ConfirmPassword}
                  className="form-control"
                  onChange={this.onChangeInput}
                />
                <label id="ConfrimPasswordlb"></label>
              </div>
            </div>
            <div className="col-md-4">
              <div className="form-group">
                <label>choose Image</label>

                <input
                  id="imageupload"
                  className="form-control"
                  name="Image"
                  onChange={this.handleChooseImage}
                  type="file"
                  accept=".jpg , .png"
                />
              </div>
              <div className="form-group">
                <img
                  src={`data:image/jpeg;base64,${this.state.Base64Image}`}
                  className="img-fluid"
                  id="stdImage"
                />
              </div>
            </div>
          </div>
        </form>
      </div>
    );
  }
}
// const convertBase64=(file)=>{
//   return new Promise(resolve,reject)
//    }
export default StudentForm;
