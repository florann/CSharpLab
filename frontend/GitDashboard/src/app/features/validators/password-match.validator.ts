import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function confirmPasswordValidator(passwordField: string): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    if (!control.parent) {
      return null;
    }
    
    const password = control.parent.get(passwordField);
    const confirmPassword = control;

    if (!password || !confirmPassword) {
      return null;
    }

    return password.value === confirmPassword.value 
      ? null 
      : { passwordMismatch: true };
  };
}