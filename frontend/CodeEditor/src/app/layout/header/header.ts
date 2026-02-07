import { Component, computed, inject, signal } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { Router, RouterLink } from '@angular/router';
import { ThemeService } from '../../core/services/theme/theme.service';
import { UserService } from '../../features/services/user/user.service';
import { HeaderButton } from '../../components/header-button/header-button';

@Component({
  selector: 'app-header',
  imports: [
    HeaderButton,
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
  router = inject(Router);

  isMenuOpen = false;
  isConnected = computed(() => {
    return this.userService.user != null;
  });

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

  pageLogin() {
    this.router.navigate(["/login"]);
  }

}
