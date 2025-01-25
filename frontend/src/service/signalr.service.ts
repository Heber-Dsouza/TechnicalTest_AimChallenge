import { Injectable } from '@angular/core';
import { HubConnectionBuilder, HubConnection } from '@microsoft/signalr'
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class GameHubService {
  private hubConnection: HubConnection;
  private totalUsersOnlineSubject = new BehaviorSubject<number | null>(null);
  public totalUsersOnline$ = this.totalUsersOnlineSubject.asObservable();

  private playersSubject = new BehaviorSubject<number | null>(null);
  public players$ = this.playersSubject.asObservable();

  private myIdSubject = new BehaviorSubject<string | null>(null);
  public myId$ = this.myIdSubject.asObservable();

  private targetSubject = new BehaviorSubject<string | null>(null);
  public targetSubject$ = this.targetSubject.asObservable();

  constructor() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:7124/hubs/UserCount')
      .build();

    this.hubConnection.on('updateTotalViews', (value) => {
      
    })

    this.hubConnection.on('getTotalUsers', (value) => {
      this.totalUsersOnlineSubject.next(value);
    })

    this.hubConnection.on('getMyConnectionId', (value) => {
      this.myIdSubject.next(value);
    })

    this.hubConnection.on('mainGameHandlerUpdate', (value) => {
      this.playersSubject.next(value)
    })

    this.hubConnection.on('targetPositionValues', (value) => {
      this.targetSubject.next(value)
    })

    this.hubConnection
      .start()
      .then(this.fulfilled, this.rejected)
      .catch(err => console.error(err));

  }

  private fulfilled = (): void => {
    console.log('Conexão com o Hub estabelecida com sucesso.');
    this.newWindowLoadedOnClient();
  };

  private rejected = (error: any): void => {
    console.error('Erro ao conectar ao Hub:', error);
  };

  private newWindowLoadedOnClient(): void {
    try {
      this.hubConnection.send('NewWindowLoaded');
    } catch (err) {
      console.error('Erro ao enviar mensagem para o Hub:', err);
    }
  }

  public joinGameHub(playerName: string): void {
    try {
      this.hubConnection.invoke('MainGameHandler', playerName)
    } catch (err) {
      console.error('Erro ao enviar mensagem para o Hub:', err);
    }
  }

  public getReadyForPlay(): void {
    try {
      this.hubConnection.send('PreGameOrganizer')
    } catch (err) {
      console.error('Erro ao enviar mensagem para o Hub:', err);
    }
  }

  public onClickListener(): void {
    try {
      this.hubConnection.send('ClicksHander')
    } catch (err) {
      console.error('Erro ao enviar mensagem para o Hub:', err);
    }
  }
}