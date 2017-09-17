import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';
import Map from './components/custom/Map'
import * as firebase from 'firebase'

class App extends Component {

  constructor(){
    super();
    this.state = {
      speed: 10
    };
  }

  componentDidMount(){
    const rootRef = firebase.database().ref();
    const speedRef = rootRef.child('speed');

    speedRef.on('value', snap => {
      this.setState({
        speed: snap.val()
      });
    })


  }


  render() {
    return (
      <div>
        <div>
        This is the react app
        </div>

        <div>{this.state.speed}</div>

        <div style={{height:300, width:300}}>
        <Map
            containerElement={<div style={{height:100+'%'}} />}
            mapElement={<div style={{height:100+'%'}} />}
        />
        </div>

      </div>
      // <div className="App">
      //   <div className="App-header">
      //     <img src={logo} className="App-logo" alt="logo" />
      //     <h2>Welcome to React</h2>
      //   </div>
      //   <p className="App-intro">
      //     To get started, edit <code>src/App.js</code> and save to reload.
      //   </p>
      // </div>
    );
  }
}

export default App;
