export type HttpClientConfig = {
  baseUrl: string;
  withAuth?: boolean;
};

export type HttpRequest = {
  url: string;
  init: RequestInit;
};

export type HttpResponse = Response;

export type HttpInterceptor = {
  onRequest?: (req: HttpRequest) => Promise<HttpRequest> | HttpRequest;
  onResponse?: (res: HttpResponse, req: HttpRequest) => Promise<HttpResponse> | HttpResponse;
  onError?: (error: any, req: HttpRequest) => Promise<HttpResponse>;
};