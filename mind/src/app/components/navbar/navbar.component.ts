import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {

  constructor(
    private router: Router,
    private cookieService: CookieService
  ) { }

  logout(): void {
    this.cookieService.deleteAll();
    this.router.navigate(['/login']);
  }

  goToHomePage(): void {
    this.router.navigate(['/deck-list']);
  }
}
