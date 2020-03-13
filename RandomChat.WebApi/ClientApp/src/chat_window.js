import React, { Component } from 'react';
import './message'
import Message from './message';

class ChatWindow extends Component {

    constructor(props) {
        super(props);
        this.state = {messages: []};
      }

    componentDidUpdate(prevProps) {
        console.log(this.props.message)
        if (this.props.message !== prevProps.message) {

            let messages = this.state.messages;
            messages.push(this.props.message)
            this.setState({
                  messages : messages
            })
        }
    }

    render(){
        console.log(this.state.messages)
        const messagesMap = this.state.messages.map((message) =>
            <Message isMain={message.isMain} message={message.message}></Message>
        );
        return (
            <div style={{backgroundColor:"pink", height: "90%", width:"100%"}}>
                {messagesMap}
            </div>
        );
    }
}

  
export default ChatWindow;