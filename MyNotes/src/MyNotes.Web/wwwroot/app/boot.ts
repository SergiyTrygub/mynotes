///<reference path="../../node_modules/angular2/typings/browser.d.ts"/>
import {bootstrap}    from 'angular2/platform/browser'
import {HTTP_PROVIDERS} from 'angular2/http';
import {AppComponent} from './app.component'
import {WishItemsService} from './services/wishitems.service';
import {WishDaysService} from './services/wishdays.service';
import 'rxjs/Rx';
import { RouteConfig, ROUTER_DIRECTIVES, ROUTER_PROVIDERS } from 'angular2/router';

bootstrap(AppComponent, [ROUTER_PROVIDERS, HTTP_PROVIDERS, WishDaysService, WishItemsService]);