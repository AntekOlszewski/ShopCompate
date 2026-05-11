import http from 'k6/http';
import { sleep } from 'k6';
import { BASE_URL } from './config.js';
import { checkOk, randomPage } from './helpers.js';

export const options = {
    stages: [
        { duration: '30s', target: 20 },
        { duration: '2m', target: 20 },
        { duration: '30s', target: 0 },
    ],
    thresholds: {
        http_req_failed: ['rate<0.01'],
        http_req_duration: ['p(95)<500'],
    },
};

const MAX_PAGE = Number(__ENV.MAX_PAGE || 100);
const PAGE_SIZE = Number(__ENV.PAGE_SIZE || 50);

export default function () {
    const page = randomPage(MAX_PAGE);

    const response = http.get(
        `${BASE_URL}/api/products?page=${page}&pageSize=${PAGE_SIZE}`
    );

    checkOk(response, 'products page loaded');

    sleep(1);
}