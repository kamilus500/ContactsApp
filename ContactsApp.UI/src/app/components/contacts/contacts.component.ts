import { Component, OnInit } from '@angular/core';
import { ContactService } from '../../services/contactService';
import { ContactDto } from '../../models/contactDto';
import { ConfirmationService, MessageService } from 'primeng/api';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { AddContactComponent } from '../add-contact/add-contact.component';
import { filter, map, Observable } from 'rxjs';
import { EditContactComponent } from '../edit-contact/edit-contact.component';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrl: './contacts.component.scss'
})
export class ContactsComponent implements OnInit {
  ref: DynamicDialogRef | undefined;
  contacts$: Observable<ContactDto[]>;
  userId: string | null = null;
  searchValue: string | null = null;

  constructor(private contactService: ContactService, 
    private confirmationService: ConfirmationService, 
    public dialogService: DialogService
  ) {
    this.contacts$ = this.contactService.contacts$;
  }

  ngOnInit(): void {
    this.contactService.loadContacts();
  }

  search(): void {
    if (this.searchValue !== null) {
      this.contacts$ = this.contacts$
                            .pipe(
                              map(c => 
                                    c.filter(c => 
                                        c.firstName.toLocaleLowerCase().includes(this.searchValue!.toLocaleLowerCase()) ||
                                        c.lastName.toLocaleLowerCase().includes(this.searchValue!.toLocaleLowerCase()) ||
                                        c.email.toLocaleLowerCase().includes(this.searchValue!.toLocaleLowerCase())
                                      )
                                  ));
    } else {
      this.contactService.loadContacts();
    }
  }

  show(formName: string, contactId: string) {
    if (formName === 'Add') {
      this.ref = this.dialogService.open(AddContactComponent, {
        header: 'Create contact',
        modal: true,
        contentStyle: { overflow: 'auto' },
      });
    } else {
      this.contactService.loadContactById(contactId);

      this.ref = this.dialogService.open(EditContactComponent, {
        header: 'Update contact',
        modal: true,
        contentStyle: { overflow: 'auto' },
      });
    }
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
          this.contactService.deleteContact(contactId);
        }
    });
  }
}
