import { Routes } from '@angular/router';
import { HomePageComponent } from '../components/home-page/home-page.component'
import { GamePageComponent } from '../components/game-page/game-page.component'

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' }, 
  { path: 'home', component: HomePageComponent },
  { path: 'game', component: GamePageComponent },
];
