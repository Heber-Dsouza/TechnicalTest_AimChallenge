import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlayerCardComponent } from '../components/game-page/player-card/player-card.component';

class CountdownTimer {
  private intervalId: any = null;

  constructor(public remainingTime: number, private onTick: (time: number) => void, private onFinish?: () => void) {}

  start() {
    this.intervalId = setInterval(() => {
      this.remainingTime -= 1000;

      if (this.remainingTime <= 0) {
        this.stop();
        this.onTick(0);
        this.onFinish?.();
      } else {
        this.onTick(this.remainingTime);
      }
    }, 1000);
  }

  stop() {
    if (this.intervalId) {
      clearInterval(this.intervalId);
      this.intervalId = null;
    }
  }
}

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [PlayerCardComponent, CommonModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'frontend';
  random255() {
    return Math.floor(Math.random() * 256);
  }
  players = [
    {
      playerName: 'Dsouza',
      playerStats: {
        secondsMs: 30000,
        isActive: false,
        randomColors: {
          r: this.random255(),
          g: this.random255(),
          b: this.random255()
        }
      },
    },
  ];

  constructor() {
    this.initializeTimers();
  }

  initializeTimers() {
    const activePlayer = this.players.find(player => player.playerStats.isActive);
  
    if (activePlayer) {
      const timer = new CountdownTimer(
        activePlayer.playerStats.secondsMs,
        (remainingTime) => {
          activePlayer.playerStats.secondsMs = remainingTime;
        },
        () => {
          console.log(`Timer do jogador ${activePlayer.playerName} finalizado.`);
        }
      );
  
      timer.start();
    } else {
      console.log('Nenhum jogador ativo encontrado.');
    }
  }
}
