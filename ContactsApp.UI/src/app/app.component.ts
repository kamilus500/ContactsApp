import { Component, computed, signal, WritableSignal } from '@angular/core';
import { TokenService } from './services/tokenService';
import { SharedSignalService } from './services/sharedSignalService';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'ContactsApp.UI';

  isLogin: WritableSignal<boolean>;

  constructor(private tokenService: TokenService, private sharedSignalService: SharedSignalService) {
    if (this.tokenService.hasToken()) {
      this.sharedSignalService.setLogin(true);
    }
    this.isLogin = this.sharedSignalService.getLogin();
  }
}
