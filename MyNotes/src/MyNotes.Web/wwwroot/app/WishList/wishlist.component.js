System.register(['angular2/core', './../wishlist/wishitem.component', './../services/wishitems.service'], function(exports_1) {
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, wishitem_component_1, wishitems_service_1;
    var WishListComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (wishitem_component_1_1) {
                wishitem_component_1 = wishitem_component_1_1;
            },
            function (wishitems_service_1_1) {
                wishitems_service_1 = wishitems_service_1_1;
            }],
        execute: function() {
            WishListComponent = (function () {
                function WishListComponent(_wishItemsService) {
                    this._wishItemsService = _wishItemsService;
                    this.currentDate = new Date();
                    this.newItem = {
                        id: 0,
                        position: 0,
                        text: 'test'
                    };
                }
                WishListComponent.prototype.getwishItems = function () {
                    var _this = this;
                    this._wishItemsService.getItems().then(function (items) { return _this.wishItems = items; });
                };
                WishListComponent.prototype.ngOnInit = function () {
                    this.getwishItems();
                };
                WishListComponent.prototype.addItem = function () {
                    console.log('Add clicked', this.wishItems);
                    //this.store.dispatch(addItem(this.newItem));
                    //this.newItem = '';
                };
                WishListComponent = __decorate([
                    core_1.Component({
                        selector: 'wish-list',
                        templateUrl: 'app/wishlist/wishlist.component.html',
                        styleUrls: ['app/wishlist/wishlist.component.css'],
                        directives: [wishitem_component_1.WishItemComponent]
                    }), 
                    __metadata('design:paramtypes', [wishitems_service_1.WishItemsService])
                ], WishListComponent);
                return WishListComponent;
            })();
            exports_1("WishListComponent", WishListComponent);
        }
    }
});
//# sourceMappingURL=wishlist.component.js.map