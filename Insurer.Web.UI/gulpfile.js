
var gulp = require('gulp');
var browserify = require('browserify');
var babelify = require('babelify');
var source = require('vinyl-source-stream');
var uglify = require('gulp-uglify');
var path = require('path');
var buffer = require('vinyl-buffer');
var browserSync = require('browser-sync').create();
var eslint = require('gulp-eslint');
var pluginReact = require('eslint-plugin-react');
var historyApiFallback = require('connect-history-api-fallback');
var stripDebug = require('gulp-strip-debug');
var argv = require('yargs').argv;
var gulpif = require('gulp-if');
var del = require('del');
var runSequence = require('run-sequence');

var DEFAULT_PATH = "build";

gulp.task('browserify', function() {

    return browserify({
        entries: 'js/Index.js',
        debug: !argv.prod
    })
        .transform(babelify, {presets: ["es2015", "react"]})
        .bundle()
        .pipe(source('bundle.js'))
        .pipe(buffer())
        .pipe(gulpif(argv.prod, uglify()))
        .pipe(gulpif(argv.prod, stripDebug()))
        .pipe(gulp.dest(DEFAULT_PATH));
});


gulp.task('eslint', function () {
    return gulp.src([
        'js/*.js'
        ,'js/**/*.js'
    ])
        .pipe(eslint({
            "parserOptions": {
                "ecmaVersion": 6,
                "sourceType": "module",
                "ecmaFeatures": {
                    "jsx": true
                }
            },
            "env": {
                "browser": true,
                "node": true,
                "es6": true,
                "commonjs": true,
                "jquery": true
            },
            "plugins": [
                "react"
            ],
            "extends": ["eslint:recommended", "plugin:react/recommended"],
            "rules": {
                "eqeqeq": 1,
                "strict": 2,
                "no-console": 0,
                "react/no-did-mount-set-state": 0
            }
        }))
        .pipe(eslint.format())
        .pipe(eslint.failAfterError());
});

gulp.task('watch', function() {
    gulp.watch('js/**/*.js', ['eslint', 'browserify']);
    gulp.watch('js/Index.js', ['eslint', 'browserify']);
    gulp.watch('css/*.css', ['package']);
    gulp.watch('*.html', ['package']);
});

gulp.task('package', function() {
    return gulp.src(
        [
            '!test.html',
            '*.html',
            'css/*.css'
        ]
    ).pipe(gulp.dest(DEFAULT_PATH));
});

gulp.task('clean', function() {
    return del([DEFAULT_PATH]);
});

gulp.task('build', ['eslint', 'browserify', 'package', 'watch'], function(){

});

/*gulp.task('build', function(callback){
    return runSequence('eslint', 'browserify', 'package', 'watch', callback);
});*/

gulp.task('browserSync', ['build'], function() {
    browserSync.init([
        DEFAULT_PATH + '/bundle.js'
        ,DEFAULT_PATH + '/*.css'
        ,DEFAULT_PATH + '/*.html'
    ],{
        /*browser: ["chrome", "firefox", "iexplore"],*/
        browser: ["chrome"],
        server: {
            baseDir: DEFAULT_PATH,
            middleware: [ historyApiFallback() ]
        },
        https: true
    });
});

gulp.task('default', ['browserSync'], function(){
});