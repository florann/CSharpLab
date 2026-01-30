import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CodeEditor } from './features/components/code-editor/code-editor';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})

export class App {
  protected readonly title = signal('CodeEditor');
}
