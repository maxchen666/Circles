using System;
using Xamarin.Forms;
using Circles;
using Circles.iOS;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using CoreGraphics;


[assembly: ExportRenderer(typeof(CustomCircleImage2), typeof(CustomCircleImageRenderer))]
namespace Circles.iOS
{
	public class CustomCircleImageRenderer: ImageRenderer
	{
		public CustomCircleImageRenderer ()
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null || Element == null)
				return;
			try{
				CustomCircleImage circle = (CustomCircleImage) Element;
				double min = Math.Min(Element.Width, Element.Height);
				Control.Layer.CornerRadius = (float) (min/2.0);
				Control.Layer.MasksToBounds = false;
				Control.Layer.BorderColor = new CGColor(circle.rgb.r, circle.rgb.g, circle.rgb.b);
				Control.Layer.BorderWidth = 3;
				Control.ClipsToBounds = false;
			}catch(Exception ex){
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == VisualElement.HeightProperty.PropertyName ||
				e.PropertyName == VisualElement.WidthProperty.PropertyName)
			{
				try{
					CustomCircleImage circle = (CustomCircleImage) Element;
					double min = Math.Min(Element.Width, Element.Height);
					Control.Layer.CornerRadius = (float) (min/2.0);
					Control.Layer.MasksToBounds = false;
					Control.Layer.BorderColor = new CGColor(circle.rgb.r, circle.rgb.g, circle.rgb.b);
					Control.Layer.BorderWidth = 3;
					Control.ClipsToBounds = true;
				}
				catch(Exception ex){
				}
			}
		}
	}
}
