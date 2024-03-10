import * as request from "../lib/request";

const baseUrl = "https://localhost:7097/api/Review";

export const get = async (userId, productId) => {
    const result = await request.get(`${baseUrl}/GetProductAverageRating?userId=${userId}&productId=${productId}`);

    return result;
};

export const post = async (data) => {
    const result = await request.post(`${baseUrl}/AddRating`, data);

    return result;
};