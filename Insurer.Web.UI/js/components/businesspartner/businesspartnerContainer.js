import React from "react"
import axios from "axios"
import { browserHistory, Router, Route, Link, withRouter } from 'react-router'

import FlatButton from 'material-ui/FlatButton';

import auth from "../../help/auth"
import {config} from "../../help/config"
import BusinessPartnerList from "./businesspartnerList"
import ErrorMessage from "../commons/errorMessage"

export default class BusinessPartnerContainer extends React.Component{
  constructor(props) {
    super(props)
    this.state = { isLoading: false, companiesLog: [] }
    this.loadCompaniesLog = this.loadCompaniesLog.bind(this)
  }
  
  loadCompaniesLog() {
    let url = config.companiesLog
    let that = this
    this.setState(({isLoading : true}))
    this.businessPartnerRequest = 
      axios
        .get(url)
        .then((result) => {    
          that.setState({  
            isLoading : false,
            companiesLog: result.data.map((item)=>{
                const total = item.Car + item.Motorcycle + item.House + item.Farm
                return {
                    Company: item.Company,
                    Car: ((item.Car * 100)/total),
                    Motorcycle: ((item.Motorcycle * 100)/total),
                    House: ((item.House * 100)/total),
                    Farm: ((item.Farm * 100)/total),
                    Total: total
                }
            })           
          });
        })
        .catch((error)=>{
          that.setState({
            isLoading : false
          });
        })
  }
  
  

  componentWillMount() {
    
  }
  
  componentDidMount() {
    this.loadCompaniesLog()
  }

  componentWillUnmount() {
    if(this.businessPartnerRequest && this.businessPartnerRequest.abort)
      this.businessPartnerRequest.abort()
      
  }

  render() {
        
    return (
      <div style={{paddingRight: 50, paddingLeft: 50}}>
        <h2>
          Business Partner - Services consumed
        </h2>
        
       <BusinessPartnerList companiesLog={this.state.companiesLog} isLoading={this.state.isLoading} />
      </div>
      
    )
  }

}