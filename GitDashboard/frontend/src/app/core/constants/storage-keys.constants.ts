export const STORAGE_KEYS = {
  ALL_GIT_SUMMARY:              'all_git_rep_summary',
  GIT_REPO:              'git_repo',
} as const;


export type StorageKey = typeof STORAGE_KEYS[keyof typeof STORAGE_KEYS];