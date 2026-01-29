import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authReq = req.clone({
    setHeaders: {
      Authorization: `Bearer ${getAuthToken()}`,
      'X-Custom-Header': 'MyValue'
    }
  });

  return next(authReq);
};

function getAuthToken(): string {
  return localStorage.getItem('authToken') || '';
}