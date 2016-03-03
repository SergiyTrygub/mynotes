System.register([], function(exports_1) {
    var WishItem;
    return {
        setters:[],
        execute: function() {
            WishItem = (function () {
                function WishItem(data) {
                    if (data === void 0) { data = undefined; }
                    data = data || { text: '', completed: false };
                    this._data = new Map(data);
                }
                Object.defineProperty(WishItem.prototype, "text", {
                    get: function () {
                        return this._data.get('text');
                    },
                    enumerable: true,
                    configurable: true
                });
                WishItem.prototype.setText = function (value) {
                    return new WishItem(this._data.set('text', value));
                };
                return WishItem;
            })();
            exports_1("WishItem", WishItem);
        }
    }
});
//# sourceMappingURL=wishitem.js.map