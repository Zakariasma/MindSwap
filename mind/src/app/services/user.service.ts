import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable, Subject } from 'rxjs';
import { User } from 'src/app/models/user';
import { HttpHeaders, HttpParams } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient, private cookieService:CookieService) {}

  createUser(data: User) {
    let params = new HttpParams()
        .set('username', data.username)
        .set('password', data.password)
        .set('email', data.email);

    return this.http.post(`${environment.api}Auth/register`, {}, { params: params });
}

login(data: User) {
  let params = new HttpParams()
      .set('username', data.username)
      .set('password', data.password);

  return this.http.post(`${environment.api}Auth/login`, {}, { params: params, responseType: 'text' });
}

getByName(username: string) {
  let token = this.cookieService.get('token');
  console.log(token);
  let headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
  return this.http.get<User>(`https://localhost:7150/name/${username}`, { headers: headers });
}



}
