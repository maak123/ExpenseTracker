import React,{useState,useEffect} from 'react';
// @material-ui/core components
import withStyles from '@material-ui/core/styles/withStyles';

// core components
import GridItem from '../../components/Grid/GridItem';
import GridContainer from '../../components/Grid/GridContainer';
import Table from '../../components/Table/Table';
import Card from '../../components/Card/Card';
import CardHeader from '../../components/Card/CardHeader';
import CardBody from '../../components/Card/CardBody';
import {getFullUrl ,getHeader} from '../../helpers/callApi.helpers';
import CategoriesTable from './CategoriesTable';
import Input from '@material-ui/core/Input';
import { createStyles } from '@material-ui/core';
import AddIcon from '@material-ui/icons/Add';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import InputLabel from '@material-ui/core/InputLabel';
import InputAdornment from '@material-ui/core/InputAdornment';
import FormControl from '@material-ui/core/FormControl';
import Snackbar from '../../components/Snackbar/Snackbar';
import AddAlert from '@material-ui/icons/AddAlert';


const styles = createStyles({
  cardCategoryWhite: {
    '&,& a,& a:hover,& a:focus': {
      color: 'rgba(255,255,255,.62)',
      margin: '0',
      fontSize: '14px',
      marginTop: '0',
      marginBottom: '0'
    },
    '& a,& a:hover,& a:focus': {
      color: '#FFFFFF'
    }
  },
  cardTitleWhite: {
    color: '#FFFFFF',
    marginTop: '0px',
    minHeight: 'auto',
    fontWeight: 300,
    fontFamily: '\'Roboto\', \'Helvetica\', \'Arial\', sans-serif',
    marginBottom: '3px',
    textDecoration: 'none',
    '& small': {
      color: '#777',
      fontSize: '65%',
      fontWeight: 400,
      lineHeight: 1
    }
  },
  rightAlign: {
    float:'right'
  },
  textFieldGap: {
    paddingBottom:'16px'
  },
});


interface Category {
  amount: string;
  title: string;
  id:Number
}
function Categories(props: any) {
  const { classes } = props;
  const [open, setOpen] = React.useState(false);
  const [openSnackbar, setOpenSnackbar] = React.useState(false);
  const [snackbarData, setSnackbarData] = React.useState({
    color:'success',
    message: 'Category Added Successfully'
  });
  const [categories,setCategories] =useState([])
  const [values, setValues] = React.useState({
    id:'',
    amount: '',
    title: '',
  });

  const handleChange = (prop: any) => (event: React.ChangeEvent<HTMLInputElement>) => {
    setValues({ ...values, [prop]: event.target.value });
  };
  const handleClickOpen = () => {
    setValues({
      id:'',
      amount: '',
      title: '',
    })
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };



  const handleSubmit = () => {

    if(!values.id){
      createCategory()
    }else {
      updateCategory()
    }

  };

  const createCategory =()=>{
    const options = {
      method: 'POST',
      headers: new Headers({'content-type': 'application/json'}),
      RequestMode: 'cors',
      body:JSON.stringify({
        title:values.title,
        amount:+values.amount
      })
  }
    fetch(getFullUrl('Categories'),options).then(
      (response:any) =>{
        return response.json();
      } 
    ).then(data =>{
      setSnackbarData({
        color:'success',
        message:'Category Added Successfully'
      })
      showNotification()

      fetchCategories()
      setOpen(false);
    }).catch(error=>{

    })
  }

  const updateCategory =()=>{
    const options = {
      method: 'PUT',
      headers: new Headers({'content-type': 'application/json'}),
      RequestMode: 'cors',
      body:JSON.stringify({
        id:values.id,
        title:values.title,
        amount:+values.amount
      })
  }
    fetch(getFullUrl('Categories') + '/' + values.id ,options).then(
      (response:any) =>{
        return response.json();
      } 
    ).then(data =>{
      setSnackbarData({
        color:'success',
        message:'Category Updated Successfully'
      })
      showNotification()
      fetchCategories()
      setOpen(false);
    }).catch(error=>{

    })
  }

  const editButtonHandler =(currentValue:any)=>{
    setValues({
      id: currentValue.id,
      amount: currentValue.amount,
      title: currentValue.title,
    })
    setOpen(true);
  }

 const  showNotification =() => {
    setOpenSnackbar( true);
    const alertTimeout = setTimeout(
      () => {
        setOpenSnackbar(false);
      },
      6000
    );
  }


  const fetchCategories=()=>{
    const options = {
      method: 'GET',
      headers: new Headers({'content-type': 'application/json'}),
      RequestMode: 'cors'
 
  }
    fetch(getFullUrl('Categories'),options).then(
      (response:any) =>{
        return response.json();
       
      } 
    ).then(data =>{
      console.log(data)
      const tableCategories =data.map((item: any)=>{
       return{
        ...item,
        amount : item.amount.toFixed(2)
       }
      })
  
      setCategories(tableCategories)
    })
  }

  useEffect(()=>{
    fetchCategories()
 },[]);

  return (
    <GridContainer>
      <GridItem xs={12} sm={12} md={12}>
        <Card>
          <CardHeader color="primary">
            <h4 className={classes.cardTitleWhite}>Category List</h4>
            <p className={classes.cardCategoryWhite}>
              Here is the list of categories available
            </p>
          </CardHeader>
          <CardBody>
          <div className={classes.floatRight}>
      <Button
        variant="contained"
        color="primary"
        onClick={handleClickOpen}
        className={classes.button +' '+classes.rightAlign} 
      >
        Add Category
      </Button>
      </div>
            <CategoriesTable
              tableHeaderColor="primary"
              tableData={categories}
              editButtonHandler={editButtonHandler}
            />
          </CardBody>
        </Card>
      </GridItem>
      <Dialog open={open} onClose={handleClose} aria-labelledby="form-dialog-title">
        <DialogTitle id="form-dialog-title">Add Category</DialogTitle>
        <DialogContent>
 
          <div className="mt-2">
          <FormControl fullWidth className={classes.textFieldGap}>
          <InputLabel htmlFor="standard-name">Name</InputLabel>
          <Input
            id="standard-name"
            value={values.title}
            onChange={handleChange('title')}
          />
        </FormControl>
       <FormControl fullWidth className={classes.textFieldGap}>
          <InputLabel htmlFor="standard-adornment-amount">Amount</InputLabel>
          <Input
            id="standard-adornment-amount"
            value={values.amount}
            onChange={handleChange('amount')}
            startAdornment={<InputAdornment position="start">$</InputAdornment>}
          />
        </FormControl>
          </div>
         

        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose} color="primary">
            Cancel
          </Button>
          <Button onClick={handleSubmit} color="primary">
            Confirm
          </Button>
        </DialogActions>

      </Dialog>
      <Snackbar
                    place="tr"
                    color={snackbarData.color}
                    icon={AddAlert}
                    message={snackbarData.message}
                    open={openSnackbar}
                    closeNotification={() => setOpenSnackbar( false )}
                    close={true}
                  />
    </GridContainer>
  );
}

export default withStyles(styles)(Categories);
