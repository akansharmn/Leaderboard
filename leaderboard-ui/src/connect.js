import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'
import  ParticipantTable from './ParticipantTable'
import React, { Component } from 'react';

class Connect extends Component {
  constructor(props){
    super(props)

    this.state = {
      performers: [
       
      ],
      hubConnection: null
    }
  }

  async componentDidMount(){
    const hubConnection = new HubConnectionBuilder().withUrl("http://localhost:5000/TopPerformersHub").configureLogging(LogLevel.Information).build();
    
    this.setState({ hubConnection}, () => {
      this.state.hubConnection
           .start()
           .then(
             () => {
               console.log('connection started');
               this.state.hubConnection.invoke('RegisterAsReceiver');
               
             }
           ).catch( err =>
                console.log('Error while establishing connection :(')
           );

           this.state.hubConnection.on('UpdateBoard', topPerformers => {
             console.log('received performers ' + topPerformers);
             this.setState({performers:topPerformers});
           });
    });
  }

  render(){
    return <ParticipantTable rows = {this.state.performers}/>;
    
  }
}

export default Connect;