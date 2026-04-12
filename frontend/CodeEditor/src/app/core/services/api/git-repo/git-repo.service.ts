import { inject, Injectable } from '@angular/core';
import { catchError, EMPTY, filter, from, map, Observable, of, switchMap, tap } from 'rxjs';
import { GitRepoResponse, GitRepoTitleResponse } from '../../../api';
import { GitRepo } from '../../../api/index';
import { BaseServiceApi } from '../base.servce';
import { LocalStorageService } from '../../localstorage/localstorage';
import { STORAGE_KEYS } from '../../../constants/storage-keys.constants';
import { response } from 'express';

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

  // TODO : Watch how to pull all keys with the same prefix
  GetGitReposFromLocalStorage(id: number): Observable<GitRepoResponse | null> {
    const gitRepoKey = STORAGE_KEYS.GIT_REPO + '_' + id;
    let gitRepos = this.localStorageService.get<GitRepoResponse>(gitRepoKey);
    if(gitRepos !== null)
      return of(gitRepos);

    return this.ApiGetGitRepoById(id)
    .pipe(
      tap(response => {
        if(response !== null)
          this.localStorageService.set(gitRepoKey, response);
      }
      )
    );
  }

  GetGitRepoFromLocalStorage(id: number): Observable<GitRepoResponse | null> {
    const gitRepoKey = STORAGE_KEYS.GIT_REPO + '_' + id;

    let gitRepo: GitRepoResponse | null = this.localStorageService.get<GitRepoResponse>(gitRepoKey);

    if(gitRepo !== null)
      return of(gitRepo);

    return this.ApiGetGitRepoById(id)
    .pipe(
      tap(response => {
        if(response !== null)
          this.localStorageService.set(gitRepoKey, response);
      }
    ));
  }

}
