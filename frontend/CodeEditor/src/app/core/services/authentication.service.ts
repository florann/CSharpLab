import { Injectable, inject, signal } from '@angular/core';
import { Observable, map, catchError, throwError, tap } from 'rxjs';
import { AuthService, LoginRequest } from '../';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private authService = inject(AuthService);

  // ✅ Declare the signal
  isAuthenticated = signal<boolean>(false);

  ApiAuthLoginPost(request: LoginRequest): Observable<Boolean> {
    return this.authService.apiAuthLoginPost(request).pipe(
      tap(() => {
        this.isAuthenticated.set(true);
      }),
      catchError(this.handleError)
    );
  }

  ApiAuthRefresh(): Observable<boolean> {
    return this.authService.apiAuthRefreshPost().pipe(
      tap(() => {
        this.isAuthenticated.set(true);
      }),
      catchError(this.handleError)
    )
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