import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
//import { RouterOutlet } from '@angular/router';

import { PlayerCardComponent } from '../components/game-page/player-card/player-card.component'

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [/* RouterOutlet */PlayerCardComponent, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'frontend';
  players = [
    {
      playerName: 'Dsouza',
      playerStats: {
        secondsMs: 3000,
        isActive: true
      }
    },
  ];
}
