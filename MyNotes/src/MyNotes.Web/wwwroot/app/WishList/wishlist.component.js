System.register(['angular2/core', './../wishlist/wishitem.component', './../services/wishdays.service', './../services/wishitems.service'], function(exports_1) {
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, wishitem_component_1, wishdays_service_1, wishitems_service_1;
    var WishListComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (wishitem_component_1_1) {
                wishitem_component_1 = wishitem_component_1_1;
            },
            function (wishdays_service_1_1) {
                wishdays_service_1 = wishdays_service_1_1;
            },
            function (wishitems_service_1_1) {
                wishitems_service_1 = wishitems_service_1_1;
            }],
        execute: function() {
            WishListComponent = (function () {
                function WishListComponent(_wishItemsService, _wishDaysService) {
                    this._wishItemsService = _wishItemsService;
                    this._wishDaysService = _wishDaysService;
                    //public wishItems: WishItem[];
                    this.newItem = {
                        id: 0,
                        position: 0,
                        text: '',
                        wishDayId: 0
                    };
                }
                //getwishItems() {
                //    var tenantId = window.location.pathname;
                //    this._wishItemsService.getItems(tenantId)
                //        .subscribe(items => this.currentWishDay.wishList = items);
                //}
                WishListComponent.prototype.createNewDay = function () {
                    var _this = this;
                    var tenantId = window.location.pathname;
                    this._wishDaysService.createNewDay(tenantId)
                        .subscribe(function (day) {
                        _this.currentWishDay = day;
                        _this.currentWishDay.wishList = [];
                    }, function (error) { return _this.errorMessage = error; });
                };
                WishListComponent.prototype.ngOnInit = function () {
                    //this.getwishItems();
                };
                WishListComponent.prototype.addItem = function () {
                    var _this = this;
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
                            .subscribe(function (item) {
                            _this.currentWishDay.wishList.push(item);
                        }, function (error) { return _this.errorMessage = error; });
                        this.newItem.text = "";
                    }
                };
                WishListComponent.prototype.removeItem = function (id) {
                    var _this = this;
                    console.log('removing item', id);
                    this._wishItemsService.removeWishItem(id)
                        .subscribe(function (res) {
                        console.log(res);
                        _this.currentWishDay.wishList = _this.currentWishDay.wishList.filter(function (current, idx, array) {
                            return current.id != id;
                        });
                    }, function (error) { return _this.errorMessage = error; });
                };
                WishListComponent = __decorate([
                    core_1.Component({
                        selector: 'wish-list',
                        templateUrl: 'app/wishlist/wishlist.component.html',
                        styleUrls: ['app/wishlist/wishlist.component.css'],
                        directives: [wishitem_component_1.WishItemComponent]
                    }), 
                    __metadata('design:paramtypes', [wishitems_service_1.WishItemsService, wishdays_service_1.WishDaysService])
                ], WishListComponent);
                return WishListComponent;
            })();
            exports_1("WishListComponent", WishListComponent);
        }
    }
});
//# sourceMappingURL=wishlist.component.js.map