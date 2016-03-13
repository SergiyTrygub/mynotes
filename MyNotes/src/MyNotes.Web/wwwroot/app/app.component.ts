import {Component} from 'angular2/core';
import {WishListComponent} from './wishlist/wishlist.component'

@Component({
    selector: 'wish-day',
    templateUrl: '/views/mywishday.html',
    directives: [WishListComponent]
})
export class AppComponent {
}