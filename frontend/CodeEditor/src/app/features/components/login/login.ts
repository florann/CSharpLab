import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthenticationService } from '../../../core/services/authentication/authentication.service';
import { Router } from '@angular/router';
import { LoginRequest } from '../../../core/api/types.gen';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatAnchor, MatButton } from "@angular/material/button";
import { UserService } from '../../services/user/user.service';
import { User } from '../../models/user.model';
import { ToastService } from '../../../core/services/toast/toast.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatAnchor, MatButton],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {
  private fb = inject(FormBuilder);
  private authService = inject(AuthenticationService);
  private router = inject(Router);
  private userService = inject(UserService);
  private toastService = inject(ToastService);

  loginForm: FormGroup = this.fb.group({
    username: ['', [Validators.required, Validators.minLength(3)]],
    password: ['', [Validators.required, Validators.minLength(6)]]
  });

  login(): void {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    const loginRequest: LoginRequest = {
      userName: this.loginForm.value.username,
      password: this.loginForm.value.password
    };

    this.authService.ApiAuthLoginPost(loginRequest).subscribe({
      next: (userResponse) => {
        if (userResponse) {
          if (!userResponse.id || !userResponse.userName || !userResponse.guid) {
            throw new Error('Invalid user response: missing required fields');
          }

          const user: User = {
            id: userResponse.id,
            userName: userResponse.userName,
            guid: userResponse.guid
          }

          this.userService.setUser(user);
          this.router.navigate(['/dashboard']);
        }
        else{
          console.log("No user found");
        }
      },
      error: (error) => {
        if(error.status === 401) {
          this.toastService.show("Invalid username or password")
        } 
      }
    });
  }

  navigateToCreateAccount(): void {
    this.router.navigate(['createaccount']);
  }
}
