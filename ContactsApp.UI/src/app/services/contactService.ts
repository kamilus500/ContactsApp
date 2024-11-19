import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { ContactDto } from "../models/contactDto";
import { LoadingService } from "./loadingService";
import { ContactHttpService } from "./httpServices/contactHttpService";

@Injectable({
    providedIn: 'root'
})
  
export class ContactService extends ContactHttpService
{
    public contacts$: BehaviorSubject<ContactDto[]> = new BehaviorSubject<ContactDto[]>([]);
    public contact$: BehaviorSubject<ContactDto|null> = new BehaviorSubject<ContactDto|null>(null);

    constructor(private loadingService: LoadingService) {
        super()
    }

    loadContacts(take: number, skip: number): void {
        this.getAll(take, skip)
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
                    this.loadContacts(10, 0);
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
                    this.loadContacts(10, 0);
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
                    this.loadContacts(10, 0);
                    this.loadingService.hide();
                },
                error: () => {
                    console.log('Error when updating contact');
                }
            })
    }
}