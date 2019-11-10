import axios, { AxiosRequestConfig } from 'axios';
import jwt_decode from 'jwt-decode';

export const FETCH_DATA = "FETCH_DATA";
export const ADD_SPENDING = "ADD_SPENDING";
export const LOGIN = "LOGIN";

export const actionCreators = {
    fetchData: async () => {
        let res = await axios.get("https://localhost:44342/api/wallet");
        return { type: FETCH_DATA, spendings: res.data }
    },
    fetchDataSecond: (dispatch) => {
        axios.get("https://localhost:44342/api/wallet").then(res => {
            dispatch({ type: FETCH_DATA, spendings: res.data });
        });
    },

    login: (dispatch, user) => {

        axios.post("https://localhost:44342/api/account/login", user,
            {
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(res => {
                let token = res.data.data;
                localStorage.setItem("token", token);
                let decoded = jwt_decode(token);

                dispatch({ type: LOGIN, decoded });
                console.log("Done");
            }).catch(err => {
                console.error(err);
            });

    }

}