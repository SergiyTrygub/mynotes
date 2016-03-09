import {Component} from 'angular2/core';
import {WishItem} from './wishlist/wishitem';

var WishItems: WishItem[] = [
    { id: 1, position: 1, text: 'test 1' },
    { id: 2, position: 2, text: 'test 2' }
];

@Component({
    selector: 'wish-list',
    templateUrl: 'views/mywishday.html'
})
export class AppComponent {

    public currentDate = new Date();
    public newItem: WishItem = {
        id: 0,
        position: 0,
        text: 'test'
    };

    public wishItems = WishItems;

    addItem() {
        console.log('Add clicked', this.wishItems);
        //this.store.dispatch(addItem(this.newItem));
        //this.newItem = '';
    }
}