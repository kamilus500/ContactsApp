import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  private readonly TOKEN_KEY = 'auth-token';
  constructor() { }

  saveToken(token: string): void {
    window.localStorage.setItem(this.TOKEN_KEY, token);
  }

  getToken(): string | null {
    return window.localStorage.getItem(this.TOKEN_KEY);
  }

  removeToken(): void {
    window.localStorage.removeItem(this.TOKEN_KEY);
  }

  hasToken(): boolean {
    return !!this.getToken();
  }
}
