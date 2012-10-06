MonoCross_TestFlightLogger
==========================

Use TestFlight to log to from your MonoCross App

To setup, reference your MonoCross.Utilities project to the MonoCross_TestFlightLogger project
in this repo, and add a reference to MonoCross_TestFlightLogger to your MonoCross.Touch 
container.  

In your AppDelegate override, set your Logger using the following lines of code:

"string teamToken = "[your Token from TestFlight]";
"MXDevice.Log = new TestFlightLogger(teamToken, 0x10);"