import { FETCH_DATA,ADD_SPENDING } from '../actions/actions';


let initialState = {
    spendings: [],
    token: "MY TOKEN"
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
    }
}