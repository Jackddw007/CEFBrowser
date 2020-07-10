using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CefiBrowser
{
	public class TitleChangedEventArgs : EventArgs
	{
		public TitleChangedEventArgs(string title,CefWebBrowser browser)
		{
			Title = title;
            CefWeb = browser;
		}

		public string Title { get; private set; }
        public CefWebBrowser CefWeb { get; private set; }
	}
}
