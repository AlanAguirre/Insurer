import React from "react"


import CheckboxItem from '../commons/checkBox'
import FlatButton from 'material-ui/FlatButton'
import ErrorMessage from '../commons/errorMessage'
import { Field, reduxForm } from 'redux-form'
import {  
  TextField
} from 'redux-form-material-ui'

// validation functions
const required = value => value === null ? 'Required' : undefined
const email = value => value &&
  !/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(value) ? 'Invalid email' : undefined
  
 const validate = values => {
  const errors = {}
  const requiredFields = [ 'name', 'email', 'phoneNumber' ]
  requiredFields.forEach(field => {
    if (!values[ field ]) {
      errors[ field ] = 'Required'
    }
  })
  if (values.email && !/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(values.email)) {
    errors.email = 'Invalid email address'
  }
  return errors
} 
 
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

    const name = this.refs.name.value
    const email = this.refs.email.value
    const phoneNumber = this.refs.phoneNumber.value
    const address = this.refs.address.value
    
    if(!name || !email || !phoneNumber){
      this.setState({
            error : "Name, email and phone number are required."
        })
      return;
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
        Address: address,
        InsuranceTypes: insuranceType
    }
    
    
    console.log("customer", customer)
    this.props.handleCreateCustomer(customer)
  }

  
  componentWillMount() {
    this.selectedCheckboxes = new Set();
  }
  
  componentDidMount() {
    this.refs.name            // the Field
      .getRenderedComponent() // on Field, returns ReduxFormMaterialUITextField
      .getRenderedComponent() // on ReduxFormMaterialUITextField, returns TextField
      .focus()                // on TextField
  }
    
  render() {
    const {insuranceTypes, handleCancel, pristine, reset, submitting} = this.props
    const createCheckbox = item => (
    <CheckboxItem
            key={"checBox" + item.Name}
            item={{id: item.InsuranceTypeId, name: item.Name}}
            handleCheckboxChange={this.toggleCheckbox}            
        />
    )        
            
    return (
        <form style={{padding: 20}} onSubmit={this.handleFormSubmit}>      
          {this.state.error !== "" ? 
                <ErrorMessage message={this.state.error} />
                :
                null    
            }
          <div>
            <Field name="name"
              component={TextField}
              hintText="Name"
              floatingLabelText="Name"
              ref="name" withRef/>
          </div>
          <div>
            <Field name="email"
              component={TextField}
              hintText="Email"
              floatingLabelText="Email"
              ref="email" withRef/>
          </div>
          <div>
            <Field name="phoneNumber"
              component={TextField}
              hintText="Phone Number"
              floatingLabelText="Phone Number"
              ref="phoneNumber" withRef/>
          </div>
          <div>
            <Field name="address"
              component={TextField}
              hintText="Address"
              floatingLabelText="Address"
              ref="address" withRef/>
          </div>
          <div>
            <h3>
                Insurance types
            </h3>
            {insuranceTypes.map(createCheckbox)}
          </div>
                   
                
          <div style={{marginTop:20, display:'flex'}}>
              <FlatButton label="Submit" disabled={pristine || submitting} primary={true} type="submit" />
              <FlatButton label="Cancel" onClick={handleCancel} /> 
          </div>                
        </form>
    )
  }

}

CustomerAdd.propTypes = {
  insuranceTypes: React.PropTypes.arrayOf(React.PropTypes.shape({
    InsuranceTypeId: React.PropTypes.number,
    Name: React.PropTypes.string
  })).isRequired,
  handleCreateCustomer: React.PropTypes.func.isRequired,
  handleCancel: React.PropTypes.func.isRequired
}

export default reduxForm({
  form: 'customerForm',
  validate
})(CustomerAdd)