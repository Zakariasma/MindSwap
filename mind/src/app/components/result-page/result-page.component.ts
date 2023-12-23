import { Component } from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-result-page',
  templateUrl: './result-page.component.html',
  styleUrls: ['./result-page.component.css']
})
export class ResultPageComponent {
  constructor(private router: Router) { }

  navigateTo(route: string) {
    this.router.navigate([route]).then(r => console.log(r));
  }
}
