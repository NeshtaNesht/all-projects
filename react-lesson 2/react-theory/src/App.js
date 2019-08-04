import React, { Component } from "react";
import "./App.css";
import Car from "./Car/Car.js";

class App extends Component {
  state = {
    //ключ cars имеет массив
    cars: [
      //Описание объектов в массивеgit
      { name: "Ford Focus 3", year: "2018" },
      { name: "Audi A3", year: "2007" },
      { name: "Mazda 3", year: "2007" }
    ],
    pageTitle: "Автомобили",
    showCars: false
  };

  OnChangeName = (name, index) => {
    const car = this.state.cars[index];
    car.name = name;
    //Клонируем массив с помощью спред-оператора ... из state в cars, чтобы не было мутации
    const cars = [...this.state.cars];
    cars[index] = car;
    this.setState({
      cars //Упрощенная запись из-за одинаковых наименований
    });
  };

  OnDeleteCar(index){
    const cars = this.state.cars.concat()
    cars.splice(index, 1)

    this.setState({cars})
  }

  showCarsHandler = () => {
    //Изменение состояния по ключу
    this.setState({
      showCars: !this.state.showCars
    });
  };

  render() {
    const divStyle = {
      margin: "10px",
      border: "1px solid #ccc",
      padding: "20px"
    };

    let nameButton = this.state.showCars ? "HIDE" : "SHOW";
    let cars = this.state.showCars
      ? this.state.cars.map((car, index) => {
          return (
            <Car
              key={index}
              name={car.name}
              year={car.year}
              onChangeName={event => {
                this.OnChangeName(event.target.value, index);
              }}
              delete={
                this.OnDeleteCar.bind(this, index)
              }
            />
          );
        })
      : null;
    return (
      <div className="App" style={divStyle}>
        <h2>{this.state.pageTitle}</h2>
        <div style={{
          width: 400,
          margin: 'auto',
          paddingTop: '10px'          
        }}>
          {cars}
        </div>        
        <button onClick={this.showCarsHandler}>{nameButton}</button>
      </div>
    );
  }
}

export default App;
