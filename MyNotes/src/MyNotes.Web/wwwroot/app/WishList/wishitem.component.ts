import {Component, Input, Output, EventEmitter, ViewEncapsulation} from 'angular2/core';
import {WishItem} from './../wishlist/wishitem';

@Component({
    selector: 'wish-item',
    templateUrl: 'app/wishlist/wishitem.component.html',
    styleUrls: ['app/wishlist/wishitem.component.css'],
    encapsulation: ViewEncapsulation.Native
})
export class WishItemComponent {
    editMode = false;

    @Input()
    item: WishItem;

    @Output()
    onWishItemRemove = new EventEmitter<number>();

    @Output()
    onWishItemUpdated = new EventEmitter<WishItem>();

    removeWishItemClicked() {
        this.onWishItemRemove.emit(this.item.id);
    }

    enterEditMode(element: HTMLInputElement) {
        console.log('edit');
        this.editMode = true;
        if (this.editMode) {
            setTimeout(() => { element.focus(); }, 0);
        }
    }

    cancelEdit(element: HTMLInputElement) {
        this.editMode = false;
        element.value = this.item.text;
    }

    commitEdit(updatedText: string) {
        this.editMode = false;
        this.onWishItemUpdated.emit({
            id: this.item.id,
            position: this.item.position,
            wishDayId: this.item.wishDayId,
            text: updatedText
        });
    }
}