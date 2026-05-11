import http from 'k6/http';
import { sleep } from 'k6';
import { BASE_URL, defaultHeaders } from './config.js';
import { checkOk, randomGuid, loadProducts, randomItem } from './helpers.js';

export const options = {
    stages: [
        { duration: '30s', target: 10 },
        { duration: '2m', target: 10 },
        { duration: '30s', target: 0 },
    ],
    thresholds: {
        http_req_failed: ['rate<0.05'],
        http_req_duration: ['p(95)<2500'],
    },
};

export function setup() {
    const products = loadProducts(Number(__ENV.PRODUCT_POOL_SIZE || 500));

    if (products.length === 0) {
        throw new Error('No products loaded for benchmark.');
    }

    return {
        products,
    };
}

export default function (data) {
    const userId = randomGuid();
    const product = randomItem(data.products);
    const productId = product.id ?? product.Id;

    const addToCartResponse = http.post(
        `${BASE_URL}/api/carts/${userId}/items`,
        JSON.stringify({
            productId,
            quantity: 1,
        }),
        defaultHeaders
    );

    checkOk(addToCartResponse, 'cart item added');

    const createOrderResponse = http.post(
        `${BASE_URL}/api/orders`,
        JSON.stringify({
            userId,
        }),
        defaultHeaders
    );

    checkOk(createOrderResponse, 'order created');

    sleep(1);
}