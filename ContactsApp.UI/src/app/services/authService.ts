import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { LoginRegisterDto } from "../models/loginRegisterDto";
import { LoginRegisterResponse } from "../models/loginRegisterResponse";
import { AuthHttpService } from "./httpServices/authHttpService";

@Injectable({
    providedIn: 'root'
})
  
export class AuthService extends AuthHttpService {
    public isLogin$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
    public userFullName$: BehaviorSubject<string> = new BehaviorSubject<string>('');
    public imageUrl$ : BehaviorSubject<string> = new BehaviorSubject<string>('');

    setImage(value: string): void {
        this.imageUrl$.next(value);
    }

    setUserFullName(value: string): void {
        this.userFullName$.next(value);
    }

    setIsLogin(value: boolean): void {
        this.isLogin$.next(value);
    }

    registerUser(newUser: FormData): Observable<Object> {
        return this.register(newUser);
    }

    loginUser(loginUser: LoginRegisterDto): Observable<LoginRegisterResponse> {
        return this.login(loginUser);
    }
}
  