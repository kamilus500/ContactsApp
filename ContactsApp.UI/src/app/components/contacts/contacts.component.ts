import { Component, OnInit } from '@angular/core';
import { ContactService } from '../../services/contactService';
import { ContactDto } from '../../models/contactDto';
import { ConfirmationService, MessageService } from 'primeng/api';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { AddContactComponent } from '../add-contact/add-contact.component';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrl: './contacts.component.scss'
})
export class ContactsComponent implements OnInit {
  ref: DynamicDialogRef | undefined;
  contacts: ContactDto[] = [];
  userId: string | null = null;
  searchValue: string | null = null;


  constructor(private contactService: ContactService, 
    private confirmationService: ConfirmationService, 
    public dialogService: DialogService,
    private messageService: MessageService
  ) {

  }

  ngOnInit(): void {
    this.contactService.getAll()
      .subscribe({
        next: (response: ContactDto[]) => {
          this.contacts = response;
        },
        error: (error) => {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Something went wrong' });
        }
      });
  }

  search(): void {
    if (this.searchValue !== '') {
      this.contacts = this.contacts
                        .filter(c => c.firstName.toLocaleLowerCase().includes(this.searchValue!.toLocaleLowerCase()) ||
                                     c.lastName.toLocaleLowerCase().includes(this.searchValue!.toLocaleLowerCase()) ||
                                     c.email.toLocaleLowerCase().includes(this.searchValue!.toLocaleLowerCase()))
    } else {
      this.contactService.getAll()
        .subscribe({
          next: (response: ContactDto[]) => {
            this.contacts = response;
          },
          error: (error) => {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Something went wrong' });
          }
        })
    }
  }

  show() {
    this.ref = this.dialogService.open(AddContactComponent, {
        header: 'Create contact',
        modal: true,
        contentStyle: { overflow: 'auto' },
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

          this.contactService.delete(contactId)
           .subscribe({
               next: () => {
                this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Deleted successfully' });
                this.contactService.getAll()
                  .subscribe({
                    next: (response: ContactDto[]) => {
                      this.contacts = response;
                    },
                    error: (error) => {
                      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Something went wrong' });
                    }
                  });
              },
              error: (error) => {
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Something went wrong' });
              }
            })
        }
    });
  }
}
