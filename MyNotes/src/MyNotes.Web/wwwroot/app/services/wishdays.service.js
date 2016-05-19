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
var WishDaysService = (function () {
    function WishDaysService(http) {
        this.http = http;
        this._apiUrl = 'api/wishdays'; // URL to web api
    }
    WishDaysService.prototype.getWishDays = function (tenantId) {
        var url = this._apiUrl + tenantId;
        console.log(url);
        return this.http.get(url)
            .map(function (res) { return res.json().data; })
            .do(function (data) { return console.log(data); })
            .catch(this.handleError);
    };
    WishDaysService.prototype.getWishDay = function (tenantId, date) {
        var url = this._apiUrl + '/' + tenantId + '/' + (date.getFullYear() + '-' + ('0' + (date.getMonth() + 1)).slice(-2) + '-' + ('0' + date.getDate()).slice(-2));
        console.log(url);
        return this.http.get(url)
            .map(function (res) {
            return res.json();
        })
            .do(function (day) {
            if (day) {
                if (!day.wishList) {
                    day.wishList = [];
                }
                day.date = new Date(day.date.toString());
            }
        })
            .catch(this.handleError);
    };
    WishDaysService.prototype.createNewDay = function (tenantId) {
        var url = this._apiUrl + '/' + tenantId;
        console.log("createNewDay", url);
        var d = new Date();
        d.setDate(d.getDate() + 1);
        var body = JSON.stringify({ id: 0, date: d });
        var headers = new http_1.Headers({ 'Content-Type': 'application/json' });
        var options = new http_1.RequestOptions({ headers: headers });
        return this.http.post(url, body, options)
            .map(function (res) { return res.json().item; })
            .do(function (item) {
            console.log(item);
        })
            .catch(this.handleError);
    };
    WishDaysService.prototype.saveWishDay = function (tenantId, wishDay) {
        var url = this._apiUrl + tenantId + "/" + wishDay.date;
        var body = JSON.stringify(wishDay);
        var headers = new http_1.Headers({ 'Content-Type': 'application/json' });
        var options = new http_1.RequestOptions({ headers: headers });
        return this.http.put(url, body, options)
            .map(function (res) { return res.json().item; })
            .do(function (item) { return console.log(item); })
            .catch(this.handleError);
    };
    WishDaysService.prototype.handleError = function (error) {
        // in a real world app, we may send the error to some remote logging infrastructure
        // instead of just logging it to the console
        console.error(error);
        return Observable_1.Observable.throw(error.json().error || 'Server error');
    };
    WishDaysService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_1.Http])
    ], WishDaysService);
    return WishDaysService;
}());
exports.WishDaysService = WishDaysService;
//# sourceMappingURL=wishdays.service.js.map