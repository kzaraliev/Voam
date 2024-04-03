import * as request from '../lib/request';

const baseUrl = 'https://localhost:7097/api/Auth';

export const login = async (email, password) => {
    const result = await request.post(`${baseUrl}/Login`, {
        email,
        password,
    });

    return result;
};

export const register = (email, password, firstName, lastName, phoneNumber) => request.post(`${baseUrl}/Register`, {
    email,
    password,
    firstName,
    lastName,
    phoneNumber
});

export const logout = () => request.get(`${baseUrl}/Logout`); //add logout to back-end

export const getInformation = async (userId) => {
    const result = await request.get(`${baseUrl}/GetUserInformation?id=${userId}`);
    return result;
};

export const getUserPhoneNumber = async (userId) => {
    const result = await request.get(`${baseUrl}/GetUserPhoneNumber?id=${userId}`);
    return result;
}