import { Component } from '@angular/core';
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
  selectedFile: File | null = null;

  constructor(private fb: FormBuilder, private authService: AuthService, private messageService: MessageService, private router: Router, private loadingService: LoadingService) {
    this.registerForm = this.fb.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required]
    }, { validator: this.passwordsMatchValidator });
  }

  passwordsMatchValidator(form: FormGroup) {
    return form.get('password')?.value === form.get('confirmPassword')?.value ? null : { mismatch: true };
  }
  
  onFileSelect(event: any) {
    const input = event.target as HTMLInputElement;
    if (input.files) {
      this.selectedFile = input.files[0];
    }
  }

  onSubmit(): void {
    if (this.registerForm.valid) {
      let image = this.selectedFile!;

      const formData = new FormData();
      formData.append('email', this.registerForm.get('email')?.value);
      formData.append('password', this.registerForm.get('password')?.value);
      formData.append('firstName', this.registerForm.get('firstName')?.value);
      formData.append('lastName', this.registerForm.get('lastName')?.value);
      formData.append('Image', image, 'image');

      this.loadingService.show();

      this.authService.registerUser(formData)
        .subscribe({
          next: (response) => {
            this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Registered successfully' });
            this.loadingService.hide();
            this.registerForm.reset();
            this.router.navigateByUrl('auth');
          },
          error: (error) => {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Something went wrong' });
            this.loadingService.hide();
          }
        });
    }
  }
}
