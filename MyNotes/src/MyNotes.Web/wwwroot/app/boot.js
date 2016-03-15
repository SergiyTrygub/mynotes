System.register(['angular2/platform/browser', 'angular2/http', './app.component', './services/wishitems.service', './services/wishdays.service', 'rxjs/Rx'], function(exports_1) {
    var browser_1, http_1, app_component_1, wishitems_service_1, wishdays_service_1;
    return {
        setters:[
            function (browser_1_1) {
                browser_1 = browser_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (app_component_1_1) {
                app_component_1 = app_component_1_1;
            },
            function (wishitems_service_1_1) {
                wishitems_service_1 = wishitems_service_1_1;
            },
            function (wishdays_service_1_1) {
                wishdays_service_1 = wishdays_service_1_1;
            },
            function (_1) {}],
        execute: function() {
            browser_1.bootstrap(app_component_1.AppComponent, [http_1.HTTP_PROVIDERS, wishdays_service_1.WishDaysService, wishitems_service_1.WishItemsService]);
        }
    }
});
//# sourceMappingURL=boot.js.map