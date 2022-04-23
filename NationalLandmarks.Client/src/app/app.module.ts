import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from './register/register.component';
import { AuthService } from './services/auth.service';
import { HttpClientModule } from '@angular/common/http';
import { LandmarkService } from './services/landmark.service';
import { CreateComponent } from './landmark/create/create.component';
import { LandmarkModule } from './landmark/landmark.module';
import { AuthGuardService } from './services/auth-guard.service';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule, //includes CommonModule (commons directives - ngIf, ngFor...)
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    LandmarkModule
  ],
  providers: [
    AuthService,
    LandmarkService,
    AuthGuardService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
