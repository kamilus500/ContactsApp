import { DOCUMENT } from '@angular/common';
import { inject, Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  private readonly TOKEN_KEY = 'jwtToken';
  private readonly SECRET_KEY = 'bgt5^YHN7*';

  private document: Document = inject(DOCUMENT);

  private localStorage = this.document.defaultView!.localStorage;

  constructor() {

  }

  saveToken(token: string): void {
    let encryptedToken = CryptoJS.AES.encrypt(token, this.SECRET_KEY).toString();
    this.localStorage?.setItem(this.TOKEN_KEY, encryptedToken);
  }

  getToken(): string | null {
    let decryptedToken: string = '';
    let token = this.localStorage.getItem(this.TOKEN_KEY);

    if (token !== null ) {
      let decryptedBytes = CryptoJS.AES.decrypt(token, this.SECRET_KEY);
      decryptedToken = decryptedBytes.toString(CryptoJS.enc.Utf8);
    }

    return decryptedToken;
  }

  removeToken(): void {
    this.localStorage?.removeItem(this.TOKEN_KEY);
  }

  hasToken(): boolean {
    return !!this.getToken();
  }
}
