import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginRegisterDto } from '../../../models/loginRegisterDto';
import { AuthService } from '../../../services/authService';
import { TokenService } from '../../../services/tokenService';
import { Router } from '@angular/router';

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
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  onSubmit() : void {
    if (this.loginForm.valid) {
      let loginDto = this.loginForm.value as LoginRegisterDto;

      this.authService.login(loginDto).subscribe(response => {
        if (response.accessToken !== '') {
          this.tokenService.saveToken(response.accessToken);
          this.router.navigateByUrl('/contacts');
        }
      })
    }
  }
}
