import { inject, Injectable, signal } from '@angular/core';
import { debounceTime, fromEvent, interval, merge, switchMap, takeUntil, timer } from 'rxjs';
import { AuthenticationService } from '../authentication/authentication.service';

@Injectable({
  providedIn: 'root',
})

export class IdleService {
  isIdle = signal(false);
  private idleTimeout = 60000; 
  authenticationService = inject(AuthenticationService);

  startWatching() {
    const activity$ = merge(
      fromEvent(document, 'mousemove'),
      fromEvent(document, 'keydown'),
      fromEvent(document, 'click'),
      fromEvent(document, 'scroll')
    );

    activity$.pipe(
      debounceTime(this.idleTimeout),
      switchMap(() =>   
        timer(0, this.idleTimeout).pipe(
        takeUntil(activity$)
      )
    )
    ).subscribe(() => {
      this.isIdle.set(true);
      this.authenticationService.ApiCheckStatus().subscribe();
    });

    activity$.subscribe(() => {
      console.log("Non Idle");
      this.isIdle.set(false);
    });
  }
}
