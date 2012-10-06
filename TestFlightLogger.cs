using System;
using MonoTouch.UIKit;
using MonoTouch.TestFlight;

using MonoCross.Utilities.Logging;

namespace MonoCross.Utilities.Logging
{
	public class TestFlightLogger : ConsoleLogger, ILog
	{
		public TestFlightLogger(string teamToken, int flags): base() 
		{ 
			CheckPointError = false;
			CheckPointWarn = false;
			CheckPointInfo = false;
			CheckPointDebug = false;
			CheckPointMetric = false;
#if DEBUG
			TestFlight.TakeOff(teamToken);

			// TODO: Replace DEBUG compiler directive w/ OpenUDID-style replacement for udid string
			string udid = UIDevice.CurrentDevice.UniqueIdentifier;
			Console.WriteLine("Registering Device ID as: " + udid);
			TestFlight.SetDeviceIdentifier(udid);

			int flag = flags & EnabledError;
			if (flag == EnabledError) { CheckPointError = true; }

			flag = flags & EnabledWarn;
			if (flag == EnabledWarn) { CheckPointWarn = true; }

			flag = flags & EnabledInfo;
			if (flag == EnabledInfo) { CheckPointInfo = true; }

			flag = flags & EnabledDebug;
			if (flag == EnabledDebug) { CheckPointDebug = true; }

			flag = flags & EnabledMetric;
			if (flag == EnabledMetric) { CheckPointMetric = true; }
#endif
		}

		public override void AppendLog(String message, LogMessageType messageType)
		{
			string textEntry = string.Format("{0:MM-dd-yyyy HH:mm:ss:ffff} :{1}: [{2}] {3}", DateTime.Now, System.Threading.Thread.CurrentThread.ManagedThreadId, messageType.ToString(), message);

			//First write to console
			Console.WriteLine(textEntry);

			bool passCheckPoint = false;
			if (CheckPointError && LogMessageType.Error == messageType) { passCheckPoint = true; }
			else if (CheckPointWarn && LogMessageType.Warn == messageType) { passCheckPoint = true; }
			else if (CheckPointInfo && LogMessageType.Info == messageType) { passCheckPoint = true; }
			else if (CheckPointDebug && LogMessageType.Debug == messageType) { passCheckPoint = true; }
			else if (CheckPointMetric && LogMessageType.Metric == messageType) { passCheckPoint = true; }

			// Log a Checkpoint if necessary
			if (passCheckPoint) { TestFlight.PassCheckpoint(message); }

			AppendText(textEntry);
		}

		bool CheckPointError;
		bool CheckPointWarn;
		bool CheckPointInfo;
		bool CheckPointDebug;
		bool CheckPointMetric;

		const int EnabledError = 0x10;
		const int EnabledWarn = 0x08;
		const int EnabledInfo = 0x04;
		const int EnabledDebug = 0x02;
		const int EnabledMetric = 0x01;
	}
}

