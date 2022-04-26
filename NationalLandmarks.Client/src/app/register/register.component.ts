import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, MaxLengthValidator, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;

  get firstName() {
    return this.registerForm.get('firstName');
  }

  get lastName() {
    return this.registerForm.get('lastName');
  }

  get username() {
    return this.registerForm.get('username');
  }

  get email() {
    return this.registerForm.get('email');
  }

  get password() {
    return this.registerForm.get('password');
  }

  constructor(
    private fb: FormBuilder, 
    private authService: AuthService,
    private toastrService: ToastrService,
    private router: Router) { 
    this.registerForm = this.fb.group({
      'firstName': ['', [Validators.required, Validators.maxLength(40)]],
      'lastName': ['', [Validators.required, Validators.maxLength(40)]],
      'username': ['', [Validators.required]],
      'email': ['', [Validators.required, Validators.email]],
      'password': ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
  }

  register() : void {
    this.authService.register(this.registerForm.value).subscribe(data => {
      this.toastrService.success("Successfully created profile!");
      this.router.navigate(['/login']);
    });
  }

}
