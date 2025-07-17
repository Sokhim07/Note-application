import axios, { AxiosError } from "axios";

const $api = {
    get: <T = any>(
        url: string,
        params: Record<string, any> = {},
        ok: (data: T) => void = () => {},
        err: (error: any) => void = () => {}
    ): void => {
        axios.get<T>(url, {
            headers: { Authorization: "xxxx" },
            params
        })
        .then(res => res.status === 200 && ok(res.data))
        .catch((e: AxiosError) => err(e.response?.data || e.message));
    }
};

export default $api;
