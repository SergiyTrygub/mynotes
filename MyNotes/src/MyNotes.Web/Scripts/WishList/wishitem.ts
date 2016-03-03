export class WishItem {
    _data: Map<string, any>;

    get text() {
        return this._data.get('text');
    }

    setText(value: string) {
        return new WishItem(this._data.set('text', value));
    }

    constructor(data: any = undefined) {
        data = data || { text: '', completed: false };
        this._data = new Map<string, any>(data);
    }
}