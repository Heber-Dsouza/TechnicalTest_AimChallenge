import { Component, Input, Output, EventEmitter  } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'home-page',
  standalone: true,
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss'],
  imports: [CommonModule]
})
export class HomePageComponent {
  @Input() totalPlayersOnline: number | null = null;
  @Input() guestNameValue: string = '';
  @Output() guestNameValueChange: EventEmitter<string> = new EventEmitter<string>();
  @Output() getNewRandomName: EventEmitter<void> = new EventEmitter<void>();
  onInputChange(event: any) {
    this.guestNameValueChange.emit(event.target.value);
  }
  onButtonClick() {
    this.getNewRandomName.emit();  // Dispara o evento para o pai
  }
  constructor() {
  }

}
