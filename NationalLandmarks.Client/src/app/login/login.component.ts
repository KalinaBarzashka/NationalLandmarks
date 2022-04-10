import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  get username() {
    return this.loginForm.get('username');
  }

  get password() {
    return this.loginForm.get('password');
  }

  constructor(private fb: FormBuilder, private authService: AuthService) { 
    this.loginForm = this.fb.group({
      'username': ['', [Validators.required]], //default value, array of validators, aync validators
      'password': ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
  }

  login() : void {
    this.authService.login(this.loginForm.value).subscribe(data => {
      this.authService.saveToken(data['token']);
    });
  }

}
