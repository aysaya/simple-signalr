import { Http } from '@angular/http';
import { Injectable } from '@angular/core';

@Injectable()
export class RateFeedsService {
    private url = 'http://localhost:56486/api/RateFeeds';

    constructor(private http: Http) {

    }

    getRateFeeds() {
        return this.http.get(this.url);
    }
}