import { Component, inject } from '@angular/core';
import { UserService } from '../../services/user/user.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormField, MatLabel, MatInput } from "@angular/material/input";

@Component({
  selector: 'app-user-account',
  imports: [ReactiveFormsModule, MatFormField, MatLabel, MatInput],
  templateUrl: './user-account.html',
  styleUrl: './user-account.scss',
})
export class UserAccount {
  private fb = inject(FormBuilder);
  userService = inject(UserService);
  
  userForm: FormGroup = this.fb.group({
    username: [this.userService.user()?.userName, [Validators.required, Validators.minLength(3)]],
    password: ['****************', [Validators.required, Validators.minLength(6)]]
  });
}
