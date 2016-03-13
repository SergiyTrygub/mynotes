import {Injectable} from 'angular2/core';
import {Http, Response, Headers, RequestOptions} from 'angular2/http';
import {Observable} from 'rxjs/Observable';
import {WishItem} from './../wishlist/wishitem';
import {WishDay} from './../wishlist/wishday.model';

var WishItems: WishItem[] = [
    { id: 1, position: 1, text: 'test 1' },
    { id: 2, position: 2, text: 'test 2' },
    { id: 3, position: 3, text: 'test 3' },
];

@Injectable()
export class WishItemsService {
    constructor(private http: Http) { }

    private _apiUrl = 'api/notedays';  // URL to web api

    getItems(tenantId: string) {
        var url = this._apiUrl + tenantId;
        console.log(url);
        return this.http.get(url)
            .map(res => <WishItem[]>res.json().data)
            .do(data => console.log(data)) 
            .catch(this.handleError);
    }

    createNewDay(tenantId: string): Observable<WishDay> {
        var url = this._apiUrl + tenantId;
        console.log("createNewDay", url);

        let body = JSON.stringify({ id: 0, date: new Date() });
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(url, body, options)
            .map(res => <WishDay>res.json().item)
            .do(item => console.log(item)) 
            .catch(this.handleError);
    }

    saveWishDay(tenantId: string, wishDay: WishDay) {
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