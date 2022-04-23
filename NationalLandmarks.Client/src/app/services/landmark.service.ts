import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Landmark } from '../models/Landmark';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class LandmarkService {
  private landmarkpath = environment.apiUrl + 'landmark';

  constructor(private http: HttpClient, private authService: AuthService) { }

  create(data: Landmark): Observable<number> {
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${this.authService.getToken()}`);
    //headers = headers.set('Content-Type', 'application/json; charset=utf-8');
    return this.http.post<number>(this.landmarkpath, data, { headers: headers });
  }
}
