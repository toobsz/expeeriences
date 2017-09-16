import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import * as firebase from 'firebase';

// Initialize Firebase
var config = {
  apiKey: "AIzaSyBibhjUGNzGXaP0S03zir3UGOdaeIE8UXc",
  authDomain: "wandertrust-react.firebaseapp.com",
  databaseURL: "https://wandertrust-react.firebaseio.com",
  projectId: "wandertrust-react",
  storageBucket: "wandertrust-react.appspot.com",
  messagingSenderId: "154161995431"
};
firebase.initializeApp(config);

ReactDOM.render(<App />, document.getElementById('root'));
registerServiceWorker();
