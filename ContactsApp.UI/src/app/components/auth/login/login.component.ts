import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginRegisterDto } from '../../../models/loginRegisterDto';
import { AuthService } from '../../../services/authService';
import { TokenService } from '../../../services/tokenService';
import { Router } from '@angular/router';
import { SharedSignalService } from '../../../services/sharedSignalService';
import { jwtDecode } from 'jwt-decode';
import { UserService } from '../../../services/userService';
import { CurrentUser } from '../../../models/currentUser';

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
    private userService: UserService,
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
          this.sharedSignalService.setLogin(true);
          
          this.userService.getUserFullName()
            .subscribe((x: CurrentUser) => {
              if (x.email) {
                this.sharedSignalService.setUserEmail(x.email);
              }
            })

          this.router.navigateByUrl('/contacts');
        }
      })
    }
  }
}
