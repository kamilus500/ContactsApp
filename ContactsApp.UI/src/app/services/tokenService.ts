import { DOCUMENT } from '@angular/common';
import { inject, Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';
import { LocalStorageService } from './localStorageService';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  private readonly TOKEN_KEY = 'jwtToken';
  private readonly SECRET_KEY = 'bgt5^YHN7*';

  constructor(private localStorageService: LocalStorageService) {

  }

  saveToken(token: string): void {
    let encryptedToken = CryptoJS.AES.encrypt(token, this.SECRET_KEY).toString();
    this.localStorageService.set(encryptedToken, this.TOKEN_KEY);
  }

  getToken(): string | null {
    let decryptedToken: string = '';
    let token = this.localStorageService.get(this.TOKEN_KEY);

    if (token !== null ) {
      let decryptedBytes = CryptoJS.AES.decrypt(token, this.SECRET_KEY);
      decryptedToken = decryptedBytes.toString(CryptoJS.enc.Utf8);
    }

    return decryptedToken;
  }

  removeToken(): void {
    this.localStorageService.remove(this.TOKEN_KEY);
  }

  hasToken(): boolean {
    return !!this.getToken();
  }
}
