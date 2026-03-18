import { Injectable } from '@angular/core';
import { StorageKey } from '../../constants/storage-keys.constants';


@Injectable({ providedIn: 'root' })
export class LocalStorageService {

  set<T>(key: StorageKey, value: T): void {
    try {
      localStorage.setItem(key, JSON.stringify(value));
    } catch (e) {
      console.error('LocalStorage set error:', e);
    }
  }

  get<T>(key: StorageKey): T | null {
    try {
      const item = localStorage.getItem(key);
      return item ? JSON.parse(item) as T : null;
    } catch (e) {
      console.error('LocalStorage get error:', e);
      return null;
    }
  }

  remove(key: StorageKey): void {
    localStorage.removeItem(key);
  }

  has(key: StorageKey): boolean {
    return localStorage.getItem(key) !== null;
  }
}