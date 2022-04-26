import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from 'src/app/models/User';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  user!: User;
  inEditMode = false;
  profileForm!: FormGroup;

  get firstName() { return this.profileForm?.get('firstName'); }
  get lastName() { return this.profileForm?.get('lastName'); }
  get userName() { return this.profileForm?.get('userName'); }
  get email() { return this.profileForm?.get('email'); }

  constructor(private authService: AuthService, private fb: FormBuilder) {
    this.authService.getProfileData().subscribe(res => {
      this.user = res
      this.profileForm = this.fb.group({
        'firstName': [this.user.firstName, [Validators.required]],
        'lastName': [this.user.lastName, [Validators.required]],
        'userName': [this.user.userName, [Validators.required]],
        'email': [this.user.email, [Validators.required, Validators.email]]
      });
    });
  }

  ngOnInit(): void {
  }

  toggleEditMode(): void{
    this.inEditMode = !this.inEditMode;
  }

  submitHandler() {
    this.authService.updateProfileData(this.profileForm.value).subscribe({
      next: () => {
        this.inEditMode = false;
        this.user = this.profileForm.value;
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
}
