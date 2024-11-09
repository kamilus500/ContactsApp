import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { UserDto } from "../models/userDto";
import { LoadingService } from "./loadingService";
import { AuthService } from "./authService";
import { UserHttpService } from "./httpServices/userHttpService";

@Injectable({
    providedIn: 'root'
})
  
export class UserService extends UserHttpService{
    public user$: BehaviorSubject<UserDto|null> = new BehaviorSubject<UserDto|null>(null);

    constructor(private loadingService: LoadingService, private authService: AuthService) {
        super()
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
}
  