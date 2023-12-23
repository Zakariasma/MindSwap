import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeMenuComponent } from './components/home-menu/home-menu.component';
import { GamePageComponent } from './components/game-page/game-page.component';
import { ResultPageComponent } from './components/result-page/result-page.component';
import {FormsModule} from "@angular/forms";

@NgModule({
  declarations: [
    AppComponent,
    HomeMenuComponent,
    GamePageComponent,
    ResultPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
