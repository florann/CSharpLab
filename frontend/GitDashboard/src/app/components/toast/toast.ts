import { Component, computed, input, OnInit, output, signal } from '@angular/core';
import { NgClass } from  '@angular/common';

@Component({
  selector: 'app-toast',
  imports: [NgClass],
  templateUrl: './toast.html',
  styleUrl: './toast.scss',
})
export class Toast implements OnInit  {
  ngOnInit(): void {
    setTimeout(() => {
      this.dismiss.emit();
    }, 1000)
  }

  message = input.required<string>();
  type = input.required<'success' | 'error' | 'warning'>();
  isVisible = signal(true);

  dismiss = output<void>();

  toast_class =  computed(() => {
    switch (this.type()) {
      case 'success': return 'toast-container-success';
      case 'error':   return 'toast-container-error';
      case 'warning': return 'toast-container-warning';
      default:        return '';
    }
  });

  leave() {
    console.log("Leave");
    this.isVisible.set(false);
  }

}
