import { Component, OnInit, OnChanges, SimpleChanges  } from '@angular/core';
import { Subscription } from 'rxjs';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { GamePageComponent } from '../components/game-page/game-page.component'
import { HomePageComponent } from '../components/home-page/home-page.component'
import { ApiService } from '../service/api.service'
import { GameHubService } from '../service/signalr.service'


@Component({
  selector: 'app-root',
  imports: [RouterModule, CommonModule, HomePageComponent, GamePageComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit, OnChanges {
  randomName: string = 'PlaceHolder100';
  totalUsersOnline: number | null;
  currentPage: number = 0;
  private subscription: Subscription = new Subscription();

  constructor(private apiService: ApiService, private gameHubService: GameHubService) {
    this.totalUsersOnline = null;
  }

  ngOnInit(): void {
    this.apiService.getRandomName().subscribe({
      next: (response: any) => {
        this.randomName = response.guestName;
      },
      error: (err) => {
        console.error('Erro ao carregar nome:', err);
      }
    });
    

    this.subscription.add(
      this.gameHubService.totalUsersOnline$.subscribe({
        next: (value) => {
          this.totalUsersOnline = value;
        }
      })
    );
  }

  onButtonClick(): void {
    this.apiService.getRandomName().subscribe({
      next: (response: any) => {
        this.randomName = response.guestName;
      },
      error: (err) => {
        console.error('Erro ao chamar o endpoint:', err);
      }
    });
  }

  onButtonClickJoinGameHub(): void {
    this.gameHubService.joinGameHub(this.randomName);
    this.currentPage = 1;
  }

  ngOnChanges(changes: SimpleChanges): void {
    //console.log(gameHubService)
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

}
