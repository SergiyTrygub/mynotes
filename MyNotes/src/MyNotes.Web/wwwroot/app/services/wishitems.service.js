System.register(['angular2/core', 'angular2/http', 'rxjs/Observable'], function(exports_1) {
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, http_1, Observable_1;
    var WishItems, WishItemsService;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (Observable_1_1) {
                Observable_1 = Observable_1_1;
            }],
        execute: function() {
            WishItems = [
                { id: 1, position: 1, text: 'test 1' },
                { id: 2, position: 2, text: 'test 2' },
                { id: 3, position: 3, text: 'test 3' },
            ];
            WishItemsService = (function () {
                function WishItemsService(http) {
                    this.http = http;
                    this._apiUrl = 'api/wishdays'; // URL to web api
                }
                WishItemsService.prototype.getItems = function (tenantId) {
                    var url = this._apiUrl + tenantId;
                    console.log(url);
                    return this.http.get(url)
                        .map(function (res) { return res.json().data; })
                        .do(function (data) { return console.log(data); })
                        .catch(this.handleError);
                };
                WishItemsService.prototype.createNewDay = function (tenantId) {
                    var url = this._apiUrl + tenantId;
                    console.log("createNewDay", url);
                    var body = JSON.stringify({ id: 0, date: new Date() });
                    var headers = new http_1.Headers({ 'Content-Type': 'application/json' });
                    var options = new http_1.RequestOptions({ headers: headers });
                    return this.http.post(url, body, options)
                        .map(function (res) { return res.json().item; })
                        .do(function (item) { return console.log(item); })
                        .catch(this.handleError);
                };
                WishItemsService.prototype.saveWishDay = function (tenantId, wishDay) {
                    var url = this._apiUrl + tenantId + "/" + wishDay.date;
                    var body = JSON.stringify(wishDay);
                    var headers = new http_1.Headers({ 'Content-Type': 'application/json' });
                    var options = new http_1.RequestOptions({ headers: headers });
                    return this.http.put(url, body, options)
                        .map(function (res) { return res.json().item; })
                        .do(function (item) { return console.log(item); })
                        .catch(this.handleError);
                };
                WishItemsService.prototype.removeWishItem = function (tenantId, itemId) {
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
            })();
            exports_1("WishItemsService", WishItemsService);
        }
    }
});
//# sourceMappingURL=wishitems.service.js.map