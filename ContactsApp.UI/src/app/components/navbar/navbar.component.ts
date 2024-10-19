import { Component } from '@angular/core';
import { TokenService } from '../../services/tokenService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {

  constructor(private tokenService: TokenService, private router: Router) {

  }

  logout(): void {
    this.tokenService.removeToken();

    this.router.navigateByUrl('/');
  }

}
