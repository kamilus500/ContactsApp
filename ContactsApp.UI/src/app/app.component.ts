import { Component, signal, WritableSignal } from '@angular/core';
import { TokenService } from './services/tokenService';
import { SharedSignalService } from './services/sharedSignalService';
import { LocalStorageService } from './services/localStorageService';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'ContactsApp.UI';

  isLogin: WritableSignal<boolean>;

  constructor(private tokenService: TokenService, private sharedSignalService: SharedSignalService, private localStorageService: LocalStorageService) {
    if (this.tokenService.hasToken()) {
      this.sharedSignalService.setLogin(true);

      let email = this.localStorageService.get('email')
      this.sharedSignalService.setUserEmail(email);
    }

    this.isLogin = this.sharedSignalService.getLogin();
  }
}
