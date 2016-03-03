import {Component} from 'angular2/core';

@Component({
    selector: 'wish-list',
    templateUrl: 'views/mywishday.html'
})
export class AppComponent {
    public currentDate = new Date();
    public newItem = 'test';

    addItem() {
        console.log('Add clicked', this.newItem);
        //this.store.dispatch(addItem(this.newItem));
        //this.newItem = '';
    }
}