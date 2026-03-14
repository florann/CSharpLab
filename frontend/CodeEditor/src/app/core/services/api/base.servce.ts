import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
@Injectable({
  providedIn: 'root',
})

export class BaseServiceApi {
    
  /**
   * Handle HTTP errors
   */
  handleError(error: Response): Observable<never> {
    console.error('API Error:', error);

    return throwError(() => error);
  }
}
