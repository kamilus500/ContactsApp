import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { UserDto } from "../../models/userDto";

@Injectable({
    providedIn: 'root'
})
  
export class UserHttpService {
    private API_URL : string = 'https://localhost:7239';
    private httpClient: HttpClient = inject(HttpClient);

    protected update(userDto: FormData): Observable<UserDto> {
        return this.httpClient.put<UserDto>(`${this.API_URL}/UpdateCurrentUser`, userDto);
    }

    protected get(): Observable<UserDto|null> {
        return this.httpClient.get<UserDto|null>(`${this.API_URL}/GetCurrentUser`);
    }
}
  