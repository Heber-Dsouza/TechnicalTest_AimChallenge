import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

// type TargetCoordinate = {
//   name: string;
//   value: number;
// }

// type TargetPosition = {
//   horizontal: TargetCoordinate;
//   vertical: TargetCoordinate;
// }

@Component({
  selector: 'game-target',
  templateUrl: './game-target.component.html',
  styleUrls: ['./game-target.component.scss'],
  imports: [CommonModule]
})

export class GameTargetComponent {
  // @Input() targetPosition?: TargetPosition;
  @Input() targetSize: number | null = null;
  @Input() targetStyleString: object | null = null;
  @Output() handleButtonClickListener: EventEmitter<void> = new EventEmitter<void>();
  @Input() players: any;
  @Input() myId: string | null = '';
  
  handleButtonClickListenerTrigger() {
    this.handleButtonClickListener.emit();
  }
  

  constructor() {
  }

  isMyTurn(): boolean {
    const player = this.players?.find((x: any ) => x.playerStats?.isPlayerTurn);
    return player ? player.connectionId === this.myId : false;
  }
}