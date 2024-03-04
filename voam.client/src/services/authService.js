import * as request from '../lib/request';

const baseUrl = 'https://localhost:7097/api/Auth';

export const login = async (email, password) => {
    const result = await request.post(`${baseUrl}/Login`, {
        email,
        password,
    });

    return result;
};

export const register = (email, password, imgURL, username) => request.post(`${baseUrl}/Register`, {
    email,
    password,
    imgURL,
    username,
});

export const logout = () => request.get(`${baseUrl}/Logout`);