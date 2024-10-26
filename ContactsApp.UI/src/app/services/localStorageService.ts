import { Injectable } from "@angular/core"

@Injectable({
    providedIn: 'root'
})

export class LocalStorageService {

    get(key: string): string {
        return localStorage.getItem(key) as string;
    }

    set(value: string, key: string): void {
        localStorage.setItem(key, value);
    }

    remove(key: string): void {
        localStorage.removeItem(key);
    }
}