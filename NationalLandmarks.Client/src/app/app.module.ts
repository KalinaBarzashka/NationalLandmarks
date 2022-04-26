import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from './register/register.component';
import { AuthService } from './services/auth.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LandmarkService } from './services/landmark.service';
import { LandmarkModule } from './landmark/landmark.module';
import { AuthGuardService } from './services/auth-guard.service';
import { TokenInterceptorService } from './services/token-interceptor.service';
import { ErrorInterceptorService } from './services/error-interceptor.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { CoreModule } from './core/core.module';
import { TownService } from './services/town.service';
import { VisitService } from './services/visit.service';

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
    LandmarkModule,
    CoreModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [
    AuthService,
    LandmarkService,
    TownService,
    VisitService,
    AuthGuardService,
    
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptorService,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
