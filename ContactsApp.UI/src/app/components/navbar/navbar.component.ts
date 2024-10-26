import { Component, Input, OnInit, signal, WritableSignal } from '@angular/core';
import { TokenService } from '../../services/tokenService';
import { Router } from '@angular/router';
import { SharedSignalService } from '../../services/sharedSignalService';
import { LocalStorageService } from '../../services/localStorageService';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent implements OnInit {

  userEmail: WritableSignal<string> = signal('');

  constructor(private tokenService: TokenService, 
    private router: Router,
    private sharedSignalService: SharedSignalService,
  ) {
    
  }
  ngOnInit(): void {
    this.userEmail = this.sharedSignalService.getUserEmail();
  }

  logout(): void {
    this.tokenService.removeToken();
    this.sharedSignalService.setLogin(false);
    this.sharedSignalService.setUserEmail('');
    this.router.navigateByUrl('/');
  }

}
