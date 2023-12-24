import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {Result} from "../models/result";

@Injectable({
  providedIn: 'root'
})
export class ResultService {
  private apiUrl = 'https://localhost:7150/api/Results';

  constructor(private http: HttpClient) { }

  insertResult(result: Result): Observable<any> {
    return this.http.post<Result>(`${this.apiUrl}`, result);
  }

  getResults(userId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/byUserId/${userId}`);
  }
}
