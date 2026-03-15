import { TestBed } from '@angular/core/testing';

import { GitRepoTitle } from './git-repo-title';

describe('GitRepoTitle', () => {
  let service: GitRepoTitle;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GitRepoTitle);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
