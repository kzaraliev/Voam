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

export const create = async (productData) => {
    const result = await request.post(`${baseUrl}/CreateProduct`, productData);

    return result;
};

export const edit = async (productId, productData) => {
    const result = await request.put(`${baseUrl}/UpdateProduct?id=${productId}`, productData);

    return result;
};

export const remove = async (productId) => request.remove(`${baseUrl}/DeleteProduct?id=${productId}`);
