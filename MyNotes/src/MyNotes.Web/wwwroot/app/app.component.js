System.register(['angular2/core', './wishlist/wishitem.component'], function(exports_1) {
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, wishitem_component_1;
    var WishItems, AppComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (wishitem_component_1_1) {
                wishitem_component_1 = wishitem_component_1_1;
            }],
        execute: function() {
            WishItems = [
                { id: 1, position: 1, text: 'test 1' },
                { id: 2, position: 2, text: 'test 2' }
            ];
            AppComponent = (function () {
                function AppComponent() {
                    this.currentDate = new Date();
                    this.newItem = {
                        id: 0,
                        position: 0,
                        text: 'test'
                    };
                    this.wishItems = WishItems;
                }
                AppComponent.prototype.addItem = function () {
                    console.log('Add clicked', this.wishItems);
                    //this.store.dispatch(addItem(this.newItem));
                    //this.newItem = '';
                };
                AppComponent = __decorate([
                    core_1.Component({
                        selector: 'wish-list',
                        templateUrl: 'views/mywishday.html',
                        directives: [wishitem_component_1.WishItemComponent]
                    }), 
                    __metadata('design:paramtypes', [])
                ], AppComponent);
                return AppComponent;
            })();
            exports_1("AppComponent", AppComponent);
        }
    }
});
//# sourceMappingURL=app.component.js.map