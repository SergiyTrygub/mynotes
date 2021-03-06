﻿import {Injectable} from 'angular2/core';
import {Http, Response, Headers, RequestOptions} from 'angular2/http';
import {Observable} from 'rxjs/Observable';
import {WishDay} from './../wishlist/wishday.model';

@Injectable()
export class WishDaysService {
    constructor(private http: Http) { }

    private _apiUrl = 'api/wishdays';  // URL to web api

    getWishDays(tenantId: string) {
        var url = this._apiUrl + tenantId;
        console.log(url);
        return this.http.get(url)
            .map(res => <WishDay[]>res.json().data)
            .do(data => console.log(data))
            .catch(this.handleError);
    }

    getWishDay(tenantId: string, date: Date): Observable<WishDay> {
        var url = this._apiUrl + '/' + tenantId + '/' + (date.getFullYear() + '-' + ('0' + (date.getMonth() + 1)).slice(-2) + '-' + ('0' + date.getDate()).slice(-2));
        console.log(url);
        return this.http.get(url)
            .map(res =>
                <WishDay>res.json()
                )
            .do(day => {
                if (day) {
                    if (!day.wishList) {
                        day.wishList = [];
                    }
                    day.date = new Date(day.date.toString());
                }
            })
            .catch(this.handleError);
    }

    createNewDay(tenantId: string): Observable<WishDay> {
        var url = this._apiUrl + '/' + tenantId;
        console.log("createNewDay", url);
        var d = new Date();
        d.setDate(d.getDate() + 1);

        let body = JSON.stringify({ id: 0, date: d});
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(url, body, options)
            .map(res => <WishDay>res.json().item)
            .do(item => {
                console.log(item);
            })
            .catch(this.handleError);
    }

    saveWishDay(tenantId: string, wishDay: WishDay): Observable<WishDay> {
        var url = this._apiUrl + tenantId + "/" + wishDay.date;

        let body = JSON.stringify(wishDay);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.put(url, body, options)
            .map(res => <WishDay>res.json().item)
            .do(item => console.log(item))
            .catch(this.handleError);
    }

    private handleError(error: Response) {
        // in a real world app, we may send the error to some remote logging infrastructure
        // instead of just logging it to the console
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }
}