import http from 'k6/http';
import { sleep } from 'k6';
import { BASE_URL, defaultHeaders } from './config.js';
import { checkOk, randomGuid, loadProducts, randomItem, randomPage } from './helpers.js';

export const options = {
    scenarios: {
        catalog_read: {
            executor: 'constant-vus',
            vus: 30,
            duration: '3m',
            exec: 'catalogRead',
        },
        create_orders: {
            executor: 'constant-vus',
            vus: 10,
            duration: '3m',
            exec: 'createOrder',
        },
        payments_io: {
            executor: 'constant-vus',
            vus: 10,
            duration: '3m',
            exec: 'payment',
        },
    },
    thresholds: {
        http_req_failed: ['rate<0.05'],
        http_req_duration: ['p(95)<2500'],
    },
};

const MAX_PAGE = Number(__ENV.MAX_PAGE || 100);
const PAGE_SIZE = Number(__ENV.PAGE_SIZE || 50);

export function setup() {
    const products = loadProducts(Number(__ENV.PRODUCT_POOL_SIZE || 500));

    if (products.length === 0) {
        throw new Error('No products loaded for benchmark.');
    }

    return {
        products,
    };
}

export function catalogRead() {
    const page = randomPage(MAX_PAGE);

    const response = http.get(
        `${BASE_URL}/api/products?page=${page}&pageSize=${PAGE_SIZE}`
    );

    checkOk(response, 'products page loaded');

    sleep(1);
}

export function createOrder(data) {
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

export function payment() {
    const response = http.post(
        `${BASE_URL}/api/payments`,
        JSON.stringify({
            orderId: randomGuid(),
            amount: 249.99,
        }),
        defaultHeaders
    );

    checkOk(response, 'payment processed');

    sleep(1);
}