import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Town } from '../models/Town';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TownService {
  private townPath = environment.apiUrl + 'town';

  constructor(private http: HttpClient) { }

  public getCities(): Observable<Array<Town>>{
    return this.http.get<Array<Town>>(this.townPath);
  }
}
