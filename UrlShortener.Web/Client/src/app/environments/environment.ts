type Env = {
    hostUrl: string,
    apiUrl: string,
    authCookieIndicator: string
}

export const environment: Env = {
    hostUrl: "http://localhost:5000",
    apiUrl: "http://localhost:5000/api",
    authCookieIndicator: "authenticated"
};
