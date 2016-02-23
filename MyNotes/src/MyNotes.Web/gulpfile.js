var gulp = require('gulp');

var paths = {
    webroot: "./wwwroot/"
};

gulp.task("angular2:moveLibs", function () {
    return gulp.src([
            "node_modules/angular2/bundles/angular2-polyfills.js",
            "node_modules/systemjs/dist/system.src.js",
            "node_modules/systemjs/dist/system-polyfills.js",
            "node_modules/rxjs/bundles/Rx.js",
            "node_modules/angular2/bundles/angular2.dev.js"
    ])
        .pipe(gulp.dest(paths.webroot + "Angular"));
});

gulp.task("angular2:moveJs", function () {
    return gulp.src(["Scripts/**/*.js", "Scripts/*.js"])
        .pipe(gulp.dest(paths.webroot + "Angular/app/"));

});

gulp.task("angular2", ["angular2:moveLibs", "angular2:moveJs"])