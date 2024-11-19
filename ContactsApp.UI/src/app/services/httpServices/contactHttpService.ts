import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ContactDto } from "../../models/contactDto";

@Injectable({
    providedIn: 'root'
})
  
export class ContactHttpService {
    private API_URL : string = 'https://localhost:7239';
    private httpClient: HttpClient = inject(HttpClient);

    protected getAll(take: number, skip: number) : Observable<ContactDto[]> {
        return this.httpClient.get<ContactDto[]>(`${this.API_URL}/GetContacts/${take}/${skip}`);
    }

    protected getById(contactId: string) : Observable<ContactDto> {
        return this.httpClient.get<ContactDto>(`${this.API_URL}/GetContactById/${contactId}`);
    }

    protected create(newContact: FormData): Observable<string> {
        return this.httpClient.post<string>(`${this.API_URL}/CreateContact`, newContact);
    }

    protected delete(contactId: string): Observable<void> {
        return this.httpClient.delete<void>(`${this.API_URL}/DeleteContact/${contactId}`);
    }

    protected update(updatedContactDto: FormData): Observable<ContactDto> {
        return this.httpClient.put<ContactDto>(`${this.API_URL}/UpdateContact`, updatedContactDto);
    }
}