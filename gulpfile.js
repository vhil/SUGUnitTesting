/// <binding AfterBuild='Publish-Solution' />
var gulp = require("gulp");
var msbuild = require("gulp-msbuild");
var debug = require("gulp-debug");
var foreach = require("gulp-foreach");
var rename = require("gulp-rename");
var watch = require("gulp-watch");
var newer = require("gulp-newer");
var runSequence = require("run-sequence");
var path = require("path");
var config = require("./gulp-config.js")();
var nugetRestore = require('gulp-nuget-restore');
var fs = require('fs');

module.exports.config = config;

gulp.task("default", function (callback) {
  config.runCleanBuilds = true;
  return runSequence(
    "Publish-Solution",
	callback);
});

/*****************************
  Publish
*****************************/
gulp.task("Publish-Solution", function () {
	var dest = config.websiteRoot;
	var targets = ["Build"];
	if (config.runCleanBuilds) {
		targets = ["Clean", "Build"];
	}
	var projects = ["./src/**/*.csproj", "!./src/**/*.Tests.csproj"];
	console.log("publish to " + dest + " folder");
	return gulp.src(projects)
	  .pipe(foreach(function (stream, file) {
	  	return stream
		  .pipe(debug({ title: "Building project:" }))
		  .pipe(msbuild({
		  	targets: targets,
		  	configuration: config.buildConfiguration,
		  	logCommand: false,
		  	verbosity: "minimal",
		  	maxcpucount: 0,
		  	toolsVersion: 14.0,
		  	properties: {
		  		DeployOnBuild: "true",
		  		DeployDefaultTarget: "WebPublish",
		  		WebPublishMethod: "FileSystem",
		  		DeleteExistingFiles: "false",
		  		publishUrl: dest,
		  		_FindDependencies: "false"
		  	}
		  }));
	  }));
});

//gulp.task("Publish-Assemblies", function () {
//  var root = "./src";
//  var binFiles = [root + "/**/bin/Sug.**.{dll,pdb}", "!" + root + "/**/bin/Sug.**.Tests.{dll,pdb}"];
//  var destination = config.websiteRoot + "/bin/";
//  return gulp.src(binFiles, { base: root })
//    .pipe(rename({ dirname: "" }))
//    .pipe(newer(destination))
//    .pipe(debug({ title: "Copying " }))
//    .pipe(gulp.dest(destination));
//});

//gulp.task("Publish-All-Views", function () {
//  var root = "./src";
//  var roots = [root + "/**/Views", "!" + root + "/**/obj/**/Views"];
//  var files = "/**/*.cshtml";
//  var destination = config.websiteRoot + "\\Views";
//  return gulp.src(roots, { base: root }).pipe(
//    foreach(function (stream, file) {
//      console.log("Publishing from " + file.path);
//      gulp.src(file.path + files, { base: file.path })
//        .pipe(newer(destination))
//        .pipe(debug({ title: "Copying " }))
//        .pipe(gulp.dest(destination));
//      return stream;
//    })
//  );
//});

//gulp.task("Publish-All-Configs", function () {
//  var root = "./src";
//  var roots = [root + "/**/App_Config", "!" + root + "/**/obj/**/App_Config"];
//  var files = "/**/*.config";
//  var destination = config.websiteRoot + "\\App_Config";
//  return gulp.src(roots, { base: root }).pipe(
//    foreach(function (stream, file) {
//      console.log("Publishing from " + file.path);
//      gulp.src(file.path + files, { base: file.path })
//        .pipe(newer(destination))
//        .pipe(debug({ title: "Copying " }))
//        .pipe(gulp.dest(destination));
//      return stream;
//    })
//  );
//});

//gulp.task("Auto-Publish-Views", function () {
//  var root = "./src";
//  var roots = [root + "/**/Views", "!" + root + "/**/obj/**/Views"];
//  var files = "/**/*.cshtml";
//  var destination = config.websiteRoot + "\\Views";
//  gulp.src(roots, { base: root }).pipe(
//    foreach(function (stream, rootFolder) {
//      gulp.watch(rootFolder.path + files, function (event) {
//        if (event.type === "changed") {
//          console.log("publish this file " + event.path);
//          gulp.src(event.path, { base: rootFolder.path }).pipe(gulp.dest(destination));
//        }
//        console.log("published " + event.path);
//      });
//      return stream;
//    })
//  );
//});