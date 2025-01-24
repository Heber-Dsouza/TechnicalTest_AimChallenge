import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GamePageComponent } from '../components/game-page/game-page.component'


@Component({
  selector: 'app-root',
  imports: [CommonModule, GamePageComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'frontend';


  constructor() {
  }

}
