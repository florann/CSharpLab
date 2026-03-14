import { Injectable } from '@angular/core';
import { catchError, from, map, Observable } from 'rxjs';
import { GitRepoTitleResponse } from '../../../api';
import { GitRepo } from '../../../api/index';
import { error } from 'console';
import { BaseServiceApi } from '../base.servce';

@Injectable({
  providedIn: 'root',
})

export class GitRepoService extends BaseServiceApi {
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
}
