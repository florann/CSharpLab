import { inject, Injectable, signal } from '@angular/core';
import { Observable, map, catchError, throwError, tap, from } from 'rxjs';
import { Auth, CreateAccountRequest, LoginRequest, UserResponse } from '../../api/index';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  
  private router = inject(Router);
  isAuthenticated = signal<boolean>(false);

  ApiAuthLoginPost(request: LoginRequest): Observable<UserResponse> {
      return from(Auth.postApiAuthLogin(
        { 
          credentials: 'include',
          body: request 
        })).pipe(
        map(response => {
          if(!response.data)
            throw response.response;

          return response.data; 
        }),
        tap((isSuccess) => {
          if (isSuccess) this.isAuthenticated.set(true);
        }),
        catchError(this.handleError)
      );
  }

  ApiAuthRefresh(): Observable<boolean> {
     return from(Auth.postApiAuthRefresh({
          credentials: 'include',
     })).pipe(
        map(response => {
          return !!response.data; 
        }),
        tap((isSuccess) => {
          if (isSuccess) this.isAuthenticated.set(true);
        }),
        
      );
  }

  ApiCreateAccount(request: CreateAccountRequest): Observable<boolean> {
    return from(Auth.postApiAuthCreateAccount({ body: request })).pipe(
      map(response => {
          console.log(response.data);
          return response.response.ok; 
      }),
      catchError(this.handleError)
    )
  }

  ApiCheckStatus(): Observable<void> {
    return from(Auth.getApiAuthStatus({
          credentials: 'include',
    }))
    .pipe(
      map(response => {
        if(response.response.status != 200)
        {
          this.router.navigate(['/login']);
        }
        return;
      }),
      catchError(err => {
        this.router.navigate(['/login']);
        return throwError(() => new Error(err));
      })
    );
  }

  /**
   * Logout user
   */
  logout(): void {
    this.isAuthenticated.set(false);
  }

  /**
   * Handle HTTP errors
   */
  private handleError(error: Response): Observable<never> {
    console.error('API Error:', error);

    return throwError(() => error);
  }
}