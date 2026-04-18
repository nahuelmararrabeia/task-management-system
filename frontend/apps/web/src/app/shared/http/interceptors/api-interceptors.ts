import { HttpInterceptor } from "../http.types";

export function withCookieAuth(baseUrl: string): HttpInterceptor {
  let isRefreshing = false;
  let refreshPromise: Promise<void> | null = null;

  async function refresh() {
    if (!refreshPromise) {
      refreshPromise = fetch(`${baseUrl}/Auth/refresh_token`, {
        method: "POST",
        credentials: "include",
      })
        .then((res) => {
          if (!res.ok) throw new Error("Refresh failed");
        })
        .finally(() => {
          refreshPromise = null;
        });
    }

    return refreshPromise;
  }

  return {
    async onResponse(res, req) {
      if (res.status !== 401) return res;

      if (isRefreshing) {
        await refreshPromise;
      } else {
        try {
          isRefreshing = true;
          await refresh();
        } catch {
          isRefreshing = false;
          window.location.href = "/features/auth";
          throw new Error("Session expired");
        }
        isRefreshing = false;
      }

      return fetch(req.url, {
        ...req.init,
        credentials: "include",
      });
    },
  };
}