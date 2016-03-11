import {Component} from 'angular2/core';
import {WishListComponent} from './wishlist/wishlist.component'

@Component({
    selector: 'wish-day',
    template: '<wish-list></wish-list>',
    directives: [WishListComponent]
})
export class AppComponent {
    
}