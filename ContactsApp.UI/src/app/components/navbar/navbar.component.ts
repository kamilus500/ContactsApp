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

  userFullName$: BehaviorSubject<string> = new BehaviorSubject<string>('');
  imageUrl$ : BehaviorSubject<string> = new BehaviorSubject<string>('');

  constructor(private tokenService: TokenService,
    private localStorageService: LocalStorageService,
    private router: Router,
    private authService: AuthService,
  ) {
    
  }

  ngOnInit(): void {
    this.userFullName$ = this.authService.userFullName$;
    this.imageUrl$ = this.authService.imageUrl$;
  }

  logout(): void {
    this.localStorageService.remove('userFullName');
    this.localStorageService.remove('image');
    this.tokenService.removeToken();
    this.authService.setIsLogin(false);
    this.authService.setUserFullName('');
    this.authService.setImage('');
    this.router.navigateByUrl('/');
  }

}
