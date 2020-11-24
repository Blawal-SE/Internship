import React, { Component } from "react";
import "../Content/css/calculator.css";
class Calculator extends Component {
  state = {
    
      result:"",
      calculate:[],
     
  };
handleChange=(e)=>{
    debugger;
    let textToShow="";
    
   let value= e.target.getAttribute('value');
  
   if(value=="ac")
   {     this.setState({result:""});
   }else if(value=="equal"){
    this.setState({result:eval(this.state.result)});

   }else{
     this.setState({result:this.state.result+value});
    }
}
  render() {
    return (
        <div className="container">
      <div class="calculator card">
        <input
          type="text"
          class="calculator-screen z-depth-1"
          value={this.state.result}
          name="result"
          disabled
        />

        <div class="calculator-keys">
          <button type="button" onClick={this.handleChange}  name="plus" class="operator btn btn-info" value="+">
            +
          </button>
          <button type="button" name="minus" onClick={this.handleChange} class="operator btn btn-info" value="-">
            -
          </button>
          <button type="button" name="multiply" onClick={this.handleChange} class="operator btn btn-info" value="*">
            &times;
          </button>
          <button type="button" name="divide" onClick={this.handleChange} class="operator btn btn-info" value="/">
            &divide;
          </button>

          <button type="button" name="seven" onClick={this.handleChange} value="7" class="btn btn-light waves-effect">
            7
          </button>
          <button type="button" name="eight" onClick={this.handleChange} value="8" class="btn btn-light waves-effect">
            8
          </button>
          <button type="button" name="nine" onClick={this.handleChange} value="9" class="btn btn-light waves-effect">
            9
          </button>

          <button type="button" name="four" onClick={this.handleChange} value="4" class="btn btn-light waves-effect">
            4
          </button>
          <button type="button" name="five" onClick={this.handleChange} value="5" class="btn btn-light waves-effect">
            5
          </button>
          <button type="button" name="six" onClick={this.handleChange} value="6" class="btn btn-light waves-effect">
            6
          </button>

          <button type="button" name="one" value="1" onClick={this.handleChange} class="btn btn-light waves-effect">
            1
          </button>
          <button type="button" name="two" value="2" onClick={this.handleChange} class="btn btn-light waves-effect">
            2
          </button>
          <button type="button" name="three" value="3" onClick={this.handleChange} class="btn btn-light waves-effect">
            3
          </button>

          <button type="button" name="zero" value="0" onClick={this.handleChange} class="btn btn-light waves-effect">
            0
          </button>
          <button
            type="button"
            class="decimal function btn btn-secondary"
            value="."
            name="dot"
            onClick={this.handleChange}
          >
            .
          </button>
          <button
            type="button"
            class="all-clear function btn btn-danger btn-sm"
            value="ac"
            name="ac"
            onClick={this.handleChange}
          >
            AC
          </button>

          <button
            type="button"
            class="equal-sign operator btn btn-default"
            value="equal"
            name="equal"
            onClick={this.handleChange}
          >
            =
          </button>
        </div>
      </div>
      </div>
    );
  }
}

export default Calculator;
