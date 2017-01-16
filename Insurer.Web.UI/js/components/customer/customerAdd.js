import React from "react"

import Paper from 'material-ui/Paper'
import TextField from 'material-ui/TextField'
import Checkbox from '../commons/checkBox'
import FlatButton from 'material-ui/FlatButton'
import ErrorMessage from '../commons/errorMessage'

class CustomerAdd extends React.Component{
  constructor(props) {
    super(props)   
    this.state = { error: "" }
    this.toggleCheckbox = this.toggleCheckbox.bind(this)
    this.handleFormSubmit = this.handleFormSubmit.bind(this)
  }
  
  toggleCheckbox (item) {
    if (this.selectedCheckboxes.has(item)) {
      this.selectedCheckboxes.delete(item);
    } else {
      this.selectedCheckboxes.add(item);
    }
  }

  handleFormSubmit (e) {
    e.preventDefault();

    const name = this.refs.name.input.value
    const email = this.refs.email.input.value
    const phoneNumber = this.refs.phoneNumber.input.value
    const adress = this.refs.adress.input.value
    
    if(!(name && email && phoneNumber)){
        this.setState({
            error : "Name, email and phone number are required."
        })
    }
    
    const insuranceType = []
    for (const item of this.selectedCheckboxes) {
      
      insuranceType.push({
          InsuranceTypeId: item.id,
          Name: item.name
      })
    }
    
    
    const customer = {
        Name: name,
        Email: email,
        PhoneNumber: phoneNumber,
        Address: adress,
        InsuranceTypes: insuranceType
    }
    
    
    console.log("customer", customer)
    this.props.handleCreateCustomer(customer)
  }

  
  componentWillMount() {
    this.selectedCheckboxes = new Set();
  }
  
  componentDidMount() {
    
  }
    
  render() {
    const {insuranceTypes} = this.props
    const createCheckbox = item => (
    <Checkbox
            item={{id: item.InsuranceTypeId, name: item.Name}}
            handleCheckboxChange={this.toggleCheckbox}
            key={item.name}
        />
    )        
            
    return (
        <form onSubmit={this.handleFormSubmit}>             
          <Paper zDepth={2} style={{padding: 20}}>
            {this.state.error !== "" ? 
                <ErrorMessage message={this.state.error} />
                :
                null    
            }
            <TextField
                    ref="name"
                    hintText="Name*"
                    floatingLabelText="Name*"
                    /><br/>
            <TextField
                  ref="email"
                  hintText="Email*"
                  floatingLabelText="Email*"
                  type="email"
                /><br/>
            <TextField
                ref="phoneNumber"
                hintText="Phone Number*"
                floatingLabelText="Phone Number*"
                /><br/>
             <TextField
                ref="adress"
                hintText="Adress"
                floatingLabelText="Adress"
                /><br/>
                <h3>
                    Insurance types
                </h3>
                {insuranceTypes.map(createCheckbox)}   
                
                <div style={{marginTop:20, display:'flex'}}>
                    <FlatButton label="Submit" primary={true} type="submit" />
                    <FlatButton label="Cancel" /> 
                </div>
                       
           </Paper>          
        </form>
    )
  }

}

CustomerAdd.propTypes = {
  insuranceTypes: React.PropTypes.arrayOf(React.PropTypes.shape({
    InsuranceTypeId: React.PropTypes.number,
    Name: React.PropTypes.string
  })).isRequired,
  handleCreateCustomer: React.PropTypes.func.isRequired
}

export default CustomerAdd