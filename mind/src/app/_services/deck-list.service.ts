import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Deck } from '../models/deck';

@Injectable({
  providedIn: 'root'
})
export class DeckListService {

  private baseUrl = 'https://localhost:7150/api/Decks';

  constructor(private http: HttpClient) { }

  getDecks(userID: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/User/${userID}`);
  }

  getDeck(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/${id}`);
  }

  createDeck(deck: Deck): Observable<Deck> {
    return this.http.post<Deck>(`${this.baseUrl}`, deck);
  }

  updateDeck(id: number, value: Deck): Observable<Object> {
    return this.http.put(`${this.baseUrl}/${id}`, value);
  }

  deleteDeck(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`, { responseType: 'text' });
  }

  deleteAllDecks(): Observable<any> {
    return this.http.delete(`${this.baseUrl}` + `/delete`, { responseType: 'text' });
  }

  getDecksList(): Observable<any> {
    return this.http.get(`${this.baseUrl}`);
  }
}
