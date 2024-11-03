import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ContactService } from '../../services/contactService';
import { Router } from '@angular/router';
import { ContactDto } from '../../models/contactDto';
import { BehaviorSubject, Observable } from 'rxjs';
import { DialogService } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-edit-contact',
  templateUrl: './edit-contact.component.html',
  styleUrl: './edit-contact.component.scss'
})
export class EditContactComponent implements OnInit {
  updateContactForm: FormGroup;
  selectedFile: File | null = null;
  contact$: Observable<ContactDto>;

  constructor(private router: Router, private fb: FormBuilder, private contactService: ContactService, private dialogService: DialogService) {
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

  onFileSelect(event: any) {
    const input = event.target as HTMLInputElement;
    if (input.files) {
      this.selectedFile = input.files[0];
    }
  }

  onSubmit(): void {
    if (this.updateContactForm.valid) {
      let image = this.selectedFile!;

      const formData = new FormData();
      formData.append('id', this.updateContactForm.get('id')?.value);
      formData.append('email', this.updateContactForm.get('email')?.value);
      formData.append('firstName', this.updateContactForm.get('firstName')?.value);
      formData.append('lastName', this.updateContactForm.get('lastName')?.value);
      formData.append('numberPhone', this.updateContactForm.get('numberPhone')?.value);
      formData.append('Image', image, 'image');
      this.contactService.updateContact(formData);

      this.dialogService.dialogComponentRefMap.forEach(dialog => {
        dialog.destroy();
    });
    }
  }
}
