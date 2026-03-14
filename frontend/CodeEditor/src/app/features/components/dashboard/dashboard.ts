import { Component, inject, OnInit } from '@angular/core';
import { GitRepoResponse, GitRepoTitleResponse } from '../../../core/api';
import { GitRepoService } from '../../../core/services/api/git-repo/git-repo.service';
import { ToastService } from '../../../core/services/toast/toast.service';

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class Dashboard implements OnInit {
  ngOnInit(): void {
    this.GetAllGitSummary();
  }

  private toastService = inject(ToastService);
  private gitRepoService = inject(GitRepoService);

  GetAllGitSummary(): void {
    this.gitRepoService.ApiGetAllGitRepoSummary()
    .subscribe({
      next: (response) => {
          if(response == null){
            this.toastService.show("No git repos summary to retrieve", "warning");
          }

          this.toastService.show("Git repos summary retrieved", "success");
      },
      error: (error) => {
        console.log(error);
        this.toastService.show("Error loading git repos summary", "error");
      }
    })
  }
}
