import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Landmark } from '../models/Landmark';
import { AuthService } from './auth.service';

@Injectable()
export class LandmarkService {
  private landmarkPath = environment.apiUrl + 'landmark';

  constructor(private http: HttpClient, private authService: AuthService) { }

  public create(data: Landmark): Observable<number> {
    //let headers = new HttpHeaders();
    //headers = headers.set('Authorization', `Bearer ${this.authService.getToken()}`);
    //headers = headers.set('Content-Type', 'application/json; charset=utf-8');
    return this.http.post<number>(this.landmarkPath, data);
  }

  public getLandmarks(): Observable<Array<Landmark>> {
    return this.http.get<Array<Landmark>>(this.landmarkPath);
  }

  public getLandmark(id: number): Observable<Landmark> {
    return this.http.get<Landmark>(this.landmarkPath + '/' + id);
  }

  public editLandmark(data: any): Observable<any> {
    return this.http.put(this.landmarkPath, data);
  }

  public deleteLandmark(id: number) {
    return this.http.delete(this.landmarkPath + '/' + id)
  }
}
