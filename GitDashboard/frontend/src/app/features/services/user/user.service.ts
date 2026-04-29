import { Injectable, signal } from '@angular/core';
import { User } from '../../models/user.model';

@Injectable({
  providedIn: 'root',
})

export class UserService {
  user = signal<User | null>(null);

  setUser(user: User) {
    this.user.set(user);
  }

  public unsetUser() {
    this.user.set(null);
  }
}
