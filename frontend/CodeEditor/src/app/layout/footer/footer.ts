import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { AuthenticationService } from '../../core/services/authentication.service';
import { interval, Subscription, switchMap } from 'rxjs';

@Component({
  selector: 'app-footer',
  imports: [],
  templateUrl: './footer.html',
  styleUrl: './footer.scss',
})
export class Footer implements OnInit, OnDestroy {
  authenticationService = inject(AuthenticationService);
  private intervalFunctionSubscription?: Subscription;

  ngOnInit(): void {
    this.intervalFunctionSubscription = interval(5000).pipe(
      switchMap(() => this.authenticationService.ApiCheckStatus())
    ).subscribe({
      next: () => console.log('Check successful'), // Todo toast, must be loggin
      error: (err) => console.log('Check failed:', err)
    });
  }

  ngOnDestroy() {
    this.intervalFunctionSubscription?.unsubscribe();
  }
}
