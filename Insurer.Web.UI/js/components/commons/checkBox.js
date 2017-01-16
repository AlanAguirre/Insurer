import React from 'react'

class Checkbox extends React.Component {
    constructor(props) {
       super(props)    
       this.state = { isChecked: false }
       this.toggleCheckboxChange = this.toggleCheckboxChange.bind(this)
    }
  
    toggleCheckboxChange() {
        const { handleCheckboxChange, item } = this.props

        this.setState(({ isChecked }) => (
        {
            isChecked: !isChecked,
        }
        ));

        handleCheckboxChange(item)
    }

    render() {
        const { item } = this.props
        const { isChecked } = this.state

        return (
        <div className="checkbox">
            <label>
            <input
                type="checkbox"
                value={item.id}
                checked={isChecked}
                onChange={this.toggleCheckboxChange}
            />
            {item.name}
            </label>
        </div>
        )
  }
}

Checkbox.propTypes = {
  item: React.PropTypes.shape({
    id: React.PropTypes.number,
    name: React.PropTypes.string
  }).isRequired,
  handleCheckboxChange: React.PropTypes.func.isRequired,
}

export default Checkbox
