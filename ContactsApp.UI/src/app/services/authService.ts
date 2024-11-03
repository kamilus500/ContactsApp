import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { LoginRegisterDto } from "../models/loginRegisterDto";
import { LoginRegisterResponse } from "../models/loginRegisterResponse";

@Injectable({
    providedIn: 'root'
})
  
export class AuthService {
    private API_URL : string = 'https://localhost:7239';
    private httpClient: HttpClient = inject(HttpClient);

    public isLogin$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
    public userFullName$: BehaviorSubject<string> = new BehaviorSubject<string>('');

    setUserFullName(value: string): void {
        this.userFullName$.next(value);
    }

    setIsLogin(value: boolean): void {
        this.isLogin$.next(value);
    }

    login(loginDto: LoginRegisterDto) : Observable<LoginRegisterResponse> {
        return this.httpClient.post<LoginRegisterResponse>(`${this.API_URL}/login`, loginDto);
    }

    register(registerDto: FormData): Observable<Object>{
        return this.httpClient.post(`${this.API_URL}/register`, registerDto);
    }
}
  