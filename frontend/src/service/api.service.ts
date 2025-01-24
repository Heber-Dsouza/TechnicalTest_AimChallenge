import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'https://localhost:7124/GuestName/GetRandomName';

  constructor(private http: HttpClient) {}

  getRandomName(): Observable<string> {
    return this.http.get<string>(this.apiUrl);
  }
}