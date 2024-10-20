import { Injectable, signal, WritableSignal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SharedSignalService {
  private isLogin: WritableSignal<boolean> = signal(false);
  
  getLogin(): WritableSignal<boolean> {
    return this.isLogin;
  }

  setLogin(value: boolean): void {
    this.isLogin.set(value);
  }
}
