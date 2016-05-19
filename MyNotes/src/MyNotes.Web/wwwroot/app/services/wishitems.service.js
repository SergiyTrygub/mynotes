"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('angular2/core');
var http_1 = require('angular2/http');
var Observable_1 = require('rxjs/Observable');
var WishItemsService = (function () {
    function WishItemsService(http) {
        this.http = http;
        this._apiUrl = 'api/wishitems'; // URL to web api
    }
    WishItemsService.prototype.saveWishItem = function (item) {
        var url = this._apiUrl;
        var body = JSON.stringify(item);
        var headers = new http_1.Headers({ 'Content-Type': 'application/json' });
        var options = new http_1.RequestOptions({ headers: headers });
        return this.http.post(url, body, options)
            .map(function (res) { return res.json().item; })
            .do(function (item) { return console.log(item); })
            .catch(this.handleError);
    };
    WishItemsService.prototype.removeWishItem = function (itemId) {
        var url = this._apiUrl + "/" + itemId;
        return this.http.delete(url);
    };
    WishItemsService.prototype.handleError = function (error) {
        // in a real world app, we may send the error to some remote logging infrastructure
        // instead of just logging it to the console
        console.error(error);
        return Observable_1.Observable.throw(error.json().error || 'Server error');
    };
    WishItemsService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_1.Http])
    ], WishItemsService);
    return WishItemsService;
}());
exports.WishItemsService = WishItemsService;
//# sourceMappingURL=wishitems.service.js.map