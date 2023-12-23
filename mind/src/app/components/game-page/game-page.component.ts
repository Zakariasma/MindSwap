import { Component } from '@angular/core';

@Component({
  selector: 'app-game-page',
  templateUrl: './game-page.component.html',
  styleUrls: ['./game-page.component.css']
})
export class GamePageComponent {
  flipped = false;

  flipCard() {
    this.flipped = !this.flipped;
  }
}
