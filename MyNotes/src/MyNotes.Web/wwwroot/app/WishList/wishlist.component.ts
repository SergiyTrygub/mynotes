import {Component, Input} from 'angular2/core';
import {OnInit} from 'angular2/core';
import {WishDay} from './../wishlist/wishday.model';
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

    public currentWishDay: WishDay;
    public errorMessage: string;

    //public wishItems: WishItem[];
    public newItem: WishItem = {
        id: 0,
        position: 0,
        text: ''
    };

    getwishItems() {
        var tenantId = window.location.pathname;
        this._wishItemsService.getItems(tenantId)
            .subscribe(items => this.currentWishDay.wishList = items);
    }

    createNewDay() {
        var tenantId = window.location.pathname;
        this._wishItemsService.createNewDay(tenantId)
            .subscribe(day => {
                this.currentWishDay = day;
                this.currentWishDay.wishList = [];
            },
            error => this.errorMessage = <any>error);
    }

    ngOnInit() {
        //this.getwishItems();
    }

    addItem() {
        console.log('Add clicked', this.newItem);
        if (this.currentWishDay) {
            this.currentWishDay.wishList.push({
                text: this.newItem.text,
                position: this.currentWishDay.wishList.length,
                id: 0
            });
            var tenantId = window.location.pathname;
            this._wishItemsService.saveWishDay(tenantId, this.currentWishDay)
                .subscribe(day => {
                    this.currentWishDay = day;
                }, error => this.errorMessage = <any>error);
            this.newItem.text = "";
        }
    }

    removeItem(id: number) {
        console.log('removing item', id);
    }
}