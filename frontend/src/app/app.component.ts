import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GamePageComponent } from '../components/game-page/game-page.component'
import { HomePageConponent } from '../components/home-page/home-page.component'


@Component({
  selector: 'app-root',
  imports: [CommonModule, HomePageConponent, GamePageComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'frontend';


  constructor() {
  }

}
