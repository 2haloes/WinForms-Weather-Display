# WinForms-Weather-Display
While this is intended for developers and a quick guide into using the DarkSky API with C#, anyone can use it (It's not like I can stop anyone anyway)

This is a program designed to run on Windows and Linux (I can't test on Mac so I can't tell if it works, it should under Mono however). The only requirement is a screen with a size of at least 800X480.

#Universal Setup
This uses the DarkSky api and as such, it requires a DarkSky account to use, it also requires the units you want to use (US for °F and SI for °C)
While not required, I highly recommend also getting the Lat and Long of the lacation you want the weather for (There are services for that), if not, it uses a GeoIp service and it will likely not be accurate

After you have everything, you have to edit the DisplayConfig.cfg, the the first line is the DarkSky API key, the second line is the units to use (US or SI), the third and fourth lines are for Lat and Long respectively, they can be left empty and a GeoIp service will be used
The Icons folder is also empty, it will need to be filled with the following (Note, only png files are supported right now. If anyone can point me to a public domain weather icon pack, I'll include it)
-clear-day
-clear-night
-cloudy
-fog
-partly-cloudy-day
-partly-cloudy-night
-rain
-sleet
-snow
-sunrise
-sunset
-wind

#Windows Setup
Ensure that you have .NET 4.5 installed, ensure that the universal setup is done and all you need to do is double click the .exe

#Linux Setup
Install Mono (A version compatable with .NET 4.5 is required)
Either use terminal or a graphical file browser to  load the program using Mono.
(If using terminal to load the program, you will need to cd to the dictonary of the exe)

#More to add here, including updating the readme and uploading a release
