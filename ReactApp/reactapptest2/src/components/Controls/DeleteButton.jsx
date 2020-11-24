import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import IconButton from '@material-ui/core/IconButton';
import DeleteIcon from '@material-ui/icons/Delete';

const useStyles = makeStyles((theme) => ({
  margin: {
    margin: theme.spacing(1),
  },
  extendedIcon: {
    marginRight: theme.spacing(1),
  },
}));

const DeleteButton=(props)=> {
  const classes = useStyles();
  console.log('Delete button rendered called');
  return (
      <IconButton aria-label="delete" className={classes.margin} onClick={props.handleDelete.bind(this,props.Id)}>
        <DeleteIcon fontSize="large" />
      </IconButton>
  );
}
export default DeleteButton;
