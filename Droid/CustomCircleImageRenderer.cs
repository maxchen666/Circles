using System;
using Xamarin.Forms;
using Circles;
using Circles.Droid;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;


[assembly: ExportRenderer(typeof(CustomCircleImage2), typeof(CustomCircleImageRenderer))]
namespace Circles.Droid
{
	public class CustomCircleImageRenderer: ImageRenderer
	{
		public CustomCircleImageRenderer ()
		{
		}

		protected override void OnElementChanged (ElementChangedEventArgs<Image> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement == null) {

				//if((int)Android.OS.Build.VERSION.SdkInt < 18)
					// SetLayerType (LayerType.Software, null);
			}
		}

		protected override bool DrawChild (Canvas canvas, global::Android.Views.View child, long drawingTime)
		{
			try{
				var radius = Math.Min (Width, Height) / 2;
				var strokeWidth = 10;
				radius -= strokeWidth / 2;


				Path path = new Path ();
				path.AddCircle (Width / 2, Height / 2, radius, Path.Direction.Ccw);
				canvas.Save ();
				canvas.ClipPath (path);

				CustomCircleImage circle = (CustomCircleImage) Element;
				var result = base.DrawChild (canvas, child, drawingTime);

				canvas.Restore ();

				path = new Path ();
				path.AddCircle (Width / 2, Height / 2, radius, Path.Direction.Ccw);

				var paint = new Paint ();
				paint.AntiAlias = true;
				paint.StrokeWidth = 5;
				paint.SetStyle (Paint.Style.Stroke);
				paint.Color = global::Android.Graphics.Color.Rgb(circle.rgb.r, circle.rgb.g, circle.rgb.b);
				// = global::Android.Graphics.Color.Pink;

				canvas.DrawPath (path, paint);

				paint.Dispose();
				path.Dispose();
				return result;
			}catch(Exception ex) {
			}

			return base.DrawChild (canvas, child, drawingTime);
		}
	}
}

