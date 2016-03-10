﻿import {Component, Input} from 'angular2/core';
import {WishItem} from './../wishlist/wishitem';

@Component({
    selector: 'wish-item',
    templateUrl: 'app/wishlist/wishitem.component.html',
    styleUrls: ['app/wishlist/wishitem.component.css'],
})
export class WishItemComponent {
    @Input()
    item: WishItem;
}