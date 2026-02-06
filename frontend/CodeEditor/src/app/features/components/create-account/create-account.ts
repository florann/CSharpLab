import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthenticationService } from '../../../core/services/authentication/authentication.service';
import { Router, RouterLink } from '@angular/router';
import { confirmPasswordValidator } from '../../validators/password-match.validator';
import { CreateAccountRequest } from '../../../core/api';

@Component({
  selector: 'app-create-account',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './create-account.html',
  styleUrl: './create-account.css',
})
export class CreateAccount {
  
  private fb = inject(FormBuilder);
  private authService = inject(AuthenticationService);
  private router = inject(Router);

  createAccountForm: FormGroup = this.fb.group({
    username: ['', [Validators.required, Validators.minLength(3)]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    confirmPassword: ['', [Validators.required, confirmPasswordValidator("password")]]
  });

  isLoading = signal(false);
  errorMessage = signal<string | null>(null);

  onSubmit() {
    this.isLoading.set(true);
    this.errorMessage.set(null);

    const createAccountRequest: CreateAccountRequest = {
      userName: this.createAccountForm.value.username,
      password: this.createAccountForm.value.password,
      confirmPassword: this.createAccountForm.value.confirmPassword
    };

    console.log("Dump request");
    console.log(createAccountRequest);

    this.authService.ApiCreateAccount(createAccountRequest).subscribe({
      next: (success) => {
        this.isLoading.set(false);
        if (success) {
          this.router.navigate(['/login']);
        }
      },
      error: (error) => {
        this.isLoading.set(false);
        this.errorMessage.set('Could not create the account');
        console.error('Creation failed:', error);
      }
    });
  }

  get usernameControl() {
    return this.createAccountForm.get("username");
  }

  get passwordControl() {
    return this.createAccountForm.get("password");
  }

  get confirmPasswordControl() {
    return this.createAccountForm.get("confirmPassword");
  }
}
