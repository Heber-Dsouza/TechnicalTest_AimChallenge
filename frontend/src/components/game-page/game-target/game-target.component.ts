import { Component, Input } from '@angular/core';
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
  

  constructor() {
  }
}