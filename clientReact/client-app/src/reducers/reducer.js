import { FETCH_DATA,ADD_SPENDING, LOGIN } from '../actions/actions';


let initialState = {
    spendings: [],
    token: "MY TOKEN",
    decoded: {
        email: "asdsad"
    }
}

// reducer must be pure function
export function reducer(state,action) {
    state = state || initialState;
    switch(action.type) {
        case FETCH_DATA:
            return {
                ...state,
                spendings: action.spendings
            }
        case LOGIN:{
            let res = {
                ...state,
                decoded: action.decoded
            };
            console.log(res);
            return res;
        }
    }
}