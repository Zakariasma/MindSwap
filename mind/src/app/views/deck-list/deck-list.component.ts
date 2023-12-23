import { Component } from '@angular/core';
import { DeckListService } from '../../_services/deck-list.service';
import { Deck } from 'src/app/models/deck';
import { User } from 'src/app/models/user';
import { Router } from '@angular/router';


@Component({
  selector: 'app-deck-list',
  templateUrl: './deck-list.component.html',
  styleUrls: ['./deck-list.component.css']
})
export class DeckListComponent {
  decks!: Deck[];
  user!: User;
  modalOpen = false;
  modalOpenEdit = false;
  selectedDeck!: Deck;

  constructor(private deckListService: DeckListService, private router: Router) { }

  ngOnInit(): void {
    this.user = JSON.parse(localStorage.getItem('user')!);
    this.deckListService.getDecks(1).subscribe(decks => {
      this.decks = decks;
      console.log(this.decks);
    });
  }

  openModal(): void {
    this.modalOpen = true;
  }

  closeModal(): void {
    this.modalOpen = false;
  }

  openModalEdit(deck: Deck): void {
    this.selectedDeck = deck;
    this.modalOpenEdit = true;
  }

  closeModalEdit(): void {
    this.modalOpenEdit = false;
  }

  createDeck(formValue: any): void {
    if (formValue.title === '') {
      return;
    }
    var deck = new Deck(
      0,
      formValue.title,
      new Date(),
      new Date(),
      this.user,
      1 // Remplacer cette valeur par this.user.id
      );
    this.deckListService.createDeck(deck).subscribe(deck => {
      this.decks.push(deck);
      this.closeModal();
    });
  }

  updateDeck(formValue: any): void {
    if (formValue.title == '') {
      console.log(formValue.title);
      return;
    }
    var deck = new Deck(
      this.selectedDeck.id,
      formValue.title,
      new Date(),
      new Date(),
      this.user,
      1 // Remplacer cette valeur par this.user.id
    );
    console.log(deck.id);
    this.deckListService.updateDeck(deck.id, deck).subscribe(d => {
      const index = this.decks.findIndex(d => d.id === deck.id);
      if (index > -1) {
        this.decks[index] = deck;
      }
      this.closeModalEdit();
    });
  }


  deleteDeck(id: number): void {
    this.deckListService.deleteDeck(id).subscribe(() => {
      this.decks = this.decks.filter(deck => deck.id !== id);
    });
  }

  editDeck(deck: Deck): void {
    localStorage.setItem('deck', JSON.stringify(deck));
    this.router.navigate(['/deck']);
  }

  goToDeckListCards(idDeck: number): void {
    this.router.navigate(['/deck-list-cards', idDeck]);
  }
}

