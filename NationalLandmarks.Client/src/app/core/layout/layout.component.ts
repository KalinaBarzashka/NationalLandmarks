import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit, OnChanges {
  isLogged: boolean = false;
  constructor(private authService: AuthService, private router: Router) { }
  
  ngOnInit(): void {
    this.isLogged = this.authService.isAuthenticated();
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.isLogged = this.authService.isAuthenticated();
  }

  logoutHandler(): void{
    this.authService.deleteToken();
    this.router.navigate(['landmarks']);
  }
}
