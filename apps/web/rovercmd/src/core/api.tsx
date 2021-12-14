async function post<T> (url: string, data: Object) {
    const rawRespone = await fetch(url, {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
          },
        body: JSON.stringify(data)
    });

    return rawRespone.json() as Promise<T>;
}

export { post };