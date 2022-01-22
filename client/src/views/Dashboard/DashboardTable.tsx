import React from "react";
// import PropTypes from 'prop-types';
// @material-ui/core components
import withStyles from "@material-ui/core/styles/withStyles";
import Table from "@material-ui/core/Table";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";

// core components
import tableStyle from "../../assets/jss/material-dashboard-react/components/tableStyle";

function DashboardTable({ ...props }: any) {
  const { classes, tableHead, tableData, tableHeaderColor } = props;
  return (
    <div className={classes.tableResponsive}>
      <Table className={classes.table}>

        <TableHead className={classes[tableHeaderColor + "TableHeader"]}>
          <TableRow>
            <TableCell key={1}
              className={classes.tableCell + " " + classes.tableHeadCell}
            >
              No.
            </TableCell>
            <TableCell key={2}
              className={classes.tableCell + " " + classes.tableHeadCell}
            >
              Category
            </TableCell>
            <TableCell key={3}
              className={classes.tableCell + " " + classes.tableHeadCell}
            >
              limit
            </TableCell>
            <TableCell key={4}
              className={classes.tableCell + " " + classes.tableHeadCell}
            >
              Total
            </TableCell>

          </TableRow>
        </TableHead>

        <TableBody>
          {tableData.map((prop: any, key: any) => {
            return (
              <TableRow key={key}>
                <TableCell className={classes.tableCell} key={1}>
                  {key + 1}
                </TableCell>
                <TableCell className={classes.tableCell} key={2}>
                  {prop.title}
                </TableCell>
                <TableCell className={classes.tableCell} key={3}>
                  {prop.amount}
                </TableCell>
                <TableCell className={classes.tableCell} key={4}>
                  {prop.transactionsTotal}
                </TableCell>

              </TableRow>
            );
          })}
        </TableBody>
      </Table>
    </div>
  );
}

DashboardTable.defaultProps = {
  tableHeaderColor: "gray",
};

// CustomTable.propTypes = {
//   classes: PropTypes.object.isRequired,
//   tableHeaderColor: PropTypes.oneOf([
//     'warning',
//     'primary',
//     'danger',
//     'success',
//     'info',
//     'rose',
//     'gray'
//   ]),
//   tableHead: PropTypes.arrayOf(PropTypes.string),
//   tableData: PropTypes.arrayOf(PropTypes.arrayOf(PropTypes.string))
// };

export default withStyles(tableStyle)(DashboardTable);
