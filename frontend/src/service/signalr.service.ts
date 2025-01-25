import { Injectable } from '@angular/core';
import { HubConnectionBuilder, HubConnection } from '@microsoft/signalr'

@Injectable({
  providedIn: 'root'
})

export class GameHubService {
  private hubConnection: HubConnection;
  public totalUsersOnline: number | null;
  constructor() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:7124/hubs/UserCount')
      .build();
    // this.hubConnection.on('ReceiveMessage', (user, message) => {
    //   console.log(`User: ${user}, Message: ${message}`);
    // });

    this.totalUsersOnline = null;

    this.hubConnection.on('updateTotalViews', (value) => {
      console.log(value)
    })

    this.hubConnection.on('getTotalUsers', (value) => {
      this.totalUsersOnline = value;
      console.log(value)
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
}