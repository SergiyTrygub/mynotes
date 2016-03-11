import {Component, Input} from 'angular2/core';
import {WishItem} from './../wishlist/wishitem';
import {WishItemComponent} from './../wishlist/wishitem.component'

var WishItems: WishItem[] = [
    { id: 1, position: 1, text: 'test 1' },
    { id: 2, position: 2, text: 'test 2' },
    { id: 3, position: 3, text: 'test 3' },
];

@Component({
    selector: 'wish-list',
    templateUrl: 'app/wishlist/wishlist.component.html',
    styleUrls: ['app/wishlist/wishlist.component.css'],
    directives: [WishItemComponent]
})
export class WishListComponent {
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