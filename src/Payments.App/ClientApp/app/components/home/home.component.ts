import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { HubConnection } from '@aspnet/signalr-client';
import { RateFeedsService } from './../../services/ratefeeds.service'
import { RateFeedHub } from './../../hubs/ratefeeds.hub'


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
    public status: boolean;

    constructor(private rateFeedsServce: RateFeedsService,
                private rateFeedHub: RateFeedHub) {     
    }

    ngOnInit() {
        this.getRateFeeds();
        this.initHub();
    }

    initRateFeedHub() {
        this.getRateFeed(this.rateFeedHub.RateFeedData);
        this.status = this.rateFeedHub.Status;
        if (!status) {
            this.initMessage = "Error establishing connection to hub."
        }
    }

    initHub() {
        this.hubConnection = new HubConnection('http://localhost:56486/rate-feed-hub');

        this.hubConnection.on('Send', (data: any) => this.getRateFeed(data));

        this.hubConnection.start()
            .then(() => {
                this.status  = true;
            })
            .catch(err => {
                this.status = false;
                this.initMessage = "Error establishing connection to hub."
            });              
    }

    getRateFeed(rateFeed: any) {
        if (rateFeed) {
            this.currencyPair = rateFeed.baseCurrency + '/' + rateFeed.tradeCurrency;
            this.rate = rateFeed.rate;
            if (this.rateFeeds.length > 4)
                this.rateFeeds.shift();

            this.rateFeeds.push(rateFeed);
        }    
    }
    
    getRateFeeds() {
        this.rateFeedsServce.getRateFeeds()
            .subscribe(response => {
                this.assembleRateFeeds(response.json());
                if (this.rateFeeds.length > 0) {
                    this.currencyPair = this.rateFeeds[0].baseCurrency + '/' + this.rateFeeds[0].targetCurrency;
                    this.rate = this.rateFeeds[0].rateValue;
                } else {
                    this.currencyPair = "Connected. No rate feed yet."
                }
            });
    }
    
    assembleRateFeeds(rateFeeds: any) {
        for (let rateFeed of rateFeeds)
        {
            this.rateFeeds.push(
                {
                    baseCurrency: rateFeed.baseCurrency,
                    targetCurrency: rateFeed.tradeCurrency,
                    rateValue: rateFeed.rate,
                    reference: rateFeed.id
                });
        }
    }
}

interface RateFeedData {
    baseCurrency: string;
    targetCurrency: string;
    rateValue: number;
    reference: string;
}

