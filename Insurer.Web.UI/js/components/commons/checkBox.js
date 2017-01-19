import React from 'react'
import Checkbox from 'material-ui/Checkbox'

class Toggle extends React.Component{
    constructor(props) {
       super(props)       
    }
    
    render(){
        const { name, isChecked, toggleCheckboxChange } = this.props
        return (
            <Checkbox
                label={name}
                defaultChecked={isChecked} 
                onCheck={ toggleCheckboxChange.bind(isChecked) }/>        
        )
    }
    
}


class CheckboxItem extends React.Component {
    constructor(props) {
       super(props)    
       this.state = { isChecked: false }
       this.toggleCheckboxChange = this.toggleCheckboxChange.bind(this)
    }
  
    toggleCheckboxChange(toggleVal) {
        const { handleCheckboxChange, item } = this.props

        this.setState({
            isChecked: !toggleVal
        })

        handleCheckboxChange(item)
    }
    
    render() {
        const { item } = this.props
        const { isChecked } = this.state

        return (            
            <Toggle 
                name={item.name} 
                isChecked={isChecked} 
                toggleCheckboxChange={this.toggleCheckboxChange}
            />                                   
        )
  }
}

CheckboxItem.propTypes = {
  item: React.PropTypes.shape({
    id: React.PropTypes.number,
    name: React.PropTypes.string
  }).isRequired,
  handleCheckboxChange: React.PropTypes.func.isRequired
}

export default CheckboxItem
