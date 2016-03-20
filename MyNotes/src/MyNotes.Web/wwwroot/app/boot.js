System.register(['angular2/platform/browser', 'angular2/http', './app.component', './services/wishitems.service', './services/wishdays.service', 'rxjs/Rx', 'angular2/router'], function(exports_1) {
    var browser_1, http_1, app_component_1, wishitems_service_1, wishdays_service_1, router_1;
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
            function (_1) {},
            function (router_1_1) {
                router_1 = router_1_1;
            }],
        execute: function() {
            browser_1.bootstrap(app_component_1.AppComponent, [router_1.ROUTER_PROVIDERS, http_1.HTTP_PROVIDERS, wishdays_service_1.WishDaysService, wishitems_service_1.WishItemsService]);
        }
    }
});
//# sourceMappingURL=boot.js.map