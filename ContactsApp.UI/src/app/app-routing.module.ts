import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthComponent } from './components/auth/auth.component';
import { AuthGuard } from './guards/authGuard';
import { ContactsComponent } from './components/contacts/contacts.component';

const routes: Routes = [
  { path: 'auth', component: AuthComponent}, 
  { path: 'contacts', component: ContactsComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: 'auth', pathMatch: 'full' },
  { path: '**', redirectTo: 'auth' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
