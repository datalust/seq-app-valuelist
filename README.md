# _Seq.App.ValueList_ [![NuGet Package](https://img.shields.io/nuget/vpre/Seq.App.ValueList)](https://www.nuget.org/packages/seq.app.valuelist) [![Build status](https://ci.appveyor.com/api/projects/status/5vgsjl8usuoapwb0/branch/dev?svg=true)](https://ci.appveyor.com/project/datalust/seq-app-valuelist/branch/dev)

An simple Seq App example, currently under development.

The app is configured with the name of an event property. Whenever the app sees a new value for the property, it raises an event with both the new value and the list of all values seen so far.

![Screenshot](https://github.com/datalust/seq-app-valuelist/raw/dev/asset/ValueList.png)

Property values are stored in a text file under the app's storage folder, so they survive server restarts. The text file is rewritten every time a new value is seen; no provisions are made to avoid corruption in case of IO failures etc.
