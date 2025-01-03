import { Component, OnInit} from '@angular/core';
import { TokenService } from '../../services/tokenService';
import { Router } from '@angular/router';
import { AuthService } from '../../services/authService';
import { BehaviorSubject } from 'rxjs';
import { LocalStorageService } from '../../services/localStorageService';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { UserDetailsComponent } from '../user-details/user-details.component';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent implements OnInit {

  userFullName$: BehaviorSubject<string> = new BehaviorSubject<string>('');
  imageUrl$ : BehaviorSubject<string> = new BehaviorSubject<string>('');
  ref: DynamicDialogRef | undefined;
  
  constructor(private tokenService: TokenService,
    private localStorageService: LocalStorageService,
    private router: Router,
    private authService: AuthService,
    private dialogService: DialogService,
    private translate: TranslateService
  ) {
    
  }

  ngOnInit(): void {
    this.userFullName$ = this.authService.userFullName$;
    this.imageUrl$ = this.authService.imageUrl$;
  }

  onFlagClick(country: string): void {
    this.translate.use(country);
  }

  showDetails(): void {
    const header = this.translate.instant('user_details_text');
    this.ref = this.dialogService.open(UserDetailsComponent, {
      modal: true,
      header: header,
      contentStyle: { overflow: 'auto' },
      width: '25%'
    });
  }

  logout(): void {
    this.localStorageService.remove('userFullName');
    this.localStorageService.remove('image');
    this.tokenService.removeToken();
    this.authService.setIsLogin(false);
    this.authService.setUserFullName('');
    this.authService.setImage('');
    this.router.navigateByUrl('/');
  }

}
