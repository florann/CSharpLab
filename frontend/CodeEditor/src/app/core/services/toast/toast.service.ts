import { ApplicationRef, createComponent, EnvironmentInjector, inject, Injectable, inputBinding, outputBinding, signal } from '@angular/core';
import { Toast } from '../../../components/toast/toast';

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  private appRef = inject(ApplicationRef);
  private injector = inject(EnvironmentInjector);
  
  message = signal<string>('');
  type = signal<'success' | 'error' | 'warning'>('success');


  show(message: string, type: 'success' | 'error' | 'warning') {
    const container = document.getElementById('toasts-container')!;
    const host = document.createElement('div');

    const ref = createComponent(Toast, {
      environmentInjector: this.injector,
      hostElement: host,
      bindings: [
        inputBinding('message', () => message),
        inputBinding('type', () => type),
        outputBinding('dismiss', () => {
          ref.instance.leave();
          setTimeout(() => {
            this.appRef.detachView(ref.hostView);
            ref.destroy();
          }, 1500)
        }),
      ],
    });

    this.appRef.attachView(ref.hostView);
    container.appendChild(host);
  }

  clear(){
    this.message.set('');
  }
}
