import {Component, Input} from 'angular2/core';
import {OnInit} from 'angular2/core';
import {WishItem} from './../wishlist/wishitem';
import {WishItemComponent} from './../wishlist/wishitem.component'
import {WishItemsService} from './../services/wishitems.service';

@Component({
    selector: 'wish-list',
    templateUrl: 'app/wishlist/wishlist.component.html',
    styleUrls: ['app/wishlist/wishlist.component.css'],
    directives: [WishItemComponent]
})
export class WishListComponent implements OnInit {
    constructor(private _wishItemsService: WishItemsService) { }

    public currentDate = new Date();

    public wishItems: WishItem[];
    public newItem: WishItem = {
        id: 0,
        position: 0,
        text: 'test'
    };

    getwishItems() {
        this._wishItemsService.getItems().then(items => this.wishItems = items);
    }
    
    ngOnInit() {
        this.getwishItems();
    }

    addItem() {
        console.log('Add clicked', this.wishItems);
        //this.store.dispatch(addItem(this.newItem));
        //this.newItem = '';
    }
}