import React from "react"

import {Table, TableBody, TableHeader, TableHeaderColumn, TableRow, TableRowColumn} from 'material-ui/Table';

export default class CustomerList extends React.Component{
  constructor(props) {
    super(props)    
  }
    
  render() {
            
    return (
        <Table>
            <TableHeader>
                <TableRow>
                    <TableHeaderColumn>Name</TableHeaderColumn>
                    <TableHeaderColumn>Email</TableHeaderColumn>
                    <TableHeaderColumn>Phone Number</TableHeaderColumn>
                    <TableHeaderColumn>Creation Date</TableHeaderColumn>
                    <TableHeaderColumn>Adress</TableHeaderColumn>
                    <TableHeaderColumn>Insurance Types</TableHeaderColumn>
                </TableRow>
            </TableHeader>
            <TableBody>
                {this.props.customers.map( (row, index) => (
                    <TableRow key={row.CustomerId}>
                        <TableRowColumn>{row.Name}</TableRowColumn>
                        <TableRowColumn>{row.Email}</TableRowColumn>
                        <TableRowColumn>{row.PhoneNumber}</TableRowColumn>
                        <TableRowColumn>{row.CreationDate}</TableRowColumn>
                        <TableRowColumn>{row.Adress}</TableRowColumn>
                        <TableRowColumn>{
                            row.InsuranceTypes.map((type) => {
                            return <div>{type.Name}</div>
                        })
                    }
                        </TableRowColumn>
                    </TableRow>
                ))}                
            </TableBody>
        </Table>      
    )
  }

}