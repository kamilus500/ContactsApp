import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRegisterDto } from '../models/LoginRegisterDto';
import { Observable } from 'rxjs';
import { LoginResponse } from '../models/LoginResponse';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private API_URL : string = '';

  constructor(private httpClient: HttpClient) {

  }

  login(loginDto: LoginRegisterDto) : Observable<LoginResponse> {
    return this.httpClient.post<LoginResponse>(`${this.API_URL}/login`, loginDto);
  }

  register(registerDto: LoginRegisterDto) : void {
    this.httpClient.post(`${this.API_URL}/register`, registerDto);
  }
}
