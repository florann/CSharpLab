import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthenticationService } from '../../../core/services/authentication.service';
import { Router } from '@angular/router';
import { LoginRequest } from '../../../core/api/types.gen';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatAnchor, MatButton } from "@angular/material/button";

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
      next: (success) => {
        if (success) {
          this.router.navigate(['/codeeditor']);
        }
      },
      error: (error) => {
        console.error('Login failed:', error);
      }
    });
  }
}
