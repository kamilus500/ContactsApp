import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ContactService } from '../../services/contactService';
import { Router } from '@angular/router';
import { ContactDto } from '../../models/contactDto';
import { BehaviorSubject, Observable } from 'rxjs';

@Component({
  selector: 'app-edit-contact',
  templateUrl: './edit-contact.component.html',
  styleUrl: './edit-contact.component.scss'
})
export class EditContactComponent implements OnInit {
  updateContactForm: FormGroup;

  contact$: Observable<ContactDto>;

  constructor(private router: Router, private fb: FormBuilder, private contactService: ContactService) {
    this.contact$ = this.contactService.contact$;

    this.updateContactForm = this.fb.group({
      id: [''],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.email, Validators.required]],
      numberPhone: ['', [Validators.required, Validators.maxLength(9), Validators.min(9)]],
      userId: ['']
    });
  }
  
  ngOnInit(): void {
    this.contact$.subscribe(contact => {
      this.updateContactForm.patchValue({
        id: contact.id,
        firstName: contact.firstName,
        lastName: contact.lastName,
        email: contact.email,
        numberPhone: contact.numberPhone,
      })
    })
  }

  onSubmit(): void {
    if (this.updateContactForm.valid) {
      let updatedContact = this.updateContactForm.value as ContactDto;
      this.contactService.updateContact(updatedContact);
    }
  }
}
