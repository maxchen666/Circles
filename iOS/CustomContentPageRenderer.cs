using System;
using Xamarin.Forms.Platform.iOS;
using Circles;
using Circles.iOS;
using Foundation;
using UIKit;
using System.Drawing;
using Xamarin.Forms;

[assembly: Xamarin.Forms.ExportRendererAttribute (typeof(CustomContentPage), typeof(CustomContentPageRenderer))]

namespace Circles.iOS
{
	public class CustomContentPageRenderer: PageRenderer
	{
		private DateTime lastClick = DateTime.Now;

		public CustomContentPageRenderer ()
		{
		}
		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			base.TouchesBegan(touches, evt);

			// Add the in frame check here
			// get the touch
			UITouch touch = touches.AnyObject as UITouch;
			if (touch != null)
			{
				CustomContentPage page = ((CustomContentPage)this.Element);

				PointF posc = (PointF)touch.LocationInView (this.View);
				var circle = page.circles[0];
				bool found = false;
				foreach(var item in page.circles)
				{
					if (item.startX - item.sizeX <= posc.X && posc.X <= item.startX + item.sizeX
						&& item.startY  - item.sizeY <= posc.Y && posc.Y <= item.startY + item.sizeY) {
						if(!found)
						{
							circle = item;
							item.active = true;
							found = true;
						}
						else
						{
							item.active = false;
						}
					}
					else
					{
						item.active = false;
					}
				}
				if (!found) {
					circle.active = false;
					App.AddCircile(posc.X - circle.sizeX, posc.Y - circle.sizeY);
				} else {
					if (touch.TapCount == 2) {
						circle.placeholder.rgb = circle.placeholder.oldrgb;
						circle.placeholder.name = circle.placeholder.oldname;
						circle.nameLabel.IsVisible = !circle.nameLabel.IsVisible;
						circle.Update = !circle.Update;
					} else{
						circle.placeholder.oldrgb = circle.placeholder.rgb;
						circle.placeholder.oldname = circle.placeholder.name;
						circle.startX = posc.X;
						circle.startY = posc.Y;
						circle.Update = !circle.Update;
					}
				}
			}

			lastClick = DateTime.Now;
		}

		public override void TouchesCancelled(NSSet touches, UIEvent evt)
		{
			base.TouchesCancelled(touches, evt);
			UITouch touch = touches.AnyObject as UITouch;
			if (touch != null)
			{
				CustomContentPage page = ((CustomContentPage)this.Element);

				PointF posc = (PointF)touch.LocationInView (this.View);

				var circle = page.circles[0];
				bool found = false;
				foreach(var item in page.circles)
				{
					if(item.active)
					{
						circle = item;
						found = true;
						break;
					}
				}
				if (found) {
					if (circle.startX - circle.sizeX <= posc.X && posc.X <= circle.startX + circle.sizeX
						&& circle.startY  - circle.sizeY <= posc.Y && posc.Y <= circle.startY + circle.sizeY) {
						if (touch.TapCount == 1) {
							circle.placeholder.oldrgb = circle.placeholder.rgb;
							circle.placeholder.oldname = circle.placeholder.name;
							circle.placeholder.GetColor ();
							circle.Update = !circle.Update;
						}

					}
				}

			}
		}

		public override void TouchesEnded(NSSet touches, UIEvent evt)
		{
			base.TouchesEnded(touches, evt);
		}

		public override void TouchesMoved(NSSet touches, UIEvent evt)
		{
			base.TouchesMoved(touches, evt);
			// get the touch
			UITouch touch = touches.AnyObject as UITouch;
			if (touch != null)
			{
				CustomContentPage page = ((CustomContentPage)this.Element);

				PointF posc = (PointF)touch.LocationInView (this.View);

				var circle = page.circles[0];
				bool found = false;
				foreach(var item in page.circles)
				{
					if(item.active)
					{
						circle = item;
						found = true;
						break;
					}
				}
				if (found) {
					if (circle.startX - circle.sizeX <= posc.X && posc.X <= circle.startX + circle.sizeX
						&& circle.startY  - circle.sizeY <= posc.Y && posc.Y <= circle.startY + circle.sizeY) {
						if (touch.TapCount == 1) {
							circle.startX = posc.X;
							circle.startY = posc.Y;
							circle.Update = !circle.Update;
						}

					}
				}
				lastClick = DateTime.Now;

			}
		}
	}
}

