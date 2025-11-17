# Anduin's Happy Recorder

[![MIT licensed](https://img.shields.io/badge/license-MIT-blue.svg)](https://gitlab.aiursoft.com/anduin/HappyRecorder/-/blob/master/LICENSE)
[![Pipeline stat](https://gitlab.aiursoft.com/anduin/HappyRecorder/badges/master/pipeline.svg)](https://gitlab.aiursoft.com/anduin/HappyRecorder/-/pipelines)
[![Test Coverage](https://gitlab.aiursoft.com/anduin/HappyRecorder/badges/master/coverage.svg)](https://gitlab.aiursoft.com/anduin/HappyRecorder/-/pipelines)
[![NuGet version (Anduin.HappyRecorder)](https://img.shields.io/nuget/v/Anduin.HappyRecorder.svg)](https://www.nuget.org/packages/Anduin.HappyRecorder/)
[![ManHours](https://manhours.aiursoft.com/r/gitlab.aiursoft.com/anduin/happyrecorder.svg)](https://gitlab.aiursoft.com/anduin/happyrecorder/-/commits/master?ref_type=heads)

This project uses some simple mathematical models to calculate your current physical state by recording the time when you have happy each time. It helps users establish a scientifically healthy behavior cycle and avoid excessive physical consumption.

## Install

Requirements:

1. [.NET 10 SDK](http://dot.net/)

Run the following command to install this tool:

```bash
dotnet tool install --global Anduin.HappyRecorder
```

## Usage

After getting the binary, run it directly in the terminal.

```bash
$ happy-recorder
Description:
  A cli tool project helps recorder happy times and reactions.

Usage:
  happy-recorder [command] [options]

Options:
  -d, --dry-run   Preview changes without actually making them
  -v, --verbose   Show detailed log
  --version       Show version information
  -?, -h, --help  Show help and usage information

Commands:
  get     Database result fetcher.
  mark    Add a new happy record to the database.
  config  Configuration management.
```
