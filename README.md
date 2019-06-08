# SonarQube F# Code Analyzer Plugin

The plugin enables analysis of F# within SonarQube.

[![Build status](https://ci.appveyor.com/api/projects/status/jira637y22trnuc4?svg=true)](https://ci.appveyor.com/project/jorgecosta/sonar-fsharp-plugin-wxq94)

Download latest snapshot from : <https://ci.appveyor.com/project/jorgecosta/sonar-fsharp-plugin-wxq94/build/artifacts>

## Description / Features

- Metrics: LOC, number of classes, number of methods
- Code duplication detection
- FSharpLint Support
- Unit test metrics and Coverage
- Runs under windows and Linux (mono)

## Configuration

### Installation

Download the JAR plugin file from releases and copy it to the _extensions/plugins/_
directory of your SonarQube installation then start server.
The file _logs/sonar.log_ will then contain a log line indicating the loaded
plugin or any errors. The installed plgin will also be shown
on the Marketplace of your SonarQube installation.

Review the F# quality profile before running.

### General Configuration

- sonar.fs.file.suffixes - files extensions to import

### Coverage

 Generate reports and feed using the following properties:

- sonar.fs.ncover3.reportsPaths
- sonar.fs.opencover.reportsPaths
- sonar.fs.dotcover.reportsPaths
- sonar.fs.vscoveragexml.reportsPaths

### Test Reports

Generate reports and feed using the following properties:

- sonar.fs.vstest.reportsPaths
- sonar.fs.nunit.reportsPaths
