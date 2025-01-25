import { Component, OnInit, OnChanges, SimpleChanges  } from '@angular/core';
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
  title = 'frontend';
  randomName: string | null = null;
  totalUsersOnline: number | null;

  constructor(private apiService: ApiService, private gameHubService: GameHubService) {
    this.totalUsersOnline = null;
    console.log('usersOnline', gameHubService.totalUsersOnline)
  }

  ngOnInit(): void {
    this.apiService.getRandomName().subscribe({
      next: (response) => {
        // this.randomName = response;
        console.log('Nome recebido:', response);
      },
      error: (err) => {
        console.error('Erro ao carregar nome:', err);
      }
    });
    
    // this.gameHubService.newWindowLoadedOnClient()
  }

  ngOnChanges(changes: SimpleChanges): void {
    //console.log(gameHubService)
  }

}
