import { Component, signal, model} from '@angular/core';
import { FormsModule } from '@angular/forms';
import * as signalR from '@microsoft/signalr';
import { getuid } from 'process';

@Component({
  selector: 'app-dummy',
  imports: [FormsModule],
  templateUrl: './dummy.html',
  styleUrl: './dummy.css',
})


export class Dummy {
    private connection!: signalR.HubConnection;
    messageToSend = model('...');
    messageReceived = model('...');


    ngOnInit() {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5242/chathub")
      .build();
    
    this.connection.on('ReceiveMessage', (message: string) => {
      console.log('Received:', message);
      this.messageReceived.update(msg => message);
    });

    this.connection.start()
      .then(() => console.log('Connected!'))
      .catch(err => console.error(err));
  }
  

  async sendMessage() {
    await this.connection.invoke('SendMessage', this.connection.connectionId?.toString(), this.messageToSend());
  }
}
