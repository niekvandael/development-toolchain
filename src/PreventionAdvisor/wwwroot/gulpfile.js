var gulp = require('gulp');
var sourcemaps = require('gulp-sourcemaps');
var concat = require('gulp-concat');
var typescript = require('gulp-typescript');
var systemjsBuilder = require('systemjs-builder');
var uglify = require('gulp-uglify');
var embedTemplates = require('gulp-angular-embed-templates');

// Compile TypeScript app to JS
gulp.task('compile:ts', function () {
  return gulp
    .src([
        "appTS/**/*.ts",
        "typings/*.d.ts"
    ])
    .pipe(sourcemaps.init())
    .pipe(typescript({
        "module": "system",
        "moduleResolution": "node",
        "outDir": "app",
        "target": "ES5"
    }))
    .pipe(sourcemaps.write('.'))
    .pipe(gulp.dest('app'))
//    .on('end', function () {
//            gulp
//                .src('./**/*.js')
//                .pipe(embedTemplates())
//                .pipe(gulp.dest('embedded'));
//            });
        });


// Generate systemjs-based bundle (app/app.js)
gulp.task('bundle:app', function() {
  var builder = new systemjsBuilder('', './systemjs.config.js');
  return builder.buildStatic('app', 'app/app.js');
});

// Copy and bundle dependencies into one file (vendor/vendors.js)
// systemjs.config.js can also bundled for convenience
gulp.task('bundle:vendor', function () {
    return gulp.src([
        'node_modules/core-js/client/shim.min.js',
        'node_modules/zone.js/dist/zone.js',
        'node_modules/reflect-metadata/Reflect.js',
        'node_modules/systemjs/dist/system.js',
        'node_modules/web-animations-js/web-animations-next.min.js',
        'systemjs.config.js',
      ])
        .pipe(concat('vendors.js'))
        .pipe(gulp.dest('vendor'));
});

// Copy dependencies loaded through SystemJS into dir from node_modules
gulp.task('copy:vendor', function () {
    return gulp.src([
        'node_modules/rxjs/bundles/Rx.js',
        'node_modules/@angular/**/*'
    ])
    .pipe(gulp.dest('vendor'));
});

gulp.task('vendor', ['bundle:vendor', 'copy:vendor']);
gulp.task('app', ['compile:ts', 'bundle:app']);

// Bundle dependencies and app into one file (app.bundle.js)
gulp.task('bundle', ['vendor', 'app'], function () {
    return gulp.src([
        'app/app.js',
        'vendor/vendors.js'
        ])
    .pipe(concat('app.bundle.js'))
//    .pipe(uglify())
    .pipe(gulp.dest('./app'));
});

gulp.task('default', ['bundle']);