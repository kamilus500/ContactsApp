import { Component } from '@angular/core';
import { LoginRegisterDto } from '../../../models/loginRegisterDto';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../../services/authService';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import { LoadingService } from '../../../services/loadingService';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  registerForm: FormGroup;
  
  constructor(private fb: FormBuilder, private authService: AuthService, private messageService: MessageService, private router: Router, private loadingService: LoadingService) {
    this.registerForm = this.fb.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
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
      let email = this.registerForm.get('email')?.value;
      let password = this.registerForm.get('password')?.value;
      let firstName = this.registerForm.get('firstName')?.value;
      let lastName = this.registerForm.get('lastName')?.value;

      let registerDto: LoginRegisterDto = {
        email: email,
        password: password,
        firstName: firstName,
        lastName: lastName
      };
      
      this.loadingService.show();

      this.authService.register(registerDto)
        .subscribe({
          next: (response) => {
            this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Registered successfully' });
            this.loadingService.hide();
            this.router.navigateByUrl('auth');
          },
          error: (error) => {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Something went wrong' });
          }
        });
    }
  }
}
