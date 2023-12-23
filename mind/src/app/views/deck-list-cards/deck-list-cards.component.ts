import { Component } from '@angular/core';
import { DeckListCardsService } from '../../_services/deck-list-cards.service';
import { Deck } from 'src/app/models/deck';
import { User } from 'src/app/models/user';
import { Card } from 'src/app/models/card';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-deck-list-cards',
  templateUrl: './deck-list-cards.component.html',
  styleUrls: ['./deck-list-cards.component.css']
})
export class DeckListCardsComponent {
  deck!: Deck;
  cards!: Card[];
  user!: User;
  deckId!: number;
  createCardModalOpen = false;
  addPhotosModalOpen = false;

  constructor(private deckListCardsService: DeckListCardsService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.deckId = Number(params.get('id'))
    });
    this.deckListCardsService.getCards(this.deckId).subscribe(cards => {
      this.cards = cards;
      console.log(this.cards);
    });
  }

  openCreateCardModal(): void {
    this.createCardModalOpen = true;
  }

  closeCreateCardModal(): void {
    this.createCardModalOpen = false;
  }

  openAddPhotosModal(): void {
    this.addPhotosModalOpen = true;
  }

  closeAddPhotosModal(): void {
    this.addPhotosModalOpen = false;
  }

  createCard(formValue: any): void {
    var card = new Card(
      0,
      formValue.frontText,
      formValue.backText,
      "",
      "",
      new Date(),
      new Date(),
      this.deckId,
    );

    console.log(card);
    this.deckListCardsService.createCard(card).subscribe(card => {
      this.cards.push(card);
      this.closeCreateCardModal();
      this.openAddPhotosModal();
    });
  }

  addPhotos(formValue: any): void {
    // Your code to add photos
    console.log(formValue);
    this.closeAddPhotosModal();
  }

  deleteCard(card: Card): void {
    this.deckListCardsService.deleteCard(card.id).subscribe(() => {
      this.cards = this.cards.filter(c => c !== card);
    });
  }

  editCard(card: Card): void {
    localStorage.setItem('card', JSON.stringify(card));
    this.router.navigate(['/card']);
  }

  logout(): void {
    localStorage.removeItem('user');
    this.router.navigate(['/login']);
  }
}

