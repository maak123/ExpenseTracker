import React, { useState, useEffect } from "react";
// @material-ui/core components
import withStyles from "@material-ui/core/styles/withStyles";

// core components
import GridItem from "../../components/Grid/GridItem";
import GridContainer from "../../components/Grid/GridContainer";
import Table from "../../components/Table/Table";
import Card from "../../components/Card/Card";
import CardHeader from "../../components/Card/CardHeader";
import CardBody from "../../components/Card/CardBody";
import { getFullUrl, getHeader } from "../../helpers/callApi.helpers";
import TransactionsTable from "./TransactionsTable";
import Input from "@material-ui/core/Input";
import { createStyles } from "@material-ui/core";
import AddIcon from "@material-ui/icons/Add";
import Button from "@material-ui/core/Button";
import TextField from "@material-ui/core/TextField";
import Dialog from "@material-ui/core/Dialog";
import DialogActions from "@material-ui/core/DialogActions";
import DialogContent from "@material-ui/core/DialogContent";
import DialogContentText from "@material-ui/core/DialogContentText";
import DialogTitle from "@material-ui/core/DialogTitle";
import InputLabel from "@material-ui/core/InputLabel";
import InputAdornment from "@material-ui/core/InputAdornment";
import FormControl from "@material-ui/core/FormControl";
import Snackbar from "../../components/Snackbar/Snackbar";
import AddAlert from "@material-ui/icons/AddAlert";
import Select from "@material-ui/core/Select";
import MenuItem from "@material-ui/core/MenuItem";
import {userId} from '../../config/core.config'

const styles = createStyles({
  cardCategoryWhite: {
    "&,& a,& a:hover,& a:focus": {
      color: "rgba(255,255,255,.62)",
      margin: "0",
      fontSize: "14px",
      marginTop: "0",
      marginBottom: "0",
    },
    "& a,& a:hover,& a:focus": {
      color: "#FFFFFF",
    },
  },
  cardTitleWhite: {
    color: "#FFFFFF",
    marginTop: "0px",
    minHeight: "auto",
    fontWeight: 300,
    fontFamily: "'Roboto', 'Helvetica', 'Arial', sans-serif",
    marginBottom: "3px",
    textDecoration: "none",
    "& small": {
      color: "#777",
      fontSize: "65%",
      fontWeight: 400,
      lineHeight: 1,
    },
  },
  rightAlign: {
    float: "right",
  },
  textFieldGap: {
    paddingBottom: "16px",
  },
});

