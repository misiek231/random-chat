import React, { Component } from 'react';

class Message extends Component {

    constructor(props) {
        super(props);
        this.state = {messages: []};
      }

    render(){
        return (
            <div style={{width:"100%", height: "10%", clear: "both"}}>
                <div style={{
                    backgroundColor: this.props.isMain ? "aqua" : "yellow",
                    float: this.props.isMain ? "right" : "left", 
                    textAlign: this.props.isMain ? "right" : "left", 
                    borderRadius: 10, 
                    padding: "8px",
                    margin: "10px",
                    fontSize:"30px"}}>
                    {this.props.message}
                </div>
            </div>
        );
    }
}

  
export default Message;