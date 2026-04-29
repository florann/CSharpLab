import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})

export class ThemeService {
  isDarkTheme = signal(false);

  initTheme(): void {
    this.isDarkTheme.set(localStorage.getItem("isDarkTheme") === "true");
    this.changeDomClass();
  }

  toggleTheme(): void {
    this.isDarkTheme.update(darkTheme => !darkTheme);
    localStorage.setItem("isDarkTheme", this.isDarkTheme().toString());
    this.changeDomClass();
  }

  changeDomClass(): void {
    if(this.isDarkTheme()){
      document.documentElement.classList.add("dark-theme");
    } else {
      document.documentElement.classList.remove("dark-theme");
    }
  }
}
