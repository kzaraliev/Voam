import * as request from "../lib/request";

const baseUrl = "https://localhost:7097/api/Product";

export const getAll = async () => {
    const result = await request.get(`${baseUrl}/GetAllProducts`);

    return Object.values(result);
};

export const getLatest = async () => {
    const result = await request.get(`${baseUrl}/GetRecentlyAddedProducts`);

    return result;
};

export const getOne = async (productId) => {
    const result = await request.get(`${baseUrl}/GetProductById?id=${productId}`);

    return result;
};

export const create = async (bookData) => {
    const result = await request.post(baseUrl, bookData);

    return result;
};

export const edit = async (bookId, bookData) => {
    const result = await request.put(`${baseUrl}/${bookId}`, bookData);

    return result;
};

export const remove = async (bookId) => request.remove(`${baseUrl}/${bookId}`);
