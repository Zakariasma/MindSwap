import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeMenuComponent} from "./components/home-menu/home-menu.component";
import {GamePageComponent} from "./components/game-page/game-page.component";
import {ResultPageComponent} from "./components/result-page/result-page.component";

const routes: Routes = [
  {path: '', redirectTo:'homePage', pathMatch: 'full'},
  {path: 'homeMenu', component: HomeMenuComponent},
  {path: 'gamePage', component: GamePageComponent},
  {path: 'resultPage', component: ResultPageComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
