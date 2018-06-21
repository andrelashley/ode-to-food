/// <binding AfterBuild='default' />
var gulp = require('gulp');
var uglify = require('gulp-uglify');
var concat = require('gulp-concat');

var files = [
    'node_modules/jquery/dist/**/*.js',
    'node_modules/jquery-validation/dist/**/*.js',
    'node_modules/jquery-validation-unobtrusive/**/*.js',
    'node_modules/bootstrap/dist/js/**/*.js'
];

gulp.task("minify", function () {
    return gulp.src(files)
        .pipe(uglify())
        .pipe(concat("odetofood.min.js"))
        .pipe(gulp.dest("wwwroot/dist"));
});

gulp.task('default', ["minify"]);