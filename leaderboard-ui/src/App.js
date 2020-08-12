import React from 'react';
import logo from './logo.svg';
import './App.css';
import { array } from 'prop-types';
import Table from '@material-ui/core/Table';
import SimpleTable from './ParticipantTable';
import ParticipantTable from './ParticipantTable';
import Connect from './connect';

function TopPerformers(props){
  const sampleParticipants = [
    {name : 'Sam', dob: '1995-08-06T00:00:00-04:00', checkpoint: 1, timeElapsed: 32},
    {name : 'Meera', dob: '1995-08-06T00:00:00-04:00', checkpoint: 2, timeElapsed: 32},
    {name : 'Jacob', dob: '1995-08-06T00:00:00-04:00', checkpoint: 3, timeElapsed: 32}
  ];
  
  const participantsList = sampleParticipants.map((p) => 
  <li>{p.name} {p.dob} {p.checkpoint} {p.timeElapsed}</li>);
 
  return (<ul>{participantsList}</ul>);
 }

 function TopPerformers1(props){
  const sampleParticipants = [
    {name : 'Sam', age: 24, checkpoint: 1, timeElapsed: 32},
    {name : 'Meera', age: 25, checkpoint: 2, timeElapsed: 32},
    {name : 'Jacob', age: 26, checkpoint: 3, timeElapsed: 32}
  ];
 }

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <Connect/>
         
      </header>
    </div>
  );
}



export default App;
