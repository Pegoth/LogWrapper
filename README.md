## Possible values

`LogLevel` (choose one):
* `Verbose`/`Trace`: The most verbose log level.
* `Debug`: Used for debugging purposes, quite spammy, but not as much as Verbose/Trace.
* `Information`: Used for informational messages, such as startup messages.
* `Warning`: Used for warning messages, such as a configuration that is not optimal.
* `Error`: Used for error messages, such as expected exceptions, that can be handled.
* `Fatal`/`Critical`: Used for fatal messages, such as exceptions that cause application shutdown.

`Sink` (can be multiple):
* `Console`: Output to console and debugger. Requires `LogWrapper.Sink.Console` package.
* `File`: Output to file. Requires `LogWrapper.Sink.File` package and `LogFilesPath` option to be set.

`LogFilesPath` (required for `File` sink):
* Directory that will hold the log files.

## Example config

`appsettings.json`:
```json
{
  "LogSettings": {
    "LogLevel": "Information",
    "Sinks": "Console,File",
    "LogFilesPath": "logs"
  }
}
```

`appsettings.Development.json`:
```json
{
  "LogSettings": {
    "LogLevel": "Trace",
    "Sinks": "Console"
  }
}
```
