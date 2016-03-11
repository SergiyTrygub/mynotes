import {Injectable} from 'angular2/core';
import {WishItem} from './../wishlist/wishitem';

var WishItems: WishItem[] = [
    { id: 1, position: 1, text: 'test 1' },
    { id: 2, position: 2, text: 'test 2' },
    { id: 3, position: 3, text: 'test 3' },
];

@Injectable()
export class WishItemsService {
    getItems() {
        return Promise.resolve(WishItems);
    }
}