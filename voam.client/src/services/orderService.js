import * as request from "../lib/request";

const baseUrl = "https://localhost:7097/api/Order";

export const add = async (id, data) => {
    const result = await request.post(`${baseUrl}/PlaceOrder?id=${id}`, data);

    return result;
};