using System;
using Xamarin.Forms;
using System.ComponentModel;

namespace Circles
{
	public class CircleViewModel: INotifyPropertyChanged
	{
		public float startX;
		public float startY;
		public float sizeX;
		public float sizeY;
		public CustomCircleImage placeholder;
		public Label nameLabel;
		public RelativeLayout parent;
		public bool update;
		public bool active;
		public float offsetX;
		public float offsetY;

		public event PropertyChangedEventHandler PropertyChanged;

		public CircleViewModel (float x, float y)
		{
			startX = x;
			startY = y;
			sizeX = 120 / 2; 
			sizeY = 120 / 2;
			active = false;
			placeholder = new CustomCircleImage () {
				Source = FileImageSource.FromFile ("circle.png")
			};
			nameLabel = new Label () {
				Text = placeholder.name,
				TextColor = Color.FromRgb(placeholder.rgb.r, placeholder.rgb.g, placeholder.rgb.b),
				FontSize = 10,
				IsVisible = false
			};
			placeholder.BackgroundColor = Color.FromRgb(placeholder.rgb.r, placeholder.rgb.g, placeholder.rgb.b);
		}

		public bool Update{
			set
			{
				if (update != value)
				{
					update = value;

					if (PropertyChanged != null)
					{
						PropertyChanged(this, 
							new PropertyChangedEventArgs("update"));
					}
				}
			}
			get
			{
				return update;
			}
		}
			
	}
}

