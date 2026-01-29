import { Injectable, inject } from '@angular/core';
import { Observable, map, catchError, throwError } from 'rxjs';
import { AuthService, LoginRequest, LoginResponse } from '../';

@Injectable({
  providedIn: 'root'
})
export class Authentication {
  private authService = inject(AuthService);

  /**
   * Get all products
   */
  apiAuthLoginPost(request: LoginRequest): Observable<LoginResponse> {
    return this.authService.apiAuthLoginPost(request);
  }

  /**
   * Handle HTTP errors
   */
  private handleError(error: any): Observable<never> {
    console.error('API Error:', error);
    
    let errorMessage = 'An error occurred';
    
    if (error.error instanceof ErrorEvent) {
      // Client-side error
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    
    return throwError(() => new Error(errorMessage));
  }
}