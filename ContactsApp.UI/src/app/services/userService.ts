import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { UserDto } from "../models/userDto";
import { Form } from "@angular/forms";
import { LoadingService } from "./loadingService";
import { AuthService } from "./authService";

@Injectable({
    providedIn: 'root'
})
  
export class UserService {
    private API_URL : string = 'https://localhost:7239';
    private httpClient: HttpClient = inject(HttpClient);

    public user$: BehaviorSubject<UserDto|null> = new BehaviorSubject<UserDto|null>(null);

    constructor(private loadingService: LoadingService, private authService: AuthService) {

    }

    getUser(): void {
        this.get()
            .subscribe({
                next: (user) => {
                    this.user$.next(user);
                },
                error: (error) => {
                    console.log('Error when loading contacts', error)
                }
            })

    }

    updateUser(userDto: FormData): void {
        this.update(userDto)
            .subscribe({
                next: (user) => {
                    this.user$.next(user);
                    let imageUrl = 'data:image/png;base64,' + user.image;
                    this.authService.setImage(imageUrl)
                    this.authService.setUserFullName(`${user.firstName} ${user.lastName}`)
                },
                error: (error) => {
                    this.loadingService.hide();
                    console.log('Error when loading contacts', error)
                }
            })
    }

    private update(userDto: FormData): Observable<UserDto> {
        return this.httpClient.put<UserDto>(`${this.API_URL}/UpdateCurrentUser`, userDto);
    }

    private get(): Observable<UserDto|null> {
        return this.httpClient.get<UserDto|null>(`${this.API_URL}/GetCurrentUser`);
    }
}
  