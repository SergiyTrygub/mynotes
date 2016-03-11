System.register(['angular2/platform/browser', './app.component', './services/wishitems.service'], function(exports_1) {
    var browser_1, app_component_1, wishitems_service_1;
    return {
        setters:[
            function (browser_1_1) {
                browser_1 = browser_1_1;
            },
            function (app_component_1_1) {
                app_component_1 = app_component_1_1;
            },
            function (wishitems_service_1_1) {
                wishitems_service_1 = wishitems_service_1_1;
            }],
        execute: function() {
            browser_1.bootstrap(app_component_1.AppComponent, [wishitems_service_1.WishItemsService]);
        }
    }
});
//# sourceMappingURL=boot.js.map