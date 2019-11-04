import React, { Component } from 'react';
import { actionCreators } from '../actions/actions';
import { connect } from 'react-redux';

class Login extends Component {
    state = {}

    login = () => {
        let user = {
            email: this.refs.email.value,
            password: this.refs.password.value,
            isRemember: this.refs.isRemember.checked,
        }

        actionCreators.login(this.props.dispatch,user);
    }

    render() {
        console.log(this);
        return (
            <div>
                <div>{this.props.state!==undefined?this.props.state.decoded.sub:"NONE"}</div>
                <div>
                    <p>Email:</p>
                    <input ref="email" type="text" />
                </div>
                <div>
                    <p>Password:</p>
                    <input ref="password" type="password" />
                </div>
                <div>
                    <p>IsRemember:</p>
                    <input ref="isRemember" type="checkbox" />
                </div>
                <button onClick={this.login} >Login</button>
            </div>
        );
    }
}

const mapStateToProps = state => ({ state: state })

export default connect(mapStateToProps)(Login);