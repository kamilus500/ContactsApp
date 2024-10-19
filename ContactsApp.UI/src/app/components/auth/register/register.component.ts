import { Component } from '@angular/core';
import { LoginRegisterDto } from '../../../models/loginRegisterDto';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../../services/authService';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  registerForm: FormGroup;
  
  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required],
    }, { validator: this.passwordsMatchValidator });
  }

  passwordsMatchValidator(form: FormGroup) {
    return form.get('password')?.value === form.get('confirmPassword')?.value ? null : { mismatch: true };
  }

  onSubmit(): void {
    if (this.registerForm.valid) {
      let registerDto: LoginRegisterDto = this.registerForm.value;
      
      this.authService.register(registerDto)
        .subscribe();
    }
  }
}
