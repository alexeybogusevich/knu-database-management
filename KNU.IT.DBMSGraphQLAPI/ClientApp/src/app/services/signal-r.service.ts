import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  public data: DatabaseMessage[];
  private hubConnection: signalR.HubConnection
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:44311/databasehub')
      .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }
  public addTransferChartDataListener = () => {
    this.hubConnection.on('Database', (data) => {
      this.data = data;
      console.log(data);
    });
  }
}

export interface DatabaseMessage {
  message: string
}
