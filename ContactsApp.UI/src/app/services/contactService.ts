import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ContactDto } from "../models/contactDto";

@Injectable({
    providedIn: 'root'
})
  
export class ContactService {
    private API_URL : string = 'https://localhost:7239';
    private httpClient: HttpClient = inject(HttpClient);

    getAll() : Observable<ContactDto[]> {
        return this.httpClient.get<ContactDto[]>(`${this.API_URL}/GetContacts`);
    }

    getById(contactId: string) : Observable<ContactDto> {
        return this.httpClient.get<ContactDto>(`${this.API_URL}/Getcontacts/${contactId}`);
    }

    create(newContact: ContactDto): Observable<string> {
        return this.httpClient.post<string>(`${this.API_URL}/CreateContact`, newContact);
    }

    delete(contactId: string): Observable<void> {
        return this.httpClient.delete<void>(`${this.API_URL}/DeleteContact/${contactId}`);
    }

    update(updatedContactDto: ContactDto): Observable<ContactDto> {
        return this.httpClient.put<ContactDto>(`${this.API_URL}/UpdateContact`, updatedContactDto);
    }
}