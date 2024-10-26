import { Injectable, signal, WritableSignal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SharedSignalService {
  private isLogin: WritableSignal<boolean> = signal(false);
  private userEmail: WritableSignal<string> = signal('');

  getLogin(): WritableSignal<boolean> {
    return this.isLogin;
  }

  setLogin(value: boolean): void {
    this.isLogin.set(value);
  }

  getUserEmail(): WritableSignal<string> {
    return this.userEmail;
  }

  setUserEmail(value: string): void {
    this.userEmail.set(value);
  }
}
