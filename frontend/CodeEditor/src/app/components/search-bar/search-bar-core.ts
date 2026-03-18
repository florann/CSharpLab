import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { filter, fromEvent, Subscription } from 'rxjs';

@Component({
  selector: 'app-search-bar-core',
  imports: [],
  templateUrl: './search-bar-core.html',
  styleUrl: './search-bar-core.scss',
})
export class SearchBarCore implements OnInit, OnDestroy {
  isVisible = signal(false);
  private sub!: Subscription;

  ngOnInit(): void {
    this.sub = fromEvent<KeyboardEvent>(document, 'keydown').pipe(
      filter(e => e.ctrlKey && e.key === 'k') 
    ).subscribe(e => {
      console.log("Event fired");
      e.preventDefault();
      this.isVisible.set(!this.isVisible());
    })
  }
  
  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
}
