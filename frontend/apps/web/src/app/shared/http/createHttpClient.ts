import { HttpInterceptor, HttpRequest } from "./http.types";

export function createHttpClient(baseUrl: string) {
  const interceptors: HttpInterceptor[] = [];

  async function request(path: string, init: RequestInit = {}) {
    let req: HttpRequest = {
      url: `${baseUrl}${path}`,
      init,
    };

    for (const interceptor of interceptors) {
      if (interceptor.onRequest) {
        req = await interceptor.onRequest(req);
      }
    }
    
    try {
      let res = await fetch(req.url, {
        ...req.init,
        credentials: "include",
      });

      for (const interceptor of [...interceptors].reverse()) {
        if (interceptor.onResponse) {
          res = await interceptor.onResponse(res, req);
        }
      }

      return res;
    } catch (error) {
      for (const interceptor of [...interceptors].reverse()) {
        if (interceptor.onError) {
          return interceptor.onError(error, req);
        }
      }

      throw error;
    }
  }

  return {
    use(interceptor: HttpInterceptor) {
      interceptors.push(interceptor);
      return this;
    },

    get: (path: string) => request(path),
    post: (path: string, body?: unknown, headers?: HeadersInit) =>
      request(path, {
        method: "POST",
        headers: headers ?? { "Content-Type": "application/json" },
        body: JSON.stringify(body),
      }),
    put: (path: string, body?: unknown, headers?: HeadersInit) =>
      request(path, {
        method: "PUT",
        headers: headers ?? { "Content-Type": "application/json" },
        body: JSON.stringify(body),
      }),
    patch: (path: string, body?: unknown, headers?: HeadersInit) =>
      request(path, {
        method: "PATCH",
        headers: headers ?? { "Content-Type": "application/json" },
        body: JSON.stringify(body),
      }),
    delete: (path: string) =>
      request(path, { method: "DELETE" }),
  };
}