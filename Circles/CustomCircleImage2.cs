using System;
using Xamarin.Forms;
using System.Net;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Threading;

namespace Circles
{
	public class CustomCircleImage2: Image
	{
		public class RGB 
		{
			public int r;
			public int g;
			public int b;
		}

		public RGB rgb;
		public string name;
		public  CustomCircleImage2()
		{
			string url = "http://www.colourlovers.com/api/colors/random";

			HttpWebResponse wrResp = null;
			HttpWebRequest wrReq = (HttpWebRequest)WebRequest.Create(url);
			wrReq.Method = "GET";
			wrReq.ContentType = "text/xml";
			Encoding encoding = Encoding.GetEncoding("utf-8");
			string responseMessage = "";

			try
			{
				// Execute on the url
				wrResp = (HttpWebResponse)GetResponse(wrReq);

				// Interpret the response
				Stream sr1 = wrResp.GetResponseStream();
				StreamReader srResponse = new StreamReader(sr1, encoding);
				responseMessage = srResponse.ReadToEnd();

				var doc = XDocument.Parse(responseMessage);
				rgb = doc.Root.Element("color").Elements("rgb")
					.Select(x => new RGB()
						{
							r = Convert.ToInt32(x.Element("red").Value),
							g = Convert.ToInt32(x.Element("green").Value),
							b = Convert.ToInt32(x.Element("blue").Value)
						}).FirstOrDefault();

				name = doc.Root.Element("color").Element("title").Value;
			}
			catch (WebException ex)
			{
				this.rgb.r = 0;
				this.rgb.g = 255;
				this.rgb.b = 255;

				name = "unknown";
			}
		}

		public WebResponse GetResponse(WebRequest request){
			ManualResetEvent evt = new ManualResetEvent (false);
			WebResponse response = null;
			request.BeginGetResponse ((IAsyncResult ar) => {
				response = request.EndGetResponse(ar);
				evt.Set();
			}, null);
			evt.WaitOne ();
			return response as WebResponse;
		}

		public Stream GetRequestStream(WebRequest request){
			ManualResetEvent evt = new ManualResetEvent (false);
			Stream requestStream = null;
			request.BeginGetRequestStream ((IAsyncResult ar) => {
				requestStream = request.EndGetRequestStream(ar);
				evt.Set();
			}, null);
			evt.WaitOne ();
			return requestStream;
		}
	}
}

