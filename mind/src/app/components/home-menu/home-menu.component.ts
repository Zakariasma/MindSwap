import { Component } from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-home-menu',
  templateUrl: './home-menu.component.html',
  styleUrls: ['./home-menu.component.css']
})
export class HomeMenuComponent {
  constructor(private router: Router) { }

  navigateTo(route: string) {
    this.router.navigate([route]).then(r => console.log(r));
  }

}
