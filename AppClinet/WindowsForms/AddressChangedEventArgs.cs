using System;
using Xilium.CefGlue;

namespace CefiBrowser
{
	public class AddressChangedEventArgs : EventArgs
	{
		public AddressChangedEventArgs(CefFrame frame, string address,CefWebBrowser browser)
		{
			Address = address;
			Frame = frame;
            CefWeb = browser;

        }

		public string Address { get; private set; }

		public CefFrame Frame { get; private set; }

        public CefWebBrowser CefWeb { get; private set; }

    }
}
