///<reference path="../../node_modules/angular2/typings/browser.d.ts"/>
import {bootstrap}    from 'angular2/platform/browser'
import {HTTP_PROVIDERS} from 'angular2/http';
import {AppComponent} from './app.component'
import {WishItemsService} from './services/wishitems.service';
import {WishDaysService} from './services/wishdays.service';
import 'rxjs/Rx';

bootstrap(AppComponent, [HTTP_PROVIDERS, WishDaysService, WishItemsService]);