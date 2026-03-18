export const STORAGE_KEYS = {
  ALL_GIT_SUMMARY:              'all_git_rep_summary',
} as const;


export type StorageKey = typeof STORAGE_KEYS[keyof typeof STORAGE_KEYS];