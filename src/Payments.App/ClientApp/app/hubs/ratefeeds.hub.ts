import { HubConnection } from '@aspnet/signalr-client';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class RateFeedHub {
    private status = new Subject<boolean>();
    private hubConnection: HubConnection;
    private url = 'http://localhost:56486/rate-feed-hub';
    private rateFeedSubject = new Subject<any>();

    constructor() {
        this.hubConnection = new HubConnection(this.url);

        this.hubConnection.on('Send', (data: any) => this.sendRateFeed(data));

        this.hubConnection.start()
            .then(() => {
                this.status.next(true);
                console.log('Connected!');
            })
            .catch(err => {
                this.status.next(false);
                console.log('Error connecting!');
            });
    }

    sendRateFeed(rateFeedData: any) {
        this.rateFeedSubject.next(rateFeedData);
    }

    getRateFeed(): Observable<any> {
        return this.rateFeedSubject.asObservable();
    }

    getStatus(): Observable<boolean> {
        return this.status.asObservable();
    }

}