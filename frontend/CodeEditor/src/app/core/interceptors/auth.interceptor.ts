import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthenticationService } from '../services/authentication/authentication.service';
import { catchError, switchMap, throwError } from 'rxjs';
import { Router } from '@angular/router';

let isRefreshing = false;

export const authInterceptor: HttpInterceptorFn = (req, next) => {

  const authService = inject(AuthenticationService);
  const router = inject(Router);

  const authReq = req.clone({
    withCredentials: true
  });

  return next(authReq).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status == 401 && !isRefreshing && !req.url && !req.url.includes('/api/auth/refresh')) {
        isRefreshing = true;

            return authService.ApiAuthRefresh().pipe(
              switchMap(() => {
                isRefreshing = false;

                return next(authReq);
              }),
              catchError((refreshError) => {
                isRefreshing = false;

                router.navigate(['/login']);
                return throwError(() => refreshError);
              })
            );
          }

          // If refresh endpoint fails or other error
          if (error.status === 401) {
            router.navigate(['/login']);
          }

          return throwError(() => error);
  }));
};

