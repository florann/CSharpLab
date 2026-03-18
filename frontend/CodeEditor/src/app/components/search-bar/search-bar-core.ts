import { Component, computed, inject, OnDestroy, OnInit, signal } from '@angular/core';
import { filter, fromEvent, Subscription } from 'rxjs';
import { LocalStorageService } from '../../core/services/localstorage/localstorage';
import { STORAGE_KEYS } from '../../core/constants/storage-keys.constants';
import { GitRepoTitle } from '../../features/services/git-repo-title/git-repo-title';
import { GitRepoTitleResponse } from '../../core/api';

@Component({
  selector: 'app-search-bar-core',
  imports: [],
  templateUrl: './search-bar-core.html',
  styleUrl: './search-bar-core.scss',
})
export class SearchBarCore implements OnInit, OnDestroy {

  private localStorageService = inject(LocalStorageService);
  private listElement: GitRepoTitleResponse[] | null = this.localStorageService.get(STORAGE_KEYS.ALL_GIT_SUMMARY);

  isVisible = signal(false);
  searchText = signal("");

  listDisplayedElement = computed<GitRepoTitleResponse[]>(() => {
    console.log("Dump list el");
    console.log(this.listElement);
    if (!this.listElement || this.searchText().length === 0)
      return [];

    var filtered = this.listElement?.filter(gitRepoTitle => {
      return gitRepoTitle.name?.toLocaleLowerCase().includes(this.searchText().toLocaleLowerCase());
    });

    console.log("filtered");
    console.log(filtered);

    return filtered; 
  });

  onSearch(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.searchText.set(input.value);  // triggers computed automatically ✅
  }

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
