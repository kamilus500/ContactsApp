import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { LoginRegisterDto } from "../models/loginRegisterDto";
import { LoginRegisterResponse } from "../models/loginRegisterResponse";

@Injectable({
    providedIn: 'root'
})
  
export class AuthService {
    private API_URL : string = 'https://localhost:7239';
    private httpClient: HttpClient = inject(HttpClient);

    login(loginDto: LoginRegisterDto) : Observable<LoginRegisterResponse> {
        return this.httpClient.post<LoginRegisterResponse>(`${this.API_URL}/login`, loginDto);
    }

    register(registerDto: LoginRegisterDto): Observable<Object>{
        return this.httpClient.post(`${this.API_URL}/register`, registerDto);
    }
}
  