import React from "react"
import auth from "../help/auth"
import { browserHistory, Router, Route, Link, withRouter } from 'react-router'


import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import AppBar from 'material-ui/AppBar';
import IconButton from 'material-ui/IconButton';
import IconMenu from 'material-ui/IconMenu';
import MenuItem from 'material-ui/MenuItem';
import FlatButton from 'material-ui/FlatButton';
import Toggle from 'material-ui/Toggle';
import MoreVertIcon from 'material-ui/svg-icons/navigation/more-vert';
import NavigationClose from 'material-ui/svg-icons/navigation/close';
import {Toolbar, ToolbarGroup, ToolbarSeparator, ToolbarTitle} from 'material-ui/Toolbar';

import {Login} from "./login"

export default class App extends React.Component{
  constructor(props) {
    super(props)
    this.state = { loggedIn: auth.loggedIn() }
    this.updateAuth = this.updateAuth.bind(this)
  }

  updateAuth(loggedIn) {
    this.setState({
      loggedIn
    })
  }

  componentWillMount() {
    auth.onChange = this.updateAuth
    auth.login()
  }

componentDidMount(){
  if(!this.state.loggedIn)
    browserHistory.push('/login')
}


  render() {
    
    const Logged = (props) => (
      <IconMenu
        {...props}
        iconButtonElement={
          <IconButton><MoreVertIcon /></IconButton>
        }
        targetOrigin={{horizontal: 'right', vertical: 'top'}}
        anchorOrigin={{horizontal: 'right', vertical: 'top'}}
      >
        
       <MenuItem primaryText="Sign out" onClick={()=>{
         auth.logout()
         browserHistory.push('/login')
       }} />
      </IconMenu>
    )
        
    return (
      <MuiThemeProvider>        
        <div>
          <AppBar
            title="Insurer Portal"
            iconElementLeft={null}
            iconElementRight={this.state.loggedIn ? <Logged /> : null}
          />
          {this.state.loggedIn ? (
            <Toolbar>
              <ToolbarGroup>
                <Link to="/customers">Customers</Link>
                <Link to="/businesspartner">Business Partners</Link>
              </ToolbarGroup>              
            </Toolbar>
          ) : (
            null
          )}
              
          {this.props.children}
        </div>
      </MuiThemeProvider>
    )
  }
}