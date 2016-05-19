"use strict";
///<reference path="../../node_modules/angular2/typings/browser.d.ts"/>
var browser_1 = require('angular2/platform/browser');
var http_1 = require('angular2/http');
var app_component_1 = require('./app.component');
var wishitems_service_1 = require('./services/wishitems.service');
var wishdays_service_1 = require('./services/wishdays.service');
require('rxjs/Rx');
var router_1 = require('angular2/router');
browser_1.bootstrap(app_component_1.AppComponent, [router_1.ROUTER_PROVIDERS, http_1.HTTP_PROVIDERS, wishdays_service_1.WishDaysService, wishitems_service_1.WishItemsService]);
//# sourceMappingURL=boot.js.map