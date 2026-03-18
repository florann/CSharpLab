import { inject, Injectable } from '@angular/core';
import { catchError, from, map, Observable } from 'rxjs';
import { GitRepoResponse, GitRepoTitleResponse } from '../../../api';
import { GitRepo } from '../../../api/index';
import { BaseServiceApi } from '../base.servce';
import { LocalStorageService } from '../../localstorage/localstorage';

@Injectable({
  providedIn: 'root',
})

export class GitRepoService extends BaseServiceApi {
  localStorageService = inject(LocalStorageService);

  ApiGetAllGitRepoSummary(): Observable<GitRepoTitleResponse[] | null>  {
    return from(GitRepo.getApiGitRepoGetAllGitRepoSummary(
      {
         credentials: 'include',
      }
    ))
    .pipe(
      map(response => {
        if(!response.data)
          throw response.response;

        if(!Array.isArray(response.data)){
          return null;
        }

        return response.data;
      }),
      catchError(this.handleError)
    );
  }

  ApiGetGitRepoById(id : number): Observable<GitRepoResponse | null>  {
    return from(GitRepo.getApiGitRepoByGitRepoId(
      {
        credentials: 'include',
        path: {
          gitRepoId: id
        }
      }
    ))
    .pipe(
      map(response => {
        if(!response.data)
          throw response.response;

        return response.data;
      }),
      catchError(this.handleError)
    );
  }

  GetGitReposFromLocalStorage(): Observable<GitRepoResponse[] | null> {
    this.localStorageService.get()
  }

}
