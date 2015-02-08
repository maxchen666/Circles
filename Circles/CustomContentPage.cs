using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Circles
{
	public class CustomContentPage: ContentPage
	{
		public List<CircleViewModel> circles;
		public CustomContentPage ()
		{
			circles = new List<CircleViewModel> ();
		}
	}
}

