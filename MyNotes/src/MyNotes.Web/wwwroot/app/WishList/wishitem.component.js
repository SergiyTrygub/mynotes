System.register(['angular2/core'], function(exports_1) {
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1;
    var WishItemComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            }],
        execute: function() {
            WishItemComponent = (function () {
                function WishItemComponent() {
                    this.editMode = false;
                    this.onWishItemRemove = new core_1.EventEmitter();
                    this.onWishItemUpdated = new core_1.EventEmitter();
                }
                WishItemComponent.prototype.removeWishItemClicked = function () {
                    this.onWishItemRemove.emit(this.item.id);
                };
                WishItemComponent.prototype.enterEditMode = function (element) {
                    console.log('edit');
                    this.editMode = true;
                    if (this.editMode) {
                        setTimeout(function () { element.focus(); }, 0);
                    }
                };
                WishItemComponent.prototype.cancelEdit = function (element) {
                    this.editMode = false;
                    element.value = this.item.text;
                };
                WishItemComponent.prototype.commitEdit = function (updatedText) {
                    this.editMode = false;
                    this.onWishItemUpdated.emit({
                        id: this.item.id,
                        position: this.item.position,
                        wishDayId: this.item.wishDayId,
                        text: updatedText
                    });
                };
                __decorate([
                    core_1.Input(), 
                    __metadata('design:type', Object)
                ], WishItemComponent.prototype, "item", void 0);
                __decorate([
                    core_1.Output(), 
                    __metadata('design:type', Object)
                ], WishItemComponent.prototype, "onWishItemRemove", void 0);
                __decorate([
                    core_1.Output(), 
                    __metadata('design:type', Object)
                ], WishItemComponent.prototype, "onWishItemUpdated", void 0);
                WishItemComponent = __decorate([
                    core_1.Component({
                        selector: 'wish-item',
                        templateUrl: 'app/wishlist/wishitem.component.html',
                        styleUrls: ['app/wishlist/wishitem.component.css'],
                        encapsulation: core_1.ViewEncapsulation.Native
                    }), 
                    __metadata('design:paramtypes', [])
                ], WishItemComponent);
                return WishItemComponent;
            })();
            exports_1("WishItemComponent", WishItemComponent);
        }
    }
});
//# sourceMappingURL=wishitem.component.js.map