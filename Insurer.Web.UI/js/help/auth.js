//import {request} from "./fetch"
import axios from "axios"
import {config} from "./config"

module.exports = {
  login(email, pass, cb) {
    cb = arguments[arguments.length - 1]
    if (sessionStorage.token) {
      axios.defaults.headers.common['Authorization'] = sessionStorage.tokenType + " " + sessionStorage.token;
      if (cb) cb(true)
      this.onChange(true)
      return
    }
    if(email && pass){
        pretendRequest(email, pass, (res) => {
          if (res.authenticated) {
            sessionStorage.token = res.token
            sessionStorage.tokenType = res.tokenType
            sessionStorage.userName = res.userName
            if (cb) cb(true)
            this.onChange(true)
          } 
          else{
            if (cb) cb(false)
              this.onChange(false)
            }
        })
    }
    else{
      if (cb) cb(false)
      this.onChange(false)
    }
    
    
  },

  getToken() {
    return sessionStorage.token
  },

  logout(cb) {
    delete sessionStorage.token
    axios.defaults.headers.common['Authorization'] = "";
    if (cb) cb()
    this.onChange(false)
  },

  loggedIn() {
    return !!sessionStorage.token
  },

  onChange() {}
}

function pretendRequest(email, pass, cb) {
  let url = config.login;
  
  var params = new URLSearchParams();
  params.append('grant_type', 'password');
  params.append('username', email);
  params.append('password', pass);
  axios.post(url, params)
  .then((response)=>{
    axios.defaults.headers.common['Authorization'] = response.data.token_type + " " + response.data.access_token;
    cb({
        authenticated: true,
        token: response.data.access_token,
        tokenType : response.data.token_type,
        userName : response.data.userName
      })
  }).catch(()=>{
    axios.defaults.headers.common['Authorization'] = "";
    cb({ authenticated: false })
  })
      
}