using System;

using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace Circles
{
	public class App : Application
	{
		public static CustomContentPage page = new CustomContentPage();
		public static AbsoluteLayout layout = new AbsoluteLayout ();
		public App ()
		{
			AddCircile (60, 60);

			page.Content = layout;
			page.BackgroundColor = Color.Black;

			MainPage = page;
		}

		public static void AddCircile(float x, float y)
		{
			// The root page of your application
			CircleViewModel circle = new CircleViewModel (0, 0);
			circle.active = true;

			RelativeLayout circleField = new RelativeLayout ();
			circleField.Children.Add (circle.placeholder,
				Constraint.RelativeToParent ((parent) => {
					return 0;
				}),
				Constraint.RelativeToParent ((parent) => {
					return 0;
				})
			);

			circleField.Children.Add (circle.nameLabel,
				Constraint.RelativeToParent ((parent) => {
					double offsetX = (circle.placeholder.Width - circle.nameLabel.Text.Length * 5) / 2;
					if(offsetX < 0)
					{
						offsetX = 2;
					}
					return offsetX;
				}),
				Constraint.RelativeToParent ((parent) => {
					return (circle.placeholder.Height - 16) / 2;
				})
			);
			circle.parent = circleField;
			layout.Children.Add (circleField, new Point(x,y));
			circle.offsetX = x;
			circle.offsetY = y;
			//if (Device.OS == TargetPlatform.iOS) {
			circle.startX = x + circle.sizeX;
			circle.startY = y + circle.sizeY;
			//}

			circle.PropertyChanged += delegate {
				CircleViewModel active = page.circles[0];
				foreach(CircleViewModel item in page.circles)
				{
					if(item.active)
					{
						active = item;
						break;
					}
				}

				active.placeholder.BackgroundColor = Color.FromRgb(active.placeholder.rgb.r, active.placeholder.rgb.g, active.placeholder.rgb.b);
				active.nameLabel.Text = active.placeholder.name;
				active.nameLabel.TextColor = Color.FromRgb(active.placeholder.rgb.r, active.placeholder.rgb.g, active.placeholder.rgb.b);
				active.parent.TranslateTo(active.startX - active.sizeX - active.offsetX, active.startY - active.sizeY - active.offsetY, 30, Easing.SpringIn);
			};

			circle.active = true;
			page.circles.Add (circle);
		}
		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

