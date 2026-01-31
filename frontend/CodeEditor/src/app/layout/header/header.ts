import { Component, signal } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { HeaderLogin } from '../../components/layout/header-login/header-login';

@Component({
  selector: 'app-header',
  imports: [
    HeaderLogin, 
    MatToolbarModule, 
    RouterLink, 
    MatButtonModule,
    MatIcon
  ],
  templateUrl: './header.html',
  styleUrl: './header.scss',
})
export class Header {
  isMenuOpen = false;

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }

  closeMenu() {
    this.isMenuOpen = false;
  }
}
