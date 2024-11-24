import { Component } from '@angular/core';
import { TokenService } from './services/tokenService';
import { LocalStorageService } from './services/localStorageService';
import { Observable } from 'rxjs';
import { AuthService } from './services/authService';
import { TranslateService  } from '@ngx-translate/core';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'ContactsApp.UI';

  isLogin$: Observable<boolean>

  constructor(private tokenService: TokenService, private localStorageService: LocalStorageService, private authService: AuthService, private translate: TranslateService) {
    this.translate.setDefaultLang('pl');
    this.translate.use('pl');
    
    this.isLogin$ = this.authService.isLogin$;

    if (this.tokenService.hasToken()) {
      this.authService.setIsLogin(true);
      let userFullname = this.localStorageService.get('userFullName')
      this.authService.setUserFullName(userFullname);
      let imageUrl = this.localStorageService.get('image');
      this.authService.setImage(imageUrl);
    }
  }

  changeLanguage(language: string) {
    this.translate.use(language);
  }
}
