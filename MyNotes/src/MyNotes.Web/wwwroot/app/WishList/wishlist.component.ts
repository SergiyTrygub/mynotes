import {Component, Input} from 'angular2/core';
import {OnInit} from 'angular2/core';
import {Router, RouteParams} from 'angular2/router';
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
        private _router: Router,
        private _routeParams: RouteParams,
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

    createNewDay() {
        let tenantRouteParam = this._routeParams.get('tenant');
        this._wishDaysService.createNewDay(tenantRouteParam)
            .subscribe(day => {
                this.currentWishDay = {
                    id: day.id,
                    date: new Date(day.date.toString()),
                    wishList: []
                };

                this.gotoDay(tenantRouteParam, new Date(day.date.toString()));
            },
            error => this.errorMessage = <any>error);
    }

    ngOnInit() {
        let tenantRouteParam = this._routeParams.get('tenant');
        let dateParameter = this._routeParams.get('date');
        if (dateParameter && dateParameter != null) {
            this._wishDaysService.getWishDay(tenantRouteParam, new Date(dateParameter))
                .subscribe(day => {
                    console.log(day);
                    this.currentWishDay = day;
                });
        } else {
            this._wishDaysService.getWishDay(tenantRouteParam, new Date())
                .subscribe(day => {
                    console.log(day);
                    this.currentWishDay = day;

                    if (!this.currentWishDay) {
                        this.createNewDay();
                    } else {
                        this.gotoDay(tenantRouteParam, new Date(day.date.toString()));
                    }
                });
        }
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

    updateItem(item: WishItem) {
        this._wishItemsService.saveWishItem(item)
            .subscribe(item => {
                this.ngOnInit();
            }, error => this.errorMessage = <any>error);
    }

    private gotoDay(tenant: string, date: Date) {        
        var dateString = (date.getFullYear() + '-' + ('0' + (date.getMonth() + 1)).slice(-2) + '-' + ('0' + date.getDate()).slice(-2));
        let link = ['WishDay', { tenant: tenant, date: dateString }];
        this._router.navigate(link);
   }
}