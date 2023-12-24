import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DeckListComponent } from './views/deck-list/deck-list.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { DeckListCardsComponent } from './views/deck-list-cards/deck-list-cards.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DeckCardComponent } from './components/deck-card/deck-card.component';
import { UniversalModalComponent } from './components/universal-modal/universal-modal.component';
import { UniversalCardComponent } from './components/universal-card/universal-card.component';
import { GameComponent } from './views/game/game.component';
import { HistoriqueComponent } from './views/historique/historique.component';
import { ResultComponent } from './views/result/result.component';

@NgModule({
  declarations: [
    AppComponent,
    DeckListComponent,
    DeckListCardsComponent,
    DeckCardComponent,
    UniversalModalComponent,
    UniversalCardComponent,
    GameComponent,
    HistoriqueComponent,
    ResultComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
