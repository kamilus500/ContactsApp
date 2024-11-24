import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.scss'
})
export class AuthComponent {

  constructor(private translate: TranslateService) {
    
  }

  onFlagClick(country: string): void {
    this.translate.use(country);
  }
}
