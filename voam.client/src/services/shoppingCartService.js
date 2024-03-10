import * as request from "../lib/request";

const baseUrl = "https://localhost:7097/api/ShoppingCart";

export const get = async (userId) => {
    const result = await request.get(`${baseUrl}/GetShoppingCart?userId=${userId}`);

    return result;
};