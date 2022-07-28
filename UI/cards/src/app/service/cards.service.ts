import { Card } from './../Models/card.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CardsService {
  baseUrl = 'https://localhost:7294/api/cards';

  constructor(private http: HttpClient) { }

  // get all cards
  getAllCards(): Observable<Card[]> {
    return this.http.get<Card[]>(this.baseUrl);
  }
  // add a card
  addCard(card: Card): Observable<Card> {
    card.id = '00000000-0000-0000-0000-000000000000';
    return this.http.post<Card>(this.baseUrl, card);
  }
  // delete card
  deleteCard(id: string): Observable<Card>{
    return this.http.delete<Card>(this.baseUrl + "/" + id);
  }
}
