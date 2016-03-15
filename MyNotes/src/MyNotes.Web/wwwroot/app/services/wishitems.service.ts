import {Injectable} from 'angular2/core';
import {Http, Response, Headers, RequestOptions} from 'angular2/http';
import {Observable} from 'rxjs/Observable';
import {WishItem} from './../wishlist/wishitem';
import {WishDay} from './../wishlist/wishday.model';

@Injectable()
export class WishItemsService {
    constructor(private http: Http) { }

    private _apiUrl = 'api/wishitems';  // URL to web api

    saveWishItem(item: WishItem): Observable<WishItem> {
        var url = this._apiUrl;

        let body = JSON.stringify(item);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(url, body, options)
            .map(res => <WishItem>res.json().item)
            .do(item => console.log(item))
            .catch(this.handleError);
    }

    removeWishItem(itemId: number): Observable<Response> {
        var url = this._apiUrl + "/" + itemId;
        return this.http.delete(url);
    }

    private handleError(error: Response) {
        // in a real world app, we may send the error to some remote logging infrastructure
        // instead of just logging it to the console
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }
}