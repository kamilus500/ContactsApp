import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ContactService } from '../../services/contactService';
import { ContactDto } from '../../models/contactDto';
import { DialogService } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-add-contact',
  templateUrl: './add-contact.component.html',
  styleUrl: './add-contact.component.scss'
})
export class AddContactComponent {
  createContactForm: FormGroup;
  selectedFile: File | null = null;
  constructor(private fb: FormBuilder, private contactService: ContactService, private dialogService: DialogService) {
    this.createContactForm = this.fb.group({
      id: [''],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.email, Validators.required]],
      numberPhone: ['', [Validators.required, Validators.maxLength(9), Validators.min(9)]],
      userId: ['']
    });
  }

  onFileSelect(event: any) {
    const input = event.target as HTMLInputElement;
    if (input.files) {
      this.selectedFile = input.files[0];
    }
  }

  onSubmit(): void {
    if (this.createContactForm.valid) {
      let image = this.selectedFile!;

      const formData = new FormData();
      formData.append('email', this.createContactForm.get('email')?.value);
      formData.append('firstName', this.createContactForm.get('firstName')?.value);
      formData.append('lastName', this.createContactForm.get('lastName')?.value);
      formData.append('numberPhone', this.createContactForm.get('numberPhone')?.value);
      formData.append('Image', image, 'image');

      this.contactService.createContact(formData);

      this.dialogService.dialogComponentRefMap.forEach(dialog => {
          dialog.destroy();
      });
    }
  }
}
