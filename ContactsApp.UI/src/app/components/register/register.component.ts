import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { LoginRegisterDto } from '../../models/LoginRegisterDto';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [AuthService],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  constructor(private authService: AuthService) {}

  register(registerDto: LoginRegisterDto): void {
    this.authService.register(registerDto);
  }
}
