import { Routes } from '@angular/router';
import { HomePageComponent } from '../components/home-page/home-page.component'
import { GamePageComponent } from '../components/game-page/game-page.component'

// rotas configuradas, mas como o projeto é algo simples, roteamento será uma melhoria no futuro.
export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' }, 
  { path: 'home', component: HomePageComponent },
  { path: 'game', component: GamePageComponent },
];
