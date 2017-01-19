import React from "react"

import Loading from "../commons/loading"
import {Table, TableBody, TableHeader, TableHeaderColumn, TableRow, TableRowColumn} from 'material-ui/Table';

export default class CustomerList extends React.Component{
  constructor(props) {
    super(props)    
  }
    
  render() {
            
    return (
        <div style={{position: 'relative'}}>
            {this.props.customers.length === 0 && this.props.isLoading &&
            <Loading />  
            }     
            <Table>
                <TableHeader displaySelectAll={false}>
                    <TableRow className={"removeFirstTh"}>
                        <TableHeaderColumn>Name</TableHeaderColumn>
                        <TableHeaderColumn>Email</TableHeaderColumn>
                        <TableHeaderColumn>Phone Number</TableHeaderColumn>
                        <TableHeaderColumn>Creation Date</TableHeaderColumn>
                        <TableHeaderColumn>Address</TableHeaderColumn>
                        <TableHeaderColumn>Insurance Types</TableHeaderColumn>
                    </TableRow>
                </TableHeader>
                <TableBody displayRowCheckbox={false}>                
                   {this.props.customers.map( (row, index) => (
                    <TableRow key={row.CustomerId}>                        
                        <TableRowColumn title={row.Name}>{row.Name}</TableRowColumn>
                        <TableRowColumn title={row.Email}>{row.Email}</TableRowColumn>
                        <TableRowColumn title={row.PhoneNumber}>{row.PhoneNumber}</TableRowColumn>
                        <TableRowColumn title={row.CreationDate}>{row.CreationDate}</TableRowColumn>
                        <TableRowColumn title={row.Address}>{row.Address}</TableRowColumn>
                        <TableRowColumn>{
                            row.InsuranceTypes.map((type) => {
                            return <div key={"type" + type.InsuranceTypeId}>{type.Name}</div>
                        })
                    }
                        </TableRowColumn>
                    </TableRow>
                    ))}           
                </TableBody>
            </Table>  
        </div>    
    )
  }

}