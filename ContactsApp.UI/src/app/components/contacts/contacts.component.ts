import { Component, OnInit } from '@angular/core';
import { ContactService } from '../../services/contactService';
import { ContactDto } from '../../models/contactDto';
import { ConfirmationService } from 'primeng/api';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { AddContactComponent } from '../add-contact/add-contact.component';
import { map, Observable } from 'rxjs';
import { EditContactComponent } from '../edit-contact/edit-contact.component';
import { TranslateService } from '@ngx-translate/core';

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

  currentPage: number = 0;

  rows: number = 10;

  constructor(private contactService: ContactService, 
    private confirmationService: ConfirmationService, 
    public dialogService: DialogService,
    private translate: TranslateService
  ) {
    this.contacts$ = this.contactService.contacts$;
  }

  ngOnInit(): void {
    this.contactService.loadContacts(this.rows, this.currentPage);
  }

  onFlagClick(country: string): void {
    this.translate.use(country);
  }

  onPageChange(event: any) {
    this.currentPage = event.first;
    this.rows = event.rows;
    this.contactService.loadContacts(this.rows, this.currentPage);
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
      this.contactService.loadContacts(10, 0);
    }
  }

  show(formName: string, contactId: string) {
    const header = formName === 'Add' ? this.translate.instant('create_contact_header'): this.translate.instant('update_contact_header');
    if (formName === 'Add') {
      this.ref = this.dialogService.open(AddContactComponent, {
        header: header,
        modal: true,
        contentStyle: { overflow: 'auto' },
      });
    } else {
      this.contactService.loadContactById(contactId);
      this.ref = this.dialogService.open(EditContactComponent, {
        header: header,
        modal: true,
        contentStyle: { overflow: 'auto' },
      });
    }
  }

  delete(event: Event, contactId: string) {
    const message = this.translate.instant('delete_contact_message');
    const header = this.translate.instant('confirmation');
    const acceptLabel = this.translate.instant('accept');
    const rejectLabel = this.translate.instant('reject');
    this.confirmationService.confirm({
        target: event.target as EventTarget,
        message: message,
        header: header,
        icon: 'pi pi-exclamation-triangle',
        acceptIcon:"none",
        rejectIcon:"none",
        acceptLabel: acceptLabel,
        rejectLabel: rejectLabel,
        rejectButtonStyleClass:"p-button-text",
        accept: () => {
          this.contactService.deleteContact(contactId);
        }
    });
  }

  getImage(image: any): string {
    return 'data:image/png;base64,' + image;
  }
}
