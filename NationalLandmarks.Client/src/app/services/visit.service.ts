import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Visit } from '../models/Visit';

@Injectable({
  providedIn: 'root'
})
export class VisitService {
  private visitPath = environment.apiUrl + 'visit';

  constructor(private http: HttpClient) { }

  public visitLandmark(data: Visit): Observable<any> {
    return this.http.post<any>(this.visitPath, data);
  }

  public getAllVisitedLandmarksForUser(): Observable<Array<Visit>>{
    return this.http.get<Array<Visit>>(this.visitPath);
  }
}
