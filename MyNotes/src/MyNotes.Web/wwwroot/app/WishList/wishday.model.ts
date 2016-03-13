import {WishItem} from './../wishlist/wishitem';

export interface WishDay {
    id: number;
    date: Date;
    wishList: WishItem[];
}