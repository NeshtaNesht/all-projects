import React, { Component } from 'react';
import './App.css';
import Car from './Car/Car.js'

class App extends Component {
  render() {
    const divStyle = {
      margin: '10px',
      border: '1px solid #ccc',
      padding: '20px'
    }
    return (
      <div className="App" style={divStyle}>
        <h2>Hello, World, this is class App</h2>
        <Car name="Ford Focus 3" year="2018" />
        <Car name="Audi A3" year="2007" >
          <p style={{color: 'red'}}>COLOR</p>
        </Car>
        <Car name="Mazda 3" year="2007" />
      </div>
    )
  }
}

export default App;
