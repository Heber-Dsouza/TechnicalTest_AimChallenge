import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlayerCardComponent } from './player-card/player-card.component';
import { GameTargetComponent } from './game-target/game-target.component'; 

class CountdownTimer {
  private intervalId: any = null;

  constructor(
    public remainingTime: number,
    private onTick: (time: number) => void,
    private onFinish?: () => void
  ) {}

  start() {
    this.stop(); 
    this.onTick(this.remainingTime); 
    this.intervalId = setInterval(() => {
      this.remainingTime -= 100;

      if (this.remainingTime <= 0) {
        this.stop();
        this.onTick(0);
        this.onFinish?.();
      } else {
        this.onTick(this.remainingTime);
      }
    }, 100);
  }

  stop() {
    if (this.intervalId) {
      clearInterval(this.intervalId);
      this.intervalId = null;
    }
  }
}

@Component({
  standalone: true,
  selector: 'game-page',
  imports: [CommonModule, PlayerCardComponent, GameTargetComponent],
  templateUrl: './game-page.component.html',
  styleUrls: ['./game-page.component.scss'],
})
export class GamePageComponent implements OnChanges  {
  @Input() players: any;
  @Input() myId: string | null = '';
  @Output() onButtonClickGetReadyForPlay: EventEmitter<void> = new EventEmitter<void>();
  @Output() onButtonClickListener: EventEmitter<void> = new EventEmitter<void>();
  @Output() handleCountDownTimer: EventEmitter<void> = new EventEmitter<void>();
  @Input() targetStyleString: object | null = null;
  @Input() targetSize: number | null = null;

  private activeTimer: CountdownTimer | null = null;


  handleButtonClickGetReadyForPlay() {
    this.onButtonClickGetReadyForPlay.emit();
  }
  handleButtonClickListener() {
    this.onButtonClickListener.emit();
  }

  handleCountDownTimerTrigger() {
    this.handleCountDownTimer.emit();
  }

  currentPlayerConnectionId: string | null = this.getPlayerTurnConnectionId();

  getPlayerTurnConnectionId(): string | null {
    const player = this.players?.find((x: any) => x.playerStats?.isPlayerTurn);
    return player ? player.connectionId : null;
  }


  constructor() {
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['players'] && this.players) {
      this.handleTimerLogic();
    }
  }

  private handleTimerLogic() {
    const activePlayer = this.players.find((player: any) => player.playerStats.isPlayerTurn);

    if (activePlayer?.connectionId === this.myId) {
      if (!this.activeTimer) {
        this.activeTimer = new CountdownTimer(
          activePlayer.playerStats.secondsMs,
          (remainingTime) => {
            this.handleCountDownTimerTrigger();
            activePlayer.playerStats.secondsMs = remainingTime;
            console.log(`Tempo restante: ${remainingTime}ms`);
          },
          () => {
            console.log(`O timer do jogador ${activePlayer.playerName} terminou.`);
            this.activeTimer = null; 
          }
        );
      }

      this.activeTimer.start(); 
    } else {
      if (this.activeTimer) {
        this.activeTimer.stop(); 
        this.activeTimer = null; 
      }
    }
  }
}
