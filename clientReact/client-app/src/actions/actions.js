import axios from 'axios';

export const FETCH_DATA = "FETCH_DATA";
export const ADD_SPENDING = "ADD_SPENDING";

export const actionCreators = {
    fetchData: async () => {
        let res = await axios.get("https://localhost:44342/api/wallet");
        return { type: FETCH_DATA, spendings: res.data }
    },
    fetchDataSecond: (dispatch) => {
        axios.get("https://localhost:44342/api/wallet").then(res => {
            dispatch({ type: FETCH_DATA, spendings: res.data });
        });
    }


}