import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginRegisterDto } from '../../../models/loginRegisterDto';
import { AuthService } from '../../../services/authService';
import { TokenService } from '../../../services/tokenService';
import { Router } from '@angular/router';
import { SharedSignalService } from '../../../services/sharedSignalService';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(private fb: FormBuilder, 
    private tokenService: TokenService,
    private authService: AuthService,
    private sharedSignalService: SharedSignalService,
    private router: Router,
    private messageService: MessageService
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  onSubmit() : void {
    if (this.loginForm.valid) {
      let loginDto = this.loginForm.value as LoginRegisterDto;

      this.authService.login(loginDto)
        .subscribe({
          next: (response) => {
              this.tokenService.saveToken(response.accessToken);
              this.sharedSignalService.setLogin(true);
              this.sharedSignalService.setUserEmail(loginDto.email);              
    
              this.router.navigateByUrl('/contacts');
          },
          error: (error) => {
            if (error.status === 401) {
              this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Login or password is not correct' });
            } else {
              this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Something went wrong' });
            }
          }
        })
    }
  }
}
