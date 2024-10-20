import { Injectable } from "@angular/core";
import { TokenService } from "./tokenService";
import { jwtDecode } from "jwt-decode";

@Injectable({
    providedIn: 'root'
})
  
export class UserService {

    constructor(private tokenService: TokenService) {

    }

    getUserId(): string | null {
        let decryptedToken = this.tokenService.getToken();

        if (decryptedToken === null || decryptedToken === undefined) {
            return null;
        }

        let decodedToken = jwtDecode(decryptedToken);

        console.log(decodedToken);

        return decodedToken.sub as string | null;
    }
}
  