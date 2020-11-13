import React, { Component } from "react";
import axios from "axios";
import StudentTable from "./components/pages/StudentTableRows";
import "bootstrap/dist/css/bootstrap.min.css";
class Student extends Component {
  state = {
    token:
      sessionStorage.getItem("accessToken") == null
        ? null
        : "Bearer " + sessionStorage.getItem("accessToken"),
    todos: [],
    students: [],
    Constants: {},
    refresh:true,
  };
  constructor() {
    super();
    console.log("app Counstructor called");
  }
  handleRefresh=()=>{
    debugger;
    axios
      .get("https://localhost:44393/api/Student", {
        headers: {
          Authorization: this.state.token,
        },
      })
      .then((resp) => {
        debugger;
        this.setState({ students: resp.data });
      });
  }
  componentDidMount() {
    console.log("app didMount called");
    axios
      .get("https://localhost:44393/api/Student", {
        headers: {
          Authorization: this.state.token,
        },
      })
      .then((resp) => this.setState({ students: resp.data }));
  }
  MarkChange = (id) => {
    this.setState({
      todos: this.state.todos.map((todo) => {
        if (todo.Id === id) {
          todo.completed = !todo.completed;
        }
        return todo;
      }),
    });
  };
  OnDel = (id) => {
    debugger;
    const self = this;
    axios
      .delete("https://localhost:44393/api/Student?id=" + id, {
        headers: {
          Authorization: this.state.token,
        },
      })
      .then((resp) => {
        debugger;
        console.log(resp);
        self.setState({
          students: [
            ...self.state.students.filter((student) => student.Id != id),
          ],
        });
      });
  };

  render() {
    console.log("app.js render is called");
    return (
      <React.Fragment>
        {" "}
        <StudentTable
          students={this.state.students}
          handleDelete={this.OnDel}
          handleRefresh={this.handleRefresh}
        />
      </React.Fragment>
    );
  }
}

export default Student;
