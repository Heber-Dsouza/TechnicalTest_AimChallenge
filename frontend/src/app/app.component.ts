import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlayerCardComponent } from '../components/game-page/player-card/player-card.component';
import { GameTargetComponent } from '../components/game-page/game-target/game-target.component'; 

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
  imports: [PlayerCardComponent, CommonModule, GameTargetComponent],
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
  target = {
    showTarget: true,
    targetSize: 30,
    targetPosition: {
      horizontal: {
        name: "left",
        value: 30
      },
      vertical: {
        name: "top",
        value: 30
      },
    }
  };
  getStyles(): { [key: string]: string } {
    return {
      width: (this.target.targetSize || 0) + 'px',
      height: (this.target.targetSize || 0) + 'px',
      [this.target.targetPosition.horizontal.name]: this.target.targetPosition.horizontal.value + '%',
      [this.target.targetPosition.vertical.name]: this.target.targetPosition.vertical.value + '%'
    };
  }
  targetStyleString = this.getStyles();


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
