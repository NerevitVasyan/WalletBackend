import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import { createStore, applyMiddleware } from 'redux';
import { reducer } from './reducers/reducer';
import Provider from 'react-redux';
import thunk from 'redux-thunk';
import { actionCreators } from './actions/actions';
import axios from 'axios';

// const test = async () => {
//     let response = await fetch("https://localhost:44342/api/wallet");
//     let data = await response.json();
//     console.log(data);
// }

// test();

// fetch("https://localhost:44342/api/wallet")
//     .then(res => res.json())
//     .then(data => console.log(data))
//     .catch(err => console.error("---" + err));

// axios.defaults.baseURL = "https://localhost:44342/api/";
// axios.get("wallet/")
//     .then(res => console.log(res.data))
//     .finally(() => console.log("finally"));


// let obj1 = {
//     prop1: "prop1",
//     prop2: "prop2"
// }

// let obj2 = { 
//     ...obj1,
//     prop2: "test"
//  };

//  console.log(obj2);

let store = createStore(reducer, { spendings: [], token: "TOKEN" }, thunk);

actionCreators.fetchDataSecond(store.dispatch);

//store.dispatch(actionCreators.fetchData());

console.log(store.getState());

// ReactDOM.render(
//     <Provider store={store}>
//         <App />
//     </Provider>, document.getElementById('root'));


