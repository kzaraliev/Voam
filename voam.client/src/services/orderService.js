import * as request from "../lib/request";

const baseUrl = "https://localhost:7097/api/Order";

export const add = async (id, data) => {
    const result = await request.post(`${baseUrl}/PlaceOrder?id=${id}`, data);

    return result;
};

export const get = async (userId) => {
    const result = await request.get(`${baseUrl}/GetAllOrdersForUser?id=${userId}`);
    return result;
}

export const getAll = async () => {
    const result = await request.get(`${baseUrl}/GetAllOrders`);
    return result;
}