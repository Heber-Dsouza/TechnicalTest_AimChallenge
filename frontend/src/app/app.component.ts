import { Component, OnInit  } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { GamePageComponent } from '../components/game-page/game-page.component'
import { HomePageComponent } from '../components/home-page/home-page.component'
import { ApiService } from '../service/api.service'


@Component({
  selector: 'app-root',
  imports: [RouterModule, CommonModule, HomePageComponent, GamePageComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'frontend';
  randomName: string | null = null;

  constructor(private apiService: ApiService) {
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
  }
}
