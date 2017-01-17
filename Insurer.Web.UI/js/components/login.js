import React from "react"
import auth from "../help/auth"
import { browserHistory, Router, Route, Link, withRouter } from 'react-router'

import ErrorMessage from './commons/errorMessage'
import Loading from './commons/loading'
import TextField from 'material-ui/TextField'
import FlatButton from 'material-ui/FlatButton'
import {Card, CardActions, CardHeader, CardMedia, CardTitle, CardText} from 'material-ui/Card'

export const Login = withRouter(
  React.createClass({

    getInitialState() {
      return {
        error: false,
        isLoading: false
      }
    },

    handleSubmit(event) {
      event.preventDefault()
      this.setState({isLoading: true})
      const email = this.refs.email.input.value
      const pass = this.refs.pass.input.value

      auth.login(email, pass, (loggedIn) => {
        this.setState({isLoading: false})
        if (!loggedIn)
          return this.setState({error: true})

        const { location } = this.props

        if (location.state && location.state.nextPathname) {
          this.props.router.replace(location.state.nextPathname)
        } else {
          this.props.router.replace('/customers')
        }
      })
    },

    render() {
      
      
      
      return (
        <form onSubmit={this.handleSubmit}>    
          
          <Card style={{position: 'relative', width: 300, marginLeft: 'auto', marginRight: 'auto', marginTop:20}}>          
             {this.state.isLoading && (
                <Loading />                                                  
              )}   
            <CardTitle title="Login" />
            <CardText>
              {this.state.error && (
                <ErrorMessage message={'Incorrect email or password.'} />                                  
              )}
              <div>
                <TextField
                  ref="email"
                  hintText="Email"
                  floatingLabelText="Email"
                  type="email" 
                  required="required"
                />
                <TextField
                  ref="pass"
                  hintText="Password"
                  floatingLabelText="Password"
                  type="password" 
                  required="required"
                />          
              </div>
            </CardText>
            <CardActions>
              <FlatButton label="Login" primary={true} type="submit" />                                    
            </CardActions>
          </Card>
        </form>
      )
    }
  })
)