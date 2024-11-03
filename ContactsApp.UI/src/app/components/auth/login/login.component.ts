import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginRegisterDto } from '../../../models/loginRegisterDto';
import { AuthService } from '../../../services/authService';
import { TokenService } from '../../../services/tokenService';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { LocalStorageService } from '../../../services/localStorageService';
import { LoadingService } from '../../../services/loadingService';
import { LoginRegisterResponse } from '../../../models/loginRegisterResponse';

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
    private localStorageService: LocalStorageService,
    private router: Router,
    private messageService: MessageService,
    private loadingService: LoadingService
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  onSubmit() : void {
    if (this.loginForm.valid) {
      this.loadingService.show();

      let loginDto = this.loginForm.value as LoginRegisterDto;

      this.authService.login(loginDto)
        .subscribe({
          next: (response: LoginRegisterResponse) => {
              let imageUrl = 'data:image/png;base64,' + response.userImage;

              this.tokenService.saveToken(response.token);
              this.authService.setIsLogin(true);
              this.authService.setUserFullName(response.fullName);
              this.authService.setImage(imageUrl);
              this.localStorageService.set(imageUrl, 'image')
              this.localStorageService.set(response.fullName, 'userFullName');

              this.loadingService.hide();

              this.router.navigateByUrl('/contacts');
          },
          error: (error) => {
            if (error.status === 401) {
              this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Login or password is not correct' });
            } else {
              this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Something went wrong' });
            }

            this.loadingService.hide();
          }
        })
    }
  }
}
