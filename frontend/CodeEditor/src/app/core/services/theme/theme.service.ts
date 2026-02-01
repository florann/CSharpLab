import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})

export class ThemeService {
  isDarkTheme = signal(false);

  toggleTheme() {
    this.isDarkTheme.update(darkTheme => !darkTheme);

    if(this.isDarkTheme()){
      document.documentElement.classList.add("dark-theme");
    } else {
      document.documentElement.classList.remove("dark-theme");
    }
  }
}
