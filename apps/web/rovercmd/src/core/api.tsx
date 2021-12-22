import { baseUrl } from './apiConfig';

async function post<T> (url: string, data: Object): Promise<T> {
    const rawResponse = await fetch(`${baseUrl}${url}`, {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    });

    return rawResponse.json() as Promise<T>;
}

export { post };