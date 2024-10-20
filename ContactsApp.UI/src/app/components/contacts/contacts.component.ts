import { Component, OnInit } from '@angular/core';
import { ContactService } from '../../services/contactService';
import { ContactDto } from '../../models/contactDto';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrl: './contacts.component.scss'
})
export class ContactsComponent implements OnInit{

  contacts: ContactDto[] = [];
  userId: string | null = null;

  constructor(private contactService: ContactService) {

  }

  ngOnInit(): void {
    this.contactService.getAll()
      .subscribe(contacts => {
        this.contacts = contacts;
      });
  }

}
