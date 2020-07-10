using System;
using Xilium.CefGlue;

namespace CefiBrowser
{
	public class RenderProcessTerminatedEventArgs : EventArgs
	{
		public RenderProcessTerminatedEventArgs(CefTerminationStatus status)
		{
			Status = status;
		}

		public CefTerminationStatus Status { get; private set; }
	}
}
