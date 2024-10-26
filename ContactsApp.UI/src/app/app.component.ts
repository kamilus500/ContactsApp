import { Component, computed, signal, WritableSignal } from '@angular/core';
import { TokenService } from './services/tokenService';
import { SharedSignalService } from './services/sharedSignalService';
import { UserService } from './services/userService';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'ContactsApp.UI';

  isLogin: WritableSignal<boolean>;
  userEmail: WritableSignal<string> = signal('');
  constructor(private tokenService: TokenService, private sharedSignalService: SharedSignalService, private userService: UserService) {
    if (this.tokenService.hasToken()) {
      this.sharedSignalService.setLogin(true);
      this.userService.getUserFullName()
        .subscribe(x => {
          this.sharedSignalService.setUserEmail(x.email)
          this.userEmail = this.sharedSignalService.getUserEmail();
        });
    }
    this.isLogin = this.sharedSignalService.getLogin();
  }
}
