import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { ContactDto } from "../models/contactDto";
import { LoadingService } from "./loadingService";

@Injectable({
    providedIn: 'root'
})
  
export class ContactService {
    private API_URL : string = 'https://localhost:7239';
    private httpClient: HttpClient = inject(HttpClient);

    public contacts$: BehaviorSubject<ContactDto[]> = new BehaviorSubject<ContactDto[]>([]);
    public contact$: BehaviorSubject<ContactDto|null> = new BehaviorSubject<ContactDto|null>(null);

    constructor(private loadingService: LoadingService) {

    }

    loadContacts(): void {
        this.getAll()
            .subscribe({
                next: (contacts) => {
                    this.contacts$.next(contacts)
                },
                error: (error) => {
                    console.log('Error when loading contacts', error)
                }
            })
    }

    loadContactById(contactId: string): void {
        this.getById(contactId)
            .subscribe({
                next: (contact) => {
                    this.contact$.next(contact);
                },
                error: (error) => {
                    console.log('Error when loading contacts', error)
                }
            });
    }

    createContact(newContact: FormData): void{
        this.create(newContact)
            .subscribe({
                next: () => {
                    this.loadingService.show();
                    this.loadContacts();
                    this.loadingService.hide();
                },
                error: (error) => {
                    console.log('Error when adding contacts', error)
                }
            });
    }

    deleteContact(contactId: string): void{
        this.delete(contactId)
            .subscribe({
                next: () => {
                    this.loadingService.show();
                    this.loadContacts();
                    this.loadingService.hide();
                },
                error: (error) => {
                    console.log('Error when deleting contact');
                }
            })
    }

    updateContact(updatedContactDto: FormData): void {
        this.update(updatedContactDto)
            .subscribe({
                next: () => {
                    this.loadingService.show();
                    this.loadContacts();
                    this.loadingService.hide();
                },
                error: () => {
                    console.log('Error when updating contact');
                }
            })
    }

    private getAll() : Observable<ContactDto[]> {
        return this.httpClient.get<ContactDto[]>(`${this.API_URL}/GetContacts`);
    }

    private getById(contactId: string) : Observable<ContactDto> {
        return this.httpClient.get<ContactDto>(`${this.API_URL}/GetContactById/${contactId}`);
    }

    private create(newContact: FormData): Observable<string> {
        return this.httpClient.post<string>(`${this.API_URL}/CreateContact`, newContact);
    }

    private delete(contactId: string): Observable<void> {
        return this.httpClient.delete<void>(`${this.API_URL}/DeleteContact/${contactId}`);
    }

    private update(updatedContactDto: FormData): Observable<ContactDto> {
        return this.httpClient.put<ContactDto>(`${this.API_URL}/UpdateContact`, updatedContactDto);
    }
}