import { Component, Input, WritableSignal } from '@angular/core';
import { TokenService } from '../../services/tokenService';
import { Router } from '@angular/router';
import { SharedSignalService } from '../../services/sharedSignalService';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {

  @Input() userEmail: string = '';

  constructor(private tokenService: TokenService, 
    private router: Router,
    private sharedSignalService: SharedSignalService
  ) {
    
  }

  logout(): void {
    this.tokenService.removeToken();
    this.sharedSignalService.setLogin(false);
    this.router.navigateByUrl('/');
  }

}
