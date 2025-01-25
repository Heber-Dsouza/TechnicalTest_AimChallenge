import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

type RandomColors = {
  r: number,
  g: number,
  b: number,
}

type PlayerStats = {
  secondsMs: number;
  isPlayerTurn: boolean;
  randomColors: RandomColors;
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
  @Input() isWatching: boolean = false;
  @Input() isReady: boolean = false;
  @Input() place?: number;
  getFormattedTime(milliseconds: number | undefined): string {
    if(milliseconds === undefined)
      milliseconds = 0
    const seconds = Math.floor(milliseconds / 1000);
    return `0:${seconds.toString().padStart(2, '0')}`;
  };
  getPercentageWidth(secondsRemaining: number | undefined, totalSeconds: number = 30000): number {
    if (secondsRemaining === undefined)
      secondsRemaining = 0
    return (100 *  secondsRemaining) / totalSeconds
  };

  constructor() {}
}