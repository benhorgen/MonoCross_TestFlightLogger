MonoCross_TestFlightLogger
==========================

Use TestFlight to log to from your MonoCross App

To setup:

1. Reference your MonoCross.Utilities project from the MonoCross_TestFlightLogger project contained in this repo. 

2.) Add a reference to MonoCross_TestFlightLogger to your MonoCross.Touch container app.

3.) In your Touch Container's AppDelegate override, set your MXDevice Logger using the following lines of code:

"string teamToken = "[your Token from TestFlight]";
"MXDevice.Log = new TestFlightLogger(teamToken, 0x10);"