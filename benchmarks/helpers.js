import http from 'k6/http';
import { check } from 'k6';
import { BASE_URL } from './config.js';

export function checkOk(response, name = 'status is 2xx') {
    check(response, {
        [name]: (r) => r.status >= 200 && r.status < 300,
    });
}

export function randomGuid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        const r = Math.random() * 16 | 0;
        const v = c === 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}

export function randomItem(items) {
    return items[Math.floor(Math.random() * items.length)];
}

export function randomPage(maxPage) {
    return Math.floor(Math.random() * maxPage) + 1;
}

export function loadProducts(limit = 500) {
    const pageSize = 100;
    const pages = Math.ceil(limit / pageSize);
    const products = [];

    for (let page = 1; page <= pages; page++) {
        const response = http.get(`${BASE_URL}/api/products?page=${page}&pageSize=${pageSize}`);

        if (response.status !== 200) {
            throw new Error(`Failed to load products. Status: ${response.status}`);
        }

        const items = response.json();

        products.push(...items);

        if (items.length < pageSize) {
            break;
        }
    }

    return products.slice(0, limit);
}