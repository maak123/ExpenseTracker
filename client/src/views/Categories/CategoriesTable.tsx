import React from "react";
// import PropTypes from 'prop-types';
// @material-ui/core components
import withStyles from "@material-ui/core/styles/withStyles";
import Table from "@material-ui/core/Table";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import IconButton from '@material-ui/core/IconButton';
import Icon from '@material-ui/core/Icon';
import EditIcon from '@material-ui/icons/Edit';
// core components
import tableStyle from "../../assets/jss/material-dashboard-react/components/tableStyle";

function CategoriesTable({ ...props }: any) {
  const { classes, tableHead, tableData, tableHeaderColor } = props;
  return (
    <div className={classes.tableResponsive}>
      <Table className={classes.table}>
 
          <TableHead className={classes[tableHeaderColor + "TableHeader"]}>
            <TableRow>
              <TableCell   key={1}
                className={classes.tableCell + " " + classes.tableHeadCell}
              >
                No
              </TableCell>
              <TableCell   key={2}
                className={classes.tableCell + " " + classes.tableHeadCell}
              >
                Name
              </TableCell>
              <TableCell   key={3}
                className={classes.tableCell + " " + classes.tableHeadCell}
              >
                Amount
              </TableCell>
              <TableCell  key={4}
                className={classes.tableCell + " " + classes.tableHeadCell}
              >
                Actions
              </TableCell>
            </TableRow>
          </TableHead>
    
        <TableBody>
        {tableData.map((prop: any, key: any) => {
            return (
              <TableRow  key={key}>
                    <TableCell className={classes.tableCell} key={1}>
                     {prop.id}
                    </TableCell>
                    <TableCell className={classes.tableCell}  key={2}>
                    {prop.title}
                    </TableCell>
                    <TableCell className={classes.tableCell}  key={3}>
                    {prop.amount}
                    </TableCell>
                    <TableCell className={classes.tableCell} key={4}>
                    <IconButton onClick={(e) =>props.editButtonHandler(prop)}>
                    <EditIcon color="primary" />
                    </IconButton>
                    </TableCell>
              </TableRow>
             );
            })}
        </TableBody>
      </Table>
    </div>
  );
}

CategoriesTable.defaultProps = {
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

export default withStyles(tableStyle)(CategoriesTable);
