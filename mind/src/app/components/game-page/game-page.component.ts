import { Component } from '@angular/core';
import { Router } from '@angular/router';

interface TableauAssociatif {
  [cle: string]: string;
}
@Component({
  selector: 'app-game-page',
  templateUrl: './game-page.component.html',
  styleUrls: ['./game-page.component.css']
})
export class GamePageComponent {
  indexActuel = 0;
  flipped = false;
  inputValue = '';
  reponsesCorrectes = 0;
  showResult = false;

  constructor(private router: Router) { }


  tableauAssociatif: TableauAssociatif = {
    Enzo: 'Ferrari',
    Urus: 'Lamborghini',
    GT3RS: 'Porsche',
  };

  cles = Object.keys(this.tableauAssociatif);

  flipCard() {
    this.flipped = !this.flipped;
  }

  afficherElementSuivant() {

    // Vérifiez si la réponse est correcte avant de passer à l'élément suivant
    if (this.inputValue.toLowerCase() === this.elementActuel.toLowerCase()) {
      this.reponsesCorrectes++;
    }

    if (this.indexActuel < this.cles.length - 1) {
      this.indexActuel++;
    } else {
      // À la fin du tableau, rediriger vers un autre composant avec le score
      this.showResult = true;
      return;
    }

    this.inputValue = '';
    this.flipped = false;
  }


  get cleActuelle() {
    const cles = Object.keys(this.tableauAssociatif);
    return cles[this.indexActuel];
  }

  get elementActuel() {
    const cle = this.cleActuelle;
    return this.tableauAssociatif[cle];
  }

  rejouer() {
    this.indexActuel = 0;
    this.reponsesCorrectes = 0;
    this.showResult = false;
  }
}
