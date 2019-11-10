import Login from '../components/login';
import { actionCreators } from '../actions/actions';
import { connect } from 'react-redux';

const mapStateToProps = (state) => ({ state })

const mapDispatchToProps = dispatch => ({
    login: (user) => { 
        actionCreators.login(dispatch, user);
    }
});

export default connect(mapStateToProps,mapDispatchToProps)(Login);