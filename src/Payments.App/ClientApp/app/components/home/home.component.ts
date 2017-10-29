import { Component } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { RateFeedsService } from './../../services/ratefeeds.service'
import { RateFeedHub } from './../../hubs/ratefeeds.hub'


@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    public rateFeeds: RateFeedData[] = [];
    public currencyPair: string;
    public rate: number;
    public initMessage: string;
    public status: boolean;
    public statusSubscription: Subscription;
    public rateFeedSubscription: Subscription;

    constructor(private rateFeedsServce: RateFeedsService,
                private rateFeedHub: RateFeedHub) {     
        this.getRateFeeds();
        this.initRateFeedHub();
    }

    initRateFeedHub() {
        this.rateFeedSubscription = this.rateFeedHub.getRateFeed()
            .subscribe(data => this.getRateFeed(data));
        this.statusSubscription = this.rateFeedHub.getStatus()
            .subscribe(s => this.status = s);

    }

    getRateFeed(rateFeed: any) {
        if (rateFeed) {
            this.currencyPair = rateFeed.baseCurrency + '/' + rateFeed.tradeCurrency;
            this.rate = rateFeed.rate;
            if (this.rateFeeds.length > 4)
                this.rateFeeds.shift();

            this.rateFeeds.push(this.assembleRateFeed(rateFeed));
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

    assembleRateFeed(rateFeed: any) {
        return ({
            baseCurrency: rateFeed.baseCurrency,
            targetCurrency: rateFeed.tradeCurrency,
            rateValue: rateFeed.rate,
            reference: rateFeed.id
        });
    }

    assembleRateFeeds(rateFeeds: any) {
        for (let rateFeed of rateFeeds)
        {
            this.rateFeeds.push(this.assembleRateFeed(rateFeed));
        }
    }
}

interface RateFeedData {
    baseCurrency: string;
    targetCurrency: string;
    rateValue: number;
    reference: string;
}

