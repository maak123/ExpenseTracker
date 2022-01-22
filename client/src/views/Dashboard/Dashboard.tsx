import React, {useEffect,useState} from 'react';
// react plugin for creating charts
import ChartistGraph from 'react-chartist';
// @material-ui/core
import withStyles from '@material-ui/core/styles/withStyles';
import Icon from '@material-ui/core/Icon';


// @material-ui/icons
import Store from '@material-ui/icons/Store';
import DateRange from '@material-ui/icons/DateRange';
import LocalOffer from '@material-ui/icons/LocalOffer';
import Update from '@material-ui/icons/Update';
import Accessibility from '@material-ui/icons/Accessibility';

// core components
import GridItem from '../../components/Grid/GridItem';
import GridContainer from '../../components/Grid/GridContainer';
import Table from '../../components/Table/Table';
import Card from '../../components/Card/Card';
import CardHeader from '../../components/Card/CardHeader';
import CardIcon from '../../components/Card/CardIcon';
import CardBody from '../../components/Card/CardBody';
import CardFooter from '../../components/Card/CardFooter';
import {userId} from '../../config/core.config'
import { getFullUrl, getHeader } from "../../helpers/callApi.helpers";
import DashboardTable from "./DashboardTable"

import dashboardStyle from '../../assets/jss/material-dashboard-react/views/dashboardStyle';

interface Props {
  classes: any;
}

interface State {
  value: number;
}

function Dashboard (props: any)  {
  const { classes } = props;
  const [categories, setCategories] = useState([]);
  const [totalExpense, settotalExpense] = useState(0);
  const [totalBudget, settotalBudget] = useState(20000);
  const fetchCategoriesForUser = () => {
    const options = {
      method: "GET",
      headers: new Headers({ "content-type": "application/json" }),
      RequestMode: "cors",
    };
    fetch(getFullUrl("Categories/GetUserCategoryDetails") +'/'+ userId, options)
      .then((response: any) => {
        return response.json();
      })
      .then((data) => {
        console.log(data)
        const tableTransactions =data.map((item: any)=>{
         return{
          ...item,
          amount : item.amount.toFixed(2),
          transactionsTotal:item.transactionsTotal.toFixed(2)
         }
        })

        settotalExpense((tableTransactions.reduce((n, {transactionsTotal}) => n + (+transactionsTotal), 0)).toFixed(2))
        setCategories(tableTransactions)
      });
  };

  useEffect(() => {
    fetchCategoriesForUser()
  }, []);







    return (
      <div>
        <GridContainer>
          <GridItem xs={12} sm={6} md={4}>
            <Card>
              <CardHeader color="success" stats={true} icon={true}>
                <CardIcon color="success">
                  <Store />
                </CardIcon>
                <p className={classes.cardCategory}>Budget</p>
                <h3 className={classes.cardTitle}>{totalBudget}</h3>
              </CardHeader>
              <CardFooter stats={true}>
                <div className={classes.stats}>
                  <DateRange />
                  Monthly
                </div>
              </CardFooter>
            </Card>
          </GridItem>
          <GridItem xs={12} sm={6} md={4}>
            <Card>
              <CardHeader color="warning" stats={true} icon={true}>
                <CardIcon color="warning">
                  <Icon>call_received</Icon>
                </CardIcon>
                <p className={classes.cardCategory}>Categories</p>
                <h3 className={classes.cardTitle}>{categories.length}</h3>
              </CardHeader>
              <CardFooter stats={true}>
                <div className={classes.stats}>
                  <LocalOffer />
                  Tracked from Expense Tracker
                </div>
              </CardFooter>
            </Card>
          </GridItem>
          <GridItem xs={12} sm={6} md={4}>
            <Card>
              <CardHeader color="info" stats={true} icon={true}>
                <CardIcon color="info">
                  <Accessibility />
                </CardIcon>
                <p className={classes.cardCategory}>Your wallet</p>
                <h3 className={classes.cardTitle}>{totalBudget-totalExpense}</h3>
              </CardHeader>
              <CardFooter stats={true}>
                <div className={classes.stats}>
                  <Update />
                  Just Updated
                </div>
              </CardFooter>
            </Card>
          </GridItem>


          <GridItem xs={12} sm={12} md={12}>
            <Card>
              <CardHeader color="warning">
                <h4 className={classes.cardTitleWhite}>Transaction Stats</h4>
                <p className={classes.cardCategoryWhite}>
         
                </p>
              </CardHeader>
              <CardBody>
                <DashboardTable
                  tableData={
                    categories
                  }
                />
              </CardBody>
            </Card>
          </GridItem>
        </GridContainer>
      </div>
    );
  }


// Dashboard.propTypes = {
//   classes: PropTypes.object.isRequired
// };

export default withStyles(dashboardStyle)(Dashboard);
