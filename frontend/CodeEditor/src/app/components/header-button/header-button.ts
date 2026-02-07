import { Component, input, output  } from '@angular/core';
import { MatButtonModule, MatAnchor } from '@angular/material/button';
import { MatIconModule, MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-header-button',
  imports: [MatIcon, MatAnchor, MatButtonModule, MatIconModule],
  templateUrl: './header-button.html',
  styleUrl: './header-button.css',
})
export class HeaderButton {
  type = input<'button' | 'submit' | 'reset'>('button');
  variant = input<'basic' | 'raised' | 'flat' | 'stroked' | 'icon'>('raised');
  color = input<'primary' | 'accent' | 'warn' | undefined>('primary');
  disabled = input<boolean>(false);
  icon = input<string>('');

  clicked = output<MouseEvent>();
  
  handleClick(event: MouseEvent) {
    if (!this.disabled()) {
      this.clicked.emit(event);
    }
  }
}
