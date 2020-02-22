import React, { Component } from 'react';

class Inputs extends Component {

    constructor(props) {
        super(props);
        this.state = {message: ''};
        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
      }
    
      handleChange(event) {
        this.setState({message: event.target.value});
      }
    
      handleSubmit(event) {
        this.props.onSended(this.state.message);
        event.preventDefault();
      }

    render(){
        return (
            <form style={{height: "10%", width: "100%"}} onSubmit={this.handleSubmit}>
                <input type="text" onChange={this.handleChange} style={{float:"left", height: "100%", width:"80%", fontSize: "30px"}} />
                <button style={{float:"left", height:"100%", width:"20%"}}>Wyślij</button>               
            </form>
        );
    }
}

  
export default Inputs;