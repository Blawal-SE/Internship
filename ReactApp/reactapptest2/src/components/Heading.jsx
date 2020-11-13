import React, { Component } from "react";
import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";
import Login from "./pages/Login";
import About from "./pages/About";
import "../Content/sidebar.css";
import StudentForm from "./pages/AddStudentForm";
import App from "../App";
import $ from "jquery/dist/jquery.js";
import Calculator from './Calculator/calculator';
import CalculatorModal from "./Calculator/calculatormodel";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import Student from './pages/Student';

class Heading extends Component {
  state = {
    islogin: false,
  };
  constructor() {
    super();
    console.log("heading contrsuctor is called");
  }
  componentDidMount() {
    console.log("heading didmount is called");
  }
  componentDidUpdate() {
    console.log("heading componentDidUpdate is called");
  }
  componentDidCatch() {
    console.log("heading componentDidCatch is called");
  }
  toggle = () => {
    $("#wrapper").toggleClass("toggled");
  };
  afterLogin = () => {
    debugger;
    this.setState({ islogin: !this.state.islogin });
  };
  render() {
    if (!this.state.islogin) {
      return (
        <Router>
          <Route
            exact
            path="/"
            render={(props) => (
              <Login {...props} afterLogin={this.afterLogin} />
            )}
          />
        </Router>
      );
    }

    return (
      <Router>
        {/* <Route path="/about" component={About} />
        <Route path="/student/studentform" component={StudentForm} />
        <Route exact path="/" component={Login} />
        <Route path="/studenttable" component={App} /> */}

        <div className="d-flex" id="wrapper">
          <div className="bg-light border-right" id="sidebar-wrapper">
            <div className="sidebar-heading">School Management System</div>
            <div className="list-group list-group-flush">
              <a
                href="#"
                className="list-group-item list-group-item-action bg-light"
              >
                <Link to="/">Login</Link>
              </a>
              <a
                href="#"
                className="list-group-item list-group-item-action bg-light"
              >
                <Link to="/student/studentform">Add Student</Link>
              </a>
              <a
                href="#"
                className="list-group-item list-group-item-action bg-light"
              >
                <Link to="/studenttable">Studetn List</Link>
              </a>
              <a
                href="#"
                className="list-group-item list-group-item-action bg-light"
              >
                <Link to="/calculator">Calculator</Link>
              </a>
              <a
                href="#"
                className="list-group-item list-group-item-action bg-light"
              >
                Profile
              </a>
              <a
                href="#"
                className="list-group-item list-group-item-action bg-light"
              >
                Status
              </a>
            </div>
          </div>
          <div id="page-content-wrapper">
            <nav className="navbar navbar-expand-lg navbar-light bg-light border-bottom">
               <FontAwesomeIcon icon={['fas', 'ambulance']} />
              <FontAwesomeIcon icon="exchange"  onClick={this.toggle}/>
              <button className="btn btn-primary " onClick={this.toggle}>
                Toggle Menu
              </button>

              <button
                className="navbar-toggler"
                type="button"
                data-toggle="collapse"
                data-target="#navbarSupportedContent"
                aria-controls="navbarSupportedContent"
                aria-expanded="false"
                aria-label="Toggle navigation"
              >
                <span className="navbar-toggler-icon"></span>
              </button>

              <div
                className="collapse navbar-collapse"
                id="navbarSupportedContent"
              >
                <ul className="navbar-nav ml-auto mt-2 mt-lg-0">
                  <li className="nav-item active">
                   <CalculatorModal/>
                  </li>
                  <li className="nav-item">
                    <a className="nav-link" href="#">
                      Link
                    </a>
                  </li>
                  <li className="nav-item dropdown">
                    <a
                      className="nav-link dropdown-toggle"
                      href="#"
                      id="navbarDropdown"
                      role="button"
                      data-toggle="dropdown"
                      aria-haspopup="true"
                      aria-expanded="false"
                    >
                      Dropdown
                    </a>
                    <div
                      className="dropdown-menu dropdown-menu-right"
                      aria-labelledby="navbarDropdown"
                    >
                      <a className="dropdown-item" href="#">
                        Action
                      </a>
                      <a className="dropdown-item" href="#">
                        Another action
                      </a>
                      <div className="dropdown-divider"></div>
                      <a className="dropdown-item" href="#">
                        Something else here
                      </a>
                    </div>
                  </li>
                </ul>
              </div>
            </nav>

            <div className="container-fluid">
              <Route exact path="/login" component={Login}></Route>
              <Route
                exact
                path="/student/studentform"
                render={(props) => (
                  <StudentForm {...props} edit={false} Id={0} />
                )}
              />
              {/* <Route
                path="/student/studentform"
                component={StudentForm}
              ></Route> */}

              <Route path="/studenttable" component={Student}></Route>
              <Route path="/about" component={About}></Route>
              <Route path="/calculator" component={Calculator}></Route>
            </div>
          </div>
        </div>
      </Router>
    );
  }
}
export default Heading;
