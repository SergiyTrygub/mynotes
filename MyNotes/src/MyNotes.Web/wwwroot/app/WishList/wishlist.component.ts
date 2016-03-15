import {Component, Input} from 'angular2/core';
import {OnInit} from 'angular2/core';
import {WishDay} from './../wishlist/wishday.model';
import {WishItem} from './../wishlist/wishitem';
import {WishItemComponent} from './../wishlist/wishitem.component'
import {WishDaysService} from './../services/wishdays.service';
import {WishItemsService} from './../services/wishitems.service';

@Component({
    selector: 'wish-list',
    templateUrl: 'app/wishlist/wishlist.component.html',
    styleUrls: ['app/wishlist/wishlist.component.css'],
    directives: [WishItemComponent]
})
export class WishListComponent implements OnInit {
    constructor(
        private _wishItemsService: WishItemsService,
        private _wishDaysService: WishDaysService) {
    }

    public currentWishDay: WishDay;
    public errorMessage: string;

    //public wishItems: WishItem[];
    public newItem: WishItem = {
        id: 0,
        position: 0,
        text: '',
        wishDayId: 0
    };

    //getwishItems() {
    //    var tenantId = window.location.pathname;
    //    this._wishItemsService.getItems(tenantId)
    //        .subscribe(items => this.currentWishDay.wishList = items);
    //}

    createNewDay() {
        var tenantId = window.location.pathname;
        this._wishDaysService.createNewDay(tenantId)
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
            var newItem = {
                text: this.newItem.text,
                position: this.currentWishDay.wishList.length,
                id: 0,
                wishDayId: this.currentWishDay.id
            };
            var tenantId = window.location.pathname;
            this._wishItemsService.saveWishItem(newItem)
                .subscribe(item => {
                    this.currentWishDay.wishList.push(item);
                }, error => this.errorMessage = <any>error);
            this.newItem.text = "";
        }
    }

    removeItem(id: number) {
        console.log('removing item', id);
        this._wishItemsService.removeWishItem(id)
            .subscribe(res => {
                console.log(res);
                this.currentWishDay.wishList = this.currentWishDay.wishList.filter(function (current, idx, array) {
                    return current.id != id;
                });
            },
            error => this.errorMessage = <any>error);
    }
}