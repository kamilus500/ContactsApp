import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/auth/login/login.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { AuthComponent } from './components/auth/auth.component';
import { TokenService } from './services/tokenService';
import { ContactsComponent } from './components/contacts/contacts.component';
import { MessageModule } from 'primeng/message';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { CardModule } from 'primeng/card';
import { AuthService } from './services/authService';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NavbarComponent } from './components/navbar/navbar.component';
import { AvatarModule } from 'primeng/avatar';
import { ContactService } from './services/contactService';
import { AuthInterceptor } from './interceptors/authInterceptor';
import { TabViewModule } from 'primeng/tabview';
import { ToastModule } from 'primeng/toast';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DialogService } from 'primeng/dynamicdialog';
import { AddContactComponent } from './components/add-contact/add-contact.component';
import { LocalStorageService } from './services/localStorageService';
import { FilterService } from 'primeng/api';
import { LoadingSpinnerComponent } from './components/loading-spinner/loading-spinner.component';
import { EditContactComponent } from './components/edit-contact/edit-contact.component';
import { FileUploadModule } from 'primeng/fileupload';
import { UserDetailsComponent } from './components/user-details/user-details.component';
import { UserService } from './services/userService';
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    AuthComponent,
    ContactsComponent,
    NavbarComponent,
    AddContactComponent,
    LoadingSpinnerComponent,
    EditContactComponent,
    UserDetailsComponent
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    AppRoutingModule,
    MessageModule,
    ReactiveFormsModule,
    FormsModule,
    ButtonModule,
    InputTextModule,
    CardModule,
    HttpClientModule,
    AvatarModule,
    TabViewModule,
    ToastModule,
    ConfirmDialogModule,
    FileUploadModule
  ],
  providers: [TokenService, ContactService, AuthService, MessageService, ConfirmationService, DialogService, LocalStorageService, FilterService, UserService, {
    provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true,
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
