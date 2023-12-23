import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Card } from '../models/card';

@Injectable({
  providedIn: 'root'
})
export class DeckListCardsService {

  private baseUrl = 'https://localhost:7150/api/Cards';

  constructor(private http: HttpClient) { }

  getCards(deckID: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/Deck/${deckID}`);
  }

  getCard(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/${id}`);
  }

  createCard(card: Card): Observable<Card> {
    return this.http.post<Card>(`${this.baseUrl}`, card);
  }

  updateCard(id: number, value: any): Observable<Card> {
    return this.http.put<Card>(`${this.baseUrl}/${id}`, value);
  }

  deleteCard(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`, { responseType: 'text' });
  }
}
