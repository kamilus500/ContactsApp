import { AbstractControl, ValidationErrors, AsyncValidatorFn } from '@angular/forms';
import { map } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { AuthService } from '../services/authService';

export function emailUniqueValidator(authService: AuthService): AsyncValidatorFn {
  return (control: AbstractControl): Observable<ValidationErrors | null> => {
    if (!control.value || control.hasError('email')) {
      return of(null);
    }
    return authService.isEmailUnique(control.value).pipe(
      map(isTaken => (isTaken ? { emailTaken: true } : null))
    );
  };
}