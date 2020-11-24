import React from "react";
import DeleteButton from "../Controls/DeleteButton";
import { withStyles, makeStyles } from "@material-ui/core/styles";
import Table from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableContainer from "@material-ui/core/TableContainer";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import Paper from "@material-ui/core/Paper";
import SimpleModal from '../Controls/Modal';
import BootstrapModal from '../Controls/BootstrapModal';


const StyledTableCell = withStyles((theme) => ({
  head: {
    backgroundColor: theme.palette.common.black,
    color: theme.palette.common.white,
  },
  body: {
    fontSize: 14,
  },
}))(TableCell);

const StyledTableRow = withStyles((theme) => ({
  root: {
    "&:nth-of-type(odd)": {
      backgroundColor: theme.palette.action.hover,
    },
  },
}))(TableRow);

const useStyles = makeStyles({
  table: {
    minWidth: 200,
  },
});

const StudentTable = (props) => {
  const classes = useStyles();
  console.log("StudentTable rendered called");

  const handleAddStudent = () => {
    this.obj = "/student/addstudent";
  };

  return (
    <div className="container">
      <BootstrapModal  handleRefresh={props.handleRefresh}/>
      <TableContainer component={Paper}>
        <Table className={classes.table} aria-label="simple table">
          <TableHead className={StyledTableCell.head}>
            <StyledTableRow>
              <StyledTableCell>Name</StyledTableCell>
              <StyledTableCell>Fname</StyledTableCell>
              <StyledTableCell>Email</StyledTableCell>
              <StyledTableCell>Dob</StyledTableCell>
              <StyledTableCell>Phone</StyledTableCell>
              <StyledTableCell>CoursesCount</StyledTableCell>
              <StyledTableCell>Action</StyledTableCell>
            </StyledTableRow>
          </TableHead>
          <TableBody>
            {props.students.map((row) => (
              <StyledTableRow key={row.Id}>
                <StyledTableCell component="th" scope="row">
                  {row.Name}
                </StyledTableCell>
                <StyledTableCell>{row.FName}</StyledTableCell>
                <StyledTableCell>{row.Email}</StyledTableCell>
                <StyledTableCell>{row.Dob}</StyledTableCell>
                <StyledTableCell>{row.Phone}</StyledTableCell>
                <StyledTableCell>{row.CoursesCount}</StyledTableCell>
                <StyledTableCell>
                 <SimpleModal   Id={row.Id} handleRefresh={props.handleRefresh}/>
                  <DeleteButton handleDelete={props.handleDelete}  Id={row.Id} />
                </StyledTableCell>
              </StyledTableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </div>
  );
};
export default StudentTable;
