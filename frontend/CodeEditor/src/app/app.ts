import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Dummy } from './components/dummy/dummy';
import { CodeEditor } from './components/code-editor/code-editor';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Dummy, CodeEditor],
  templateUrl: './app.html',
  styleUrl: './app.css'
})

export class App {
  protected readonly title = signal('CodeEditor');
}