interface Category {
  amount: string;
  title: string;
  id: Number;
}
function Transactions(props: any) {
  const { classes } = props;
  const [open, setOpen] = React.useState(false);
  const [openSnackbar, setOpenSnackbar] = React.useState(false);
  const [categories, setCategories] = useState([]);
  const [snackbarData, setSnackbarData] = React.useState({
    color: "success",
    message: "Category Added Successfully",
  });
  const [transactions, setTransactions] = useState([]);
  const [values, setValues] = React.useState({
    id: "",
    amount: "",
    title: "",
    categoryId: "",
    date: new Date(),
    note:'',
  });

  const handleChange = (prop: any) => (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setValues({ ...values, [prop]: event.target.value });
  };
  const handleSelectChange = (event: React.ChangeEvent<{ value: any }>) => {
    setValues({ ...values, categoryId: event.target.value });
  };
  const handleClickOpen = () => {
    setValues({
      id: "",
      amount: "",
      title: "",
      categoryId: "",
      date: new Date(),
      note:'',
    });
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const handleSubmit = () => {
    if (!values.id) {
      createTransaction();
    } else {
      updateCategory();
    }
  };

  const createTransaction = () => {
    const options = {
      method: "POST",
      headers: new Headers({ "content-type": "application/json" }),
      RequestMode: "cors",
      body: JSON.stringify({
        categoryId:+values.categoryId,
        userId:+userId,
        date:values.date,
        note: values.note,
        amount: +values.amount,
      }),
    };
    fetch(getFullUrl("Transactions"), options)
      .then((response: any) => {
        return response.json();
      })
      .then((data) => {
        setSnackbarData({
          color: "success",
          message: "Category Added Successfully",
        });
        showNotification();

        fetchTransactions();
        setOpen(false);
      })
      .catch((error) => {});
  };

  const updateCategory = () => {
    const options = {
      method: "PUT",
      headers: new Headers({ "content-type": "application/json" }),
      RequestMode: "cors",
      body: JSON.stringify({
        id:+values.id,
        categoryId:+values.categoryId,
        userId:+userId,
        date:new Date(values.date),
        note: values.note,
        amount: +values.amount,
      }),
    };
    fetch(getFullUrl("Transactions") + "/" + values.id, options)
      .then((response: any) => {
        if (response.ok) {
          return response.json();
        } else {
          throw new Error('Something went wrong');
        }
      })
      .then((data) => {
        console.log(data)
        setSnackbarData({
          color: "success",
          message: "Category Updated Successfully",
        });
        showNotification();
        fetchTransactions();
        setOpen(false);
      })
      .catch((error) => {
        setErrorMessage()
      });
  };

  const setErrorMessage=()=>{
    setSnackbarData({
      color: "danger",
      message: "Something Went Wrong",
    });
    showNotification();
  }

  const editButtonHandler = (currentValue: any) => {
    setValues({
      id: currentValue.id,
      amount: currentValue.amount,
      title: currentValue.title,
      categoryId: currentValue.category.id,
      date: currentValue.date.substring(0,10),
      note:currentValue.note,
    });
    setOpen(true);
  };

  const deleteButtonHandler = (currentValue: any) => {
    deleteTransaction(currentValue.id)
  };

  const deleteTransaction = (id:number) => {
    const options = {
      method: "DELETE",
      headers: new Headers({ "content-type": "application/json" }),
      RequestMode: "cors",
    };
    fetch(getFullUrl("Transactions") + "/" +id, options)
      .then((response: any) => {
        return response.json();
      })
      .then((data) => {
        setSnackbarData({
          color: "success",
          message: "Transaction Deleted Successfully",
        });
        showNotification();
        fetchTransactions();
      })
      .catch((error) => {
        console.log(error)
      });
  };


  const showNotification = () => {
    setOpenSnackbar(true);
    const alertTimeout = setTimeout(() => {
      setOpenSnackbar(false);
    }, 6000);
  };

  const fetchCategories = () => {
    const options = {
      method: "GET",
      headers: new Headers({ "content-type": "application/json" }),
      RequestMode: "cors",
    };
    fetch(getFullUrl("Categories"), options)
      .then((response: any) => {
        return response.json();
      })
      .then((data) => {
        console.log(data);
        const tableCategories = data.map((item: any) => {
          return {
            ...item,
            amount: item.amount.toFixed(2),
          };
        });

        setCategories(tableCategories);
      });
  };

  const fetchTransactions = () => {
    const options = {
      method: "GET",
      headers: new Headers({ "content-type": "application/json" }),
      RequestMode: "cors",
    };
    fetch(getFullUrl("Transactions") +'/'+ userId, options)
      .then((response: any) => {
        return response.json();
      })
      .then((data) => {
        console.log(data)
        const tableTransactions =data.map((item: any)=>{
         return{
          ...item,
          amount : item.amount.toFixed(2)
         }
        })
        setTransactions(tableTransactions)
      });
  };

  useEffect(() => {
    fetchTransactions();
    fetchCategories();
  }, []);

  return (
    <GridContainer>
      <GridItem xs={12} sm={12} md={12}>
        <Card>
          <CardHeader color="primary">
            <h4 className={classes.cardTitleWhite}>Transaction List</h4>
            <p className={classes.cardCategoryWhite}>
              Here is the list of Transactions available
            </p>
          </CardHeader>
          <CardBody>
            <div className={classes.floatRight}>
              <Button
                variant="contained"
                color="primary"
                onClick={handleClickOpen}
                className={classes.button + " " + classes.rightAlign}
              >
                Add Transaction
              </Button>
            </div>
            <TransactionsTable
              tableHeaderColor="primary"
              tableData={transactions}
              editButtonHandler={editButtonHandler}
              deleteButtonHandler={deleteButtonHandler}
            />
          </CardBody>
        </Card>
      </GridItem>
      <Dialog
        open={open}
        onClose={handleClose}
        aria-labelledby="form-dialog-title"
      >
        <DialogTitle id="form-dialog-title">{values.id ? 'Update Transaction':'Add Transaction'}</DialogTitle>
        <DialogContent>
          <div className="mt-2">
            <FormControl className={classes.textFieldGap} fullWidth>
              <InputLabel id="demo-simple-select-label">Category</InputLabel>
              <Select
                fullWidth
                id="demo-simple-select"
                value={values.categoryId}
                onChange={handleSelectChange}
              >
                {categories.map((prop: any) => {
                  return <MenuItem value={prop.id} key={prop.id}>{prop.title}</MenuItem>;
                })}
              </Select>
            </FormControl>
            <FormControl fullWidth className={classes.textFieldGap}>
              <InputLabel shrink={true} htmlFor="standard-adornment-date">
                Date
              </InputLabel>
              <Input
                id="standard-adornment-date"
                value={values.date}
                onChange={handleChange("date")}
                type="date"
        
              />
            </FormControl>
            <FormControl fullWidth className={classes.textFieldGap}>
              <InputLabel htmlFor="standard-adornment-amount">
                Amount
              </InputLabel>
              <Input
                id="standard-adornment-amount"
                value={values.amount}
                onChange={handleChange("amount")}
                startAdornment={
                  <InputAdornment position="start">$</InputAdornment>
                }
              />
            </FormControl>

            <FormControl fullWidth className={classes.textFieldGap}>
              <InputLabel htmlFor="standard-adornment-amount">
                Note
              </InputLabel>
              <Input
                id="standard-adornment-amount"
                value={values.note}
                onChange={handleChange("note")}
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
        closeNotification={() => setOpenSnackbar(false)}
        close={true}
      />
    </GridContainer>
  );
}

export default withStyles(styles)(Transactions);
