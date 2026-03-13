import { Component, computed, input } from '@angular/core';
import { NgClass } from  '@angular/common';

@Component({
  selector: 'app-toast',
  imports: [NgClass],
  templateUrl: './toast.html',
  styleUrl: './toast.scss',
})
export class Toast {
  message = input.required<string>();
  type = input.required<'success' | 'error' | 'warning'>();

  toast_class =  computed(() => {
    console.log("Computed signal");
    switch (this.type()) {
      case 'success': return 'toast-container-success';
      case 'error':   return 'toast-container-error';
      case 'warning': return 'toast-container-warning';
      default:        return '';
    }
  });
}
