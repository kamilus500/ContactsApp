import { inject, Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { CurrentUser } from "../models/currentUser";

@Injectable({
    providedIn: 'root'
})
  
export class UserService {
    private API_URL : string = 'https://localhost:7239';
    private httpClient: HttpClient = inject(HttpClient);

    getUserFullName(): Observable<CurrentUser> {
        return this.httpClient.get<CurrentUser>(`${this.API_URL}/getUser`);
    }
}
  