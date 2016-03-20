import {Component, ViewEncapsulation} from 'angular2/core';
import { RouteConfig, ROUTER_DIRECTIVES, ROUTER_PROVIDERS } from 'angular2/router';
import {WishListComponent} from './wishlist/wishlist.component'

@Component({
    selector: 'wish-day',
    templateUrl: '/views/mywishday.html',
    directives: [WishListComponent, ROUTER_DIRECTIVES],
    encapsulation: ViewEncapsulation.Native
})
@RouteConfig([
    {
        path: ':tenant',
        name: 'DefaultWishDay',
        component: WishListComponent,
        useAsDefault: true
    },
    {
        path: ':tenant/wishday/:date',
        name: 'WishDay',
        component: WishListComponent
    }
])
export class AppComponent {
}