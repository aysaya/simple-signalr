import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { HubConnection } from '@aspnet/signalr-client';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
    private hubConnection: HubConnection;
    public rateFeeds: RateFeedData[] = [];
    public currencyPair: string;
    public rate: number;
    public initMessage: string;

    ngOnInit() {
        this.hubConnection = new HubConnection('http://localhost:56486/rate-feed-hub');

        this.hubConnection.on('Send', (data: any) => {
            this.currencyPair = data.baseCurrency + '/' + data.targetCurrency;
            this.rate = data.rateValue;
            if (this.rateFeeds.length > 4)
                this.rateFeeds.shift();

            this.rateFeeds.push(data);
        });

        this.hubConnection.start()
            .then(() => {
                this.initMessage = "Connected - no feeds yet.";
            })
            .catch(err => {
                this.initMessage = "Error while establishing connection.";
            });
    }
}

interface RateFeedData {
    baseCurrency: string;
    targetCurrency: string;
    rateValue: number;
    reference: string;
}