using System;
using Xamarin.Forms.Platform.Android;
using Circles;
using Circles.Droid;
using System.Drawing;
using Xamarin.Forms;
using Android.Text.Method;
using Android.Views;

[assembly: Xamarin.Forms.ExportRendererAttribute (typeof(CustomContentPage), typeof(CustomContentPageRenderer))]

namespace Circles.Droid
{
	public class CustomContentPageRenderer: PageRenderer
	{
		private DateTime lastClick = DateTime.Now;
		private bool firstTouch = false;
		private bool isMove = false;
		private bool isDoubleTap = false;
		public CustomContentPageRenderer ()
		{

			Touch += (object sender, Android.Views.View.TouchEventArgs e) => {
				if (DateTime.Now.Subtract (lastClick).TotalMilliseconds > 100){
					CustomContentPage page = ((CustomContentPage)this.Element);

					float realX = (float)((e.Event.GetX () * page.Width) / this.Width);
					float realY = (float)((e.Event.GetY () * page.Height) / this.Height);
					// Logger.Instance.LogDebug (this.ToString (), String.Format ("{0} - {1}", this.Width, gbl.Width));
					// Logger.Instance.LogDebug (this.ToString (), String.Format ("{0} - {1}", e.Event.GetX (), e.Event.GetY ()));
					// Logger.Instance.LogDebug (this.ToString (), String.Format ("{0} - {1}", realX, realY));

					switch (e.Event.Action & MotionEventActions.Mask) {
					case MotionEventActions.Down:
						{
							var circle = page.circles[0];
							bool found = false;
							foreach(var item in page.circles)
							{
								if (item.startX - item.sizeX <= realX && realX <= item.startX + item.sizeX
									&& item.startY - item.sizeY <= realY && realY <= item.startY + item.sizeY) {
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
							if(!found)
							{
								circle.active = false;
								App.AddCircile(realX - circle.sizeX, realY - circle.sizeY);
							}
							else
							{
								if(firstTouch && DateTime.Now.Subtract (lastClick).TotalMilliseconds < 400)
								{
									circle.placeholder.rgb = circle.placeholder.oldrgb;
									circle.placeholder.name = circle.placeholder.oldname;
									circle.nameLabel.IsVisible = !circle.nameLabel.IsVisible;
									circle.Update = !circle.Update;
									firstTouch = false;
									isMove = false;
									isDoubleTap = true;
								}
								else
								{
									circle.placeholder.oldrgb = circle.placeholder.rgb;
									circle.placeholder.oldname = circle.placeholder.name;
									//page.circle.startX = realX;
									//page.circle.startY = realY;
									//page.circle.Update = !page.circle.Update;
									firstTouch = true;
									isMove = false;
									isDoubleTap = false;
								}
							}
						}
						break;
					case MotionEventActions.Move:
						{
							isMove = true;
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
							if(found)
							{
								if (circle.startX - circle.sizeX <= realX && realX <= circle.startX + circle.sizeX
									&& circle.startY - circle.sizeY  <= realY && realY <= circle.startY + circle.sizeY) {
									circle.startX = realX;
									circle.startY = realY;
									circle.Update = !circle.Update;
								}
							}
						}
						break;

					case MotionEventActions.Up:
						{
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
							if(found)
							{
								if (circle.startX - circle.sizeX  <= realX && realX <= circle.startX + circle.sizeX
									&& circle.startY  - circle.sizeY  <= realY && realY <= circle.startY + circle.sizeY) {
									if(firstTouch && !isMove && !isDoubleTap)
									{
										circle.placeholder.oldrgb = circle.placeholder.rgb;
										circle.placeholder.oldname = circle.placeholder.name;
										circle.placeholder.GetColor();
										circle.Update = !circle.Update;
										isMove = false;
									}
								}

							}
							firstTouch = false;
							isDoubleTap = false;
						}

						break;

					default:
						break;
					}


					lastClick = DateTime.Now;
				}
			};


		}
	}
}

