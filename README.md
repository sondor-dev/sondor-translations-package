# Sondor Translations
[![Build Status](https://dev.azure.com/sondortechnology/Sondor%20Infrastructure/_apis/build/status%2Fsondor-dev.sondor-translations-package?branchName=master)](https://dev.azure.com/sondortechnology/Sondor%20Infrastructure/_build/latest?definitionId=103&branchName=master) ![NuGet Downloads](https://img.shields.io/nuget/dt/Sondor.Translations)

Sondor translations is aims to provide an easy solution to managing translations,
offering easy to use solutions for static and dynamic translations.

## Features
1. Setup and easy to use configurable translation sources, defaulting to embedded resource files (`resx`).
2. Customizable translation providers, provide flexible and easy ways to read translations.
3. Default culture providers - Ways to recognize the current culture
   - Header `Accept-Language` sets the current culture context - [Read more](https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Accept-Language)
   - Cookie `.AspNetCore.Culture` sets the current culture context.
4. Easy-to-use options, setting the default and supported cultures.
5. Enable the translation key to be used as the default value, minimizing risk of errors.

## Install Translations
Install via NuGet
```cli
Install-Package Sondor.Translations
```
Install via .NET Core command line
```cli
dotnet add package Sondor.Translations
```

## Getting started
Follow the package instructions [here](/Sondor.Translations/Sondor.Translations/README.md).