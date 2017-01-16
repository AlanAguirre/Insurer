import React from "react"


const errorStyle = {
        padding: '15px 20px',
        margin: '0 auto',
        marginBottom: 10,
        fontSize: 13,
        borderStyle: 'solid',
        borderWidth: 1,
        borderRadius: 5,
        color: '#911',
        backgroundColor: '#fcdede',
        borderColor: '#d2b2b2'
      }

export default class ErrorMessage extends React.Component {
    
    
  render() {
    return <div style={errorStyle} >{this.props.message}</div>;
  }
}