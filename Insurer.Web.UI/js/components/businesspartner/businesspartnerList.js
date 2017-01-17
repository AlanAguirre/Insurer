import React from "react"

import Loading from "../commons/loading"
import {Table, TableBody, TableHeader, TableHeaderColumn, TableRow, TableRowColumn} from 'material-ui/Table';

export default class BusinessPartnerList extends React.Component{
  constructor(props) {
    super(props)    
  }
    
  render() {
    const columnStyle = {textAlign: 'right'}
    return (
        <div style={{position: 'relative'}}>
            {this.props.companiesLog.length === 0 && this.props.isLoading &&
            <Loading />  
            }     
            <Table>
                <TableHeader displaySelectAll={false}>
                    <TableRow className={"removeFirstTh"}>
                        <TableHeaderColumn>Company</TableHeaderColumn>
                        <TableHeaderColumn>Car</TableHeaderColumn>
                        <TableHeaderColumn>Motorcycle</TableHeaderColumn>
                        <TableHeaderColumn>House</TableHeaderColumn>
                        <TableHeaderColumn>Farm</TableHeaderColumn>
                        <TableHeaderColumn>Total</TableHeaderColumn>
                    </TableRow>
                </TableHeader>
                <TableBody displayRowCheckbox={false}>                
                   {this.props.companiesLog.map( (row, index) => (
                    <TableRow key={row.Company}>
                        <TableRowColumn>{row.Company}</TableRowColumn>
                        <TableRowColumn style={columnStyle}>{row.Car + "%"}</TableRowColumn>
                        <TableRowColumn style={columnStyle}>{row.Motorcycle + "%"}</TableRowColumn>
                        <TableRowColumn style={columnStyle}>{row.House + "%"}</TableRowColumn>
                        <TableRowColumn style={columnStyle}>{row.Farm + "%"}</TableRowColumn>
                        <TableRowColumn style={columnStyle}>{row.Total}</TableRowColumn>                        
                    </TableRow>
                    ))}           
                </TableBody>
            </Table>  
        </div>    
    )
  }

}