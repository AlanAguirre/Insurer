import React from "react"
import axios from "axios"
import { browserHistory, Router, Route, Link, withRouter } from 'react-router'

import FlatButton from 'material-ui/FlatButton';

import auth from "../../help/auth"
import {config} from "../../help/config"
import ErrorMessage from '../commons/errorMessage'

export class CustomerAddContainer extends React.Component{
  constructor(props) {
    super(props)
    this.state = { isLoading: false, enableAddCustomer: false }    
    this.isAdmin = this.isAdmin.bind(this)
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

  componentWillMount() {
    
  }
  
  componentDidMount() {
    this.isAdmin()
  }

  componentWillUnmount() {      
    if(this.userRequest && this.userRequest.abort)
      this.userRequest.abort()
  }

  render() {
        
    return (
      <div>
        <h2>
          Customer
        </h2>
        
        {this.state.enableAddCustomer ? (
            <div></div>
          ) : (
            <ErrorMessage message={'Incorrect email or password.'} />
          )}
      </div>
      
    )
  }

}