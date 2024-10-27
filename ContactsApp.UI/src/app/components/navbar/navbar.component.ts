import { Component, OnInit} from '@angular/core';
import { TokenService } from '../../services/tokenService';
import { Router } from '@angular/router';
import { AuthService } from '../../services/authService';
import { BehaviorSubject } from 'rxjs';
import { LocalStorageService } from '../../services/localStorageService';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent implements OnInit {

  userEmail$: BehaviorSubject<string> = new BehaviorSubject<string>('');

  constructor(private tokenService: TokenService,
    private localStorageService: LocalStorageService,
    private router: Router,
    private authService: AuthService,
  ) {
    
  }
  ngOnInit(): void {
    this.userEmail$ = this.authService.userEmail$;
  }

  logout(): void {
    this.localStorageService.remove('email');
    this.tokenService.removeToken();
    this.authService.setIsLogin(false);
    this.authService.setUserEmail('');
    this.router.navigateByUrl('/');
  }

}
