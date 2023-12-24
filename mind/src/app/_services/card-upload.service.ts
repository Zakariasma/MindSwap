import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Card } from '../models/card';


@Injectable({
  providedIn: 'root'
})
export class CardUploadService {
  private apiUrl = 'https://localhost:7150/api/CardUploadModels';

  constructor(private http: HttpClient) { }

  uploadCard(formData: FormData): Observable<any> {
    return this.http.post(this.apiUrl, formData, {
      headers: {
        'accept': 'text/plain'
      }
    });
  }

  deleteCard(cardId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${cardId}`);
  }
}
