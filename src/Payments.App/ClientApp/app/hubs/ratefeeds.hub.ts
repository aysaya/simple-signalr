import { HubConnection } from '@aspnet/signalr-client';
import { Injectable, OnInit } from '@angular/core';

@Injectable()
export class RateFeedHub implements OnInit{
    private status: boolean;
    private rateFeedData: any;
    private hubConnection: HubConnection;
    private url = 'http://localhost:56486/rate-feed-hub';

    constructor() {
        this.hubConnection = new HubConnection(this.url);

        this.hubConnection.on('Send', (data: any) => this.rateFeedData = data);

        this.hubConnection.start()
            .then(() => {
                this.status = true;
                console.log('Connected!');
            })
            .catch(err => {
                this.status = false;
                console.log('Error connecting!');
            });
    }

    ngOnInit() {
        this.hubConnection = new HubConnection(this.url);

        this.hubConnection.on('Send', (data: any) => this.rateFeedData = data);

        this.hubConnection.start()
            .then(() => {
                this.status = true;
                console.log('Connected!!');
            })
            .catch(err => {
                this.status = false;
                console.log('Error connecting!!');
            });
    }

    get Status(): boolean {return this.status;}

    get RateFeedData(): any { return this.rateFeedData; }
}