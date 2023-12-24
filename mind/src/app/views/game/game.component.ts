import {Component, OnInit} from '@angular/core';
import { Router } from '@angular/router';
import {Card} from "../../models/card";
import {DeckListCardsService} from "../../_services/deck-list-cards.service";

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit{
  indexActuel = 0;
  flipped = false;
  inputValue = '';
  reponsesCorrectes = 0;
  showResult = false;
  cards!: Card[];

  constructor(private deckListCardsService: DeckListCardsService, private router: Router) { }

  ngOnInit(): void {
    this.deckListCardsService.getCards(1).subscribe(cards => {
      this.cards = cards;
    });
  }


  flipCard() {
    this.flipped = !this.flipped;
  }

  afficherElementSuivant() {

    // Vérifiez si la réponse est correcte avant de passer à l'élément suivant
    if (this.inputValue.toLowerCase() === this.cards[this.indexActuel].backText.toLowerCase()) {
      this.reponsesCorrectes++;
    }

    if (this.indexActuel < this.cards.length - 1) {
      this.indexActuel++;
    } else {
      // À la fin du tableau, rediriger vers un autre composant avec le score
      this.showResult = true;
      return;
    }

    this.inputValue = '';
    this.flipped = false;
  }



  rejouer() {
    this.indexActuel = 0;
    this.reponsesCorrectes = 0;
    this.showResult = false;
  }
}
