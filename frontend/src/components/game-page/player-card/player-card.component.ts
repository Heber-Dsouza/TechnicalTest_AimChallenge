import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

type PlayerStats = {
  secondsMs: number;
  isActive: boolean;
};

@Component({
  selector: 'player-card',
  templateUrl: './player-card.component.html',
  styleUrls: ['./player-card.component.scss'],
  imports: [CommonModule]
})

export class PlayerCardComponent {
  @Input() playerName: string = '';
  @Input() playerStats?: PlayerStats;
  

  constructor() {}
}