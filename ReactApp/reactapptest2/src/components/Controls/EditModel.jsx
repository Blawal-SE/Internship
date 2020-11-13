import React, { Component } from 'react';
import Button from '@material-ui/core/Button';
import Modal from '@material-ui/core/Modal';
import StudentForm from "../pages/AddStudentForm";

class EditModal extends Component {
    state = { 
        open:false,
       
     }
      handleOpen = () => {
       
        this.setState({open:!this.state.open});
        this.props.handleEdit.bind(this,this.props.Id);
      };
    
       handleClose = () => {
        this.setState({open:!this.state.close});
      };
    
    render() { 
        return (
          <div>
          <Button variant="contained" color="primary" onClick={this.handleOpen}>
            Edit
          </Button>
          <Modal
            open={this.state.open}
            onClose={this.handleClose}
            aria-labelledby="simple-modal-title"
            aria-describedby="simple-modal-description"
          >
            {body}
          </Modal>
        </div>
         );
    }
   
}
const getModalStyle=()=> {
 

  return {
    top: `${50}%`,
    left: `${50}%`,
    transform: `translate(-${50}%, -${50}%)`,
  };
}
const body = (
  <div style={getModalStyle} >
    <StudentForm edit={true} />
  </div>
);
export default Modal;