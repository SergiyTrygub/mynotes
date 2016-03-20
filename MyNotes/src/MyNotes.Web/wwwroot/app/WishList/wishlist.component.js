System.register(['angular2/core', 'angular2/router', './../wishlist/wishitem.component', './../services/wishdays.service', './../services/wishitems.service'], function(exports_1) {
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, router_1, wishitem_component_1, wishdays_service_1, wishitems_service_1;
    var WishListComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
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
                function WishListComponent(_router, _routeParams, _wishItemsService, _wishDaysService) {
                    this._router = _router;
                    this._routeParams = _routeParams;
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
                WishListComponent.prototype.createNewDay = function () {
                    var _this = this;
                    var tenantRouteParam = this._routeParams.get('tenant');
                    this._wishDaysService.createNewDay(tenantRouteParam)
                        .subscribe(function (day) {
                        _this.currentWishDay = {
                            id: day.id,
                            date: new Date(day.date.toString()),
                            wishList: []
                        };
                        _this.gotoDay(tenantRouteParam, new Date(day.date.toString()));
                    }, function (error) { return _this.errorMessage = error; });
                };
                WishListComponent.prototype.ngOnInit = function () {
                    var _this = this;
                    var tenantRouteParam = this._routeParams.get('tenant');
                    var dateParameter = this._routeParams.get('date');
                    if (dateParameter && dateParameter != null) {
                        this._wishDaysService.getWishDay(tenantRouteParam, new Date(dateParameter))
                            .subscribe(function (day) {
                            console.log(day);
                            _this.currentWishDay = day;
                        });
                    }
                    else {
                        this._wishDaysService.getWishDay(tenantRouteParam, new Date())
                            .subscribe(function (day) {
                            console.log(day);
                            _this.currentWishDay = day;
                            if (!_this.currentWishDay) {
                                _this.createNewDay();
                            }
                            else {
                                _this.gotoDay(tenantRouteParam, new Date(day.date.toString()));
                            }
                        });
                    }
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
                WishListComponent.prototype.gotoDay = function (tenant, date) {
                    var dateString = (date.getFullYear() + '-' + ('0' + (date.getMonth() + 1)).slice(-2) + '-' + ('0' + date.getDate()).slice(-2));
                    var link = ['WishDay', { tenant: tenant, date: dateString }];
                    this._router.navigate(link);
                };
                WishListComponent = __decorate([
                    core_1.Component({
                        selector: 'wish-list',
                        templateUrl: 'app/wishlist/wishlist.component.html',
                        styleUrls: ['app/wishlist/wishlist.component.css'],
                        directives: [wishitem_component_1.WishItemComponent]
                    }), 
                    __metadata('design:paramtypes', [router_1.Router, router_1.RouteParams, wishitems_service_1.WishItemsService, wishdays_service_1.WishDaysService])
                ], WishListComponent);
                return WishListComponent;
            })();
            exports_1("WishListComponent", WishListComponent);
        }
    }
});
//# sourceMappingURL=wishlist.component.js.map