var gulp = require('gulp'),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify");

var paths = {
    webroot: "./wwwroot/"
};
paths.css = paths.webroot + "Style/**/*.css";
paths.minCss = paths.webroot + "Style/**/*.min.css";
paths.concatVendorCssDest = paths.webroot + "css/vendor.min.css";
paths.concatCssDest = paths.webroot + "css/site.min.css";

gulp.task("angular2:moveLibs", function () {
    return gulp.src([
            "node_modules/angular2/bundles/angular2-polyfills.js",
            "node_modules/systemjs/dist/system.src.js",
            "node_modules/systemjs/dist/system-polyfills.js",
            "node_modules/rxjs/bundles/Rx.js",
            "node_modules/angular2/bundles/angular2.dev.js"
    ])
        .pipe(gulp.dest(paths.webroot + "lib"));
});

gulp.task("angular2:moveJs", function () {
    return gulp.src(["Scripts/**/*.js", "Scripts/*.js"])
        .pipe(gulp.dest(paths.webroot + "app/"));

});

gulp.task("min:vendor_css", function () {
    return gulp.src(["node_modules/bootstrap/dist/css/bootstrap.min.css", ])
        .pipe(concat(paths.concatVendorCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min:css", function () {
    return gulp.src([paths.css, "!" + paths.minCss])
        .pipe(concat(paths.concatCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});


gulp.task("run", ["angular2:moveLibs", "angular2:moveJs", "min:css", "min:vendor_css"])