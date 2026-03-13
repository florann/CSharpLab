import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  message = signal<string>('');
  type = signal<'success' | 'error' | 'warning'>('success');


  show(message: string, type: 'success' | 'error' | 'warning') {
    console.log("Set values");
    this.message.set(message);
    this.type.set(type);
    // setTimeout(() => {
    //   this.clear();
    // }, 1500)
  }

  clear(){
    this.message.set('');
  }
}
