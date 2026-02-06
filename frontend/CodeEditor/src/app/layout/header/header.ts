import { Component, inject, signal } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { HeaderLogin } from '../../components/layout/header-login/header-login';
import { ThemeService } from '../../core/services/theme/theme.service';
import { UserService } from '../../features/services/user/user.service';

@Component({
  selector: 'app-header',
  imports: [
    HeaderLogin, 
    MatToolbarModule, 
    RouterLink, 
    MatButtonModule,
    MatIcon
  ],
  standalone: true,
  templateUrl: './header.html',
  styleUrl: './header.scss',
})
export class Header {
  themeService = inject(ThemeService);
  userService = inject(UserService);

  isMenuOpen = false;
  isConnected = false;

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }

  closeMenu() {
    this.isMenuOpen = false;
  }

  isDarkTheme(): boolean {
    return this.themeService.isDarkTheme();
  }
  
  toggleTheme() {
    this.themeService.toggleTheme();
  }
}
