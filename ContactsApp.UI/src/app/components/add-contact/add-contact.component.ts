import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ContactService } from '../../services/contactService';
import { ContactDto } from '../../models/contactDto';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-contact',
  templateUrl: './add-contact.component.html',
  styleUrl: './add-contact.component.scss'
})
export class AddContactComponent {

  createContactForm: FormGroup;

  constructor(private router: Router, private fb: FormBuilder, private contactService: ContactService) {
    this.createContactForm = this.fb.group({
      id: [''],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.email, Validators.required]],
      numberPhone: ['', [Validators.required, Validators.maxLength(9), Validators.min(9)]],
      userId: ['']
    });
  }

  onSubmit(): void {
    if (this.createContactForm.valid) {
      let newContact = this.createContactForm.value as ContactDto;
      this.contactService.createContact(newContact);
    }
  }
}
