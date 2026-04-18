import { createHttpClient } from "./createHttpClient";
import { withCookieAuth } from "./interceptors/api-interceptors";

export const apiClient = createHttpClient(process.env.NEXT_PUBLIC_TMS_API_URL!)
  .use(withCookieAuth(process.env.NEXT_PUBLIC_TMS_API_URL!));