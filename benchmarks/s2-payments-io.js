import http from 'k6/http';
import { sleep } from 'k6';
import { BASE_URL, defaultHeaders } from './config.js';
import { checkOk, randomGuid } from './helpers.js';

export const options = {
    stages: [
        { duration: '30s', target: 10 },
        { duration: '2m', target: 10 },
        { duration: '30s', target: 0 },
    ],
    thresholds: {
        http_req_failed: ['rate<0.01'],
        http_req_duration: ['p(95)<1500'],
    },
};

export default function () {
    const payload = JSON.stringify({
        orderId: randomGuid(),
        amount: 249.99,
    });

    const response = http.post(
        `${BASE_URL}/api/payments`,
        payload,
        defaultHeaders
    );

    checkOk(response, 'payment processed');

    sleep(1);
}