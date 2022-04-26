import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { User } from '../models/User';

@Injectable()
export class AuthService {
  private loginPath: string = environment.apiUrl + 'identity/login';
  private registerPath: string = environment.apiUrl + 'identity/register';
  private profilePath: string = environment.apiUrl + 'identity/profile';

  constructor(private http: HttpClient) { }

  login(data: any): Observable<any> {
    return this.http.post(this.loginPath, data);
  }

  register(data: any): Observable<any> {
    return this.http.post(this.registerPath, data);
  }

  saveToken(token: string): void {
    localStorage.setItem('token', token);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isAuthenticated(): boolean {
    return this.getToken() ? true : false;
  }

  deleteToken(): void {
    localStorage.removeItem('token');
  }

  getProfileData(): Observable<User> {
    return this.http.get<User>(this.profilePath);
  }

  updateProfileData(data: User): Observable<any> {
    return this.http.put(this.profilePath, data);
  }
}
