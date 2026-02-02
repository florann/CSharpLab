import { Component, inject, OnInit, signal } from '@angular/core';
import { Layout } from './layout/layout';
import { ThemeService } from './core/services/theme/theme.service';

@Component({
  selector: 'app-root',
  imports: [Layout],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})

export class App implements OnInit {
  
  protected readonly title = signal('CodeEditor');
  themeService = inject(ThemeService);
  
  ngOnInit(): void {
    this.themeService.initTheme();
  }
  
}
