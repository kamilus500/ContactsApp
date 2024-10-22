import { Component, OnInit } from '@angular/core';
import { ContactService } from '../../services/contactService';
import { ContactDto } from '../../models/contactDto';
import { ConfirmationService } from 'primeng/api';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrl: './contacts.component.scss'
})
export class ContactsComponent implements OnInit{

  contacts: ContactDto[] = [];
  userId: string | null = null;
  searchValue: string | null = null;

  constructor(private contactService: ContactService, private confirmationService: ConfirmationService) {

  }

  ngOnInit(): void {
    this.contactService.getAll()
      .subscribe(contacts => {
        this.contacts = contacts;
      });
  }

  delete(event: Event, contactId: string) {
    this.confirmationService.confirm({
        target: event.target as EventTarget,
        message: 'Are you sure to remove this contact',
        header: 'Confirmation',
        icon: 'pi pi-exclamation-triangle',
        acceptIcon:"none",
        rejectIcon:"none",
        rejectButtonStyleClass:"p-button-text",
        accept: () => {
          this.contactService.delete(contactId)
            .subscribe(x => {      
              this.contactService.getAll()
                .subscribe(contacts => this.contacts = contacts);
            });
        }
    });
  }

}
