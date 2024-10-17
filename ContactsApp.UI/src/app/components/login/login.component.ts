import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { TokenService } from '../../services/token.service';
import { LoginRegisterDto } from '../../models/LoginRegisterDto';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [AuthService, TokenService],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  constructor(private authService: AuthService, private tokenService: TokenService) {
    
  }

  login(loginDto: LoginRegisterDto): void {
    this.authService.login(loginDto).subscribe(response => {
      this.tokenService.saveToken(response.token);
    })
  }
}
