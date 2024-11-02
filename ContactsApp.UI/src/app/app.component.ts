import { Component } from '@angular/core';
import { TokenService } from './services/tokenService';
import { LocalStorageService } from './services/localStorageService';
import { Observable } from 'rxjs';
import { AuthService } from './services/authService';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'ContactsApp.UI';

  isLogin$: Observable<boolean>

  constructor(private tokenService: TokenService, private localStorageService: LocalStorageService, private authService: AuthService) {
    this.isLogin$ = this.authService.isLogin$;

    if (this.tokenService.hasToken()) {
      this.authService.setIsLogin(true);
      let userFullname = this.localStorageService.get('userFullName')
      this.authService.setUserFullName(userFullname);
    }
  }
}
