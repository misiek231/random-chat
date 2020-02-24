import React, { Component } from 'react';
import Inputs from './inputs'
import ChatWindow from './chat_window'
import './App.css';
import {HubConnectionBuilder, HttpTransportType} from '@aspnet/signalr'

class App extends Component {

  constructor(props) {
    super(props);
    this.state = {
      message: null,
      connection: null
    };

    this.onSended = this.onSended.bind(this);
  }

  componentDidMount = () => {
    var connection = new HubConnectionBuilder().withUrl("/chat", {
      skipNegotiation: true,
      transport: HttpTransportType.WebSockets
    }).build();

    connection.on("StartChat", function () {
      console.log('StartChat')
    });

    connection.on("Message", (message) => {
      this.setState({
        message: {message: message, isMain: false}
      })
    });

    connection.start().then(function () {
      console.log('connected')
    }).catch(function (err) {
      return console.error(err.toString());
    });

    this.setState({
      connection: connection
    });
  }

  onSended(message){
    this.setState({
      message: {message: message, isMain: true}
    })

    this.state.connection.invoke("Message", message).catch(function (err) {
      return console.error(err.toString());
  });
  }

  render(){
    return (
      <div className="App">
        <ChatWindow message={this.state.message} />
        <Inputs onSended={this.onSended}/>
      </div>
    );
  }
}

export default App;
