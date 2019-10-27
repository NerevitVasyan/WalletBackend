import React, { Component } from 'react';
import axios from 'axios';
import logo from './logo.svg';
import './App.css';

class App extends Component {
	state = {
		spendings:[]
	}

	 constructor (props) {
		super(props);
		console.log("ctor");
	}

	componentWillUnmount() {
		console.log("will unmount");
	}

	componentDidMount() {
		axios.get("https://localhost:44342/api/wallet")
		.then((res) => {
			this.setState({
				spendings: res.data
			})
		});
	}

	componentDidUpdate(prevProps, prevState) {
		console.log("did update");
	}

	render() {
		console.log("render");
		
		

		return (
			<div>
				<ul style={{padding:"10px",background: "green"}}>
					{this.state.spendings.map(s => 
						<li key={s.id}>
							{s.description}
						</li>
					)}
				</ul>
			</div>
		);
	}
}

export default App;