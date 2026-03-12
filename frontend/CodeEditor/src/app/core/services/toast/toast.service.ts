import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  message = signal<string>('');

  show(message: string) {
    this.message.set(message);
    setTimeout(() => {
      this.clear();
    }, 1500)
  }

  clear(){
    this.message.set('');
  }
}
