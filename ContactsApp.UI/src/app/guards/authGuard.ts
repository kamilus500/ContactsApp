import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { TokenService } from '../services/tokenService';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router, private tokenService: TokenService) { }

  canActivate(): boolean {
    const isLoggedIn = this.tokenService.hasToken();
    if (!isLoggedIn) {
      this.router.navigate(['login']);
      return false;
    }
    return true;
  }
}