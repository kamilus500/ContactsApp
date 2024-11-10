import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { map, Observable } from "rxjs";
import { LoginRegisterDto } from "../../models/loginRegisterDto";
import { LoginRegisterResponse } from "../../models/loginRegisterResponse";

@Injectable({
    providedIn: 'root'
})
  
export class AuthHttpService {
    private API_URL : string = 'https://localhost:7239';
    private httpClient: HttpClient = inject(HttpClient);

    protected login(loginDto: LoginRegisterDto) : Observable<LoginRegisterResponse> {
        return this.httpClient.post<LoginRegisterResponse>(`${this.API_URL}/login`, loginDto);
    }

    protected register(registerDto: FormData): Observable<Object>{
        return this.httpClient.post(`${this.API_URL}/register`, registerDto);
    }

    protected emailVeryfication(email: string): Observable<boolean> {
        return this.httpClient.get<boolean>(`${this.API_URL}/emailveryfication/${email}`)
            .pipe(map(response => response));
    }
}
  