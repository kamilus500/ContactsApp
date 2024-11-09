import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/userService';
import { Observable } from 'rxjs';
import { UserDto } from '../../models/userDto';
import { AuthService } from '../../services/authService';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoadingService } from '../../services/loadingService';
import { MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrl: './user-details.component.scss'
})
export class UserDetailsComponent implements OnInit {
  userForm: FormGroup;
  user$: Observable<UserDto|null>;
  image$: Observable<string>;
  selectedFile: File | null = null;

  constructor(private fb: FormBuilder, 
    private userService: UserService, 
    private authService: AuthService, 
    private loadingService: LoadingService,
    private messageService: MessageService,
    private dialogService: DialogService
  ) {
    this.userService.getUser();
    this.user$ = this.userService.user$;
    this.image$ = this.authService.imageUrl$;

    this.userForm = this.fb.group({
      id: [''],
      firstName: ['', [Validators.required]],
      lastName: ['', Validators.required],
      email: ['', Validators.email]
    });
  }

  ngOnInit(): void {
    this.user$.subscribe(user => {
      this.userForm.patchValue({
        id: '',
        firstName: user?.firstName,
        lastName: user?.lastName,
        email: user?.email
      });
    })

  }

  onFileSelect(event: any) {
    const input = event.target as HTMLInputElement;
    if (input.files) {
      this.selectedFile = input.files[0];
    }
  }

  onSubmit(): void {
    if (this.userForm.valid) {
      let image = this.selectedFile!;

      const formData = new FormData();
      formData.append('id', this.userForm.get('id')?.value);
      formData.append('email', this.userForm.get('email')?.value);
      formData.append('firstName', this.userForm.get('firstName')?.value);
      formData.append('lastName', this.userForm.get('lastName')?.value);

      if (image) {  
        formData.append('Image', image, 'image');
      } else {
        formData.append('Image', new Blob());
      }

      this.loadingService.show();

      this.userService.updateUser(formData);

      this.loadingService.hide();
      this.dialogService.dialogComponentRefMap.forEach(dialog => {
        dialog.destroy();
      });
    }
  }
}
