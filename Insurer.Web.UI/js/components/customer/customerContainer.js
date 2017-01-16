import React from "react"
import axios from "axios"
import { browserHistory, Router, Route, Link, withRouter } from 'react-router'

import FlatButton from 'material-ui/FlatButton';

import auth from "../../help/auth"
import {config} from "../../help/config"
import CustomerList from "./customerList"
import CustomerAdd from "./customerAdd"
import ErrorMessage from "../commons/errorMessage"

export default class CustomerContainer extends React.Component{
  constructor(props) {
    super(props)
    this.state = { isLoading: false, customers: [], addCustomer: false, enableAddCustomer: false, insuranceTypes: [] }
    this.loadCustomers = this.loadCustomers.bind(this)
    this.loadInsuranceTypes = this.loadInsuranceTypes.bind(this)
    this.goToAddCustomer = this.goToAddCustomer.bind(this)
    this.isAdmin = this.isAdmin.bind(this)
    this.createCustomer = this.createCustomer.bind(this)
  }

  createCustomer(customer) {
    let url = config.customer
    let that = this
    this.setState(({isLoading : true}))
    
    
    this.customerRequest = 
      axios
        .post(url, customer)
        .then((result) => {    
          that.setState({  
            addCustomer : false                      
          });
          that.loadCustomers();
        })
        .catch((error)=>{
          that.setState({
            isLoading : false
          });
        })
  }
  
  loadCustomers() {
    let url = config.customer
    let that = this
    this.setState(({isLoading : true}))
    this.customerRequest = 
      axios
        .get(url)
        .then((result) => {    
          that.setState({  
            isLoading : false,
            customers: result.data            
          });
        })
        .catch((error)=>{
          that.setState({
            isLoading : false
          });
        })
  }
  
  loadInsuranceTypes() {
    let url = config.insuranceTypes
    let that = this
    this.insuranceRequest = 
      axios
        .get(url)
        .then((result) => {    
          that.setState({
            insuranceTypes: result.data
          });
        })
  }
  
  isAdmin(){
    let url = config.isAdmin
    let that = this
    this.setState(({enableAddCustomer : false}))
    this.userRequest = 
      axios
        .get(url)
        .then(() => {    
          that.setState({
            enableAddCustomer : true
          });
        })
  }
  
  goToAddCustomer(){
    this.setState({
            addCustomer : true
          });
  }

  componentWillMount() {
    
  }
  
  componentDidMount() {
    this.isAdmin()
    this.loadCustomers()
    this.loadInsuranceTypes()
  }

  componentWillUnmount() {
    if(this.customerRequest && this.customerRequest.abort)
      this.customerRequest.abort()
      
    if(this.userRequest && this.userRequest.abort)
      this.userRequest.abort()
          
    if(this.insuranceRequest && this.insuranceRequest.abort)
      this.insuranceRequest.abort()
  }

  render() {
        
    return (
      <div>
        <h2>
          Customer
        </h2>
        
        {!this.state.addCustomer ? 
          <div>
            <div>
              <FlatButton label="Add Customer" onClick={this.goToAddCustomer} disabled={!this.state.enableAddCustomer} />
            </div>
            <CustomerList customers={this.state.customers} />
          </div>
         :
         (this.state.insuranceTypes.length > 0 ?
          <CustomerAdd insuranceTypes={this.state.insuranceTypes} handleCreateCustomer={this.createCustomer}  />
          :
          <ErrorMessage message={"Error to load insurance type"} /> 
         )
        }
      </div>
      
    )
  }

}