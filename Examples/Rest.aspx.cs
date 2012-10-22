using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using EventSockets.API;
using EventSockets.API.Models;

namespace EventSockets.Kickstart.Examples
{
	public partial class Rest : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			// Get the channelPrefix
			var channelPrefix = Request.QueryString["channelPrefix"];

			// Make sure we got channelPrefix
			if(!String.IsNullOrEmpty(channelPrefix))
			{
				// Construct a message
				var message = new Message();

				// Add current timestamp to the message (which is mandatory when signing envelope)
				message.MessageData.Add("auth.unixtime", Convert.ToInt32((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds).ToString());

				// Construct a messageArg (to trigger a text event called onTextMessage)
				var messageArg1 = new MessageArg(channelPrefix, "channel", "client", "onTextMessage");
				messageArg1.EventData.Add("event.payload", "Hello World!");
				message.MessageArgs.Add(messageArg1);

				// Construct a second messageArg (to trigger a JSON event called onJsonMessage)
				var messageArg2 = new MessageArg(channelPrefix, "channel", "client", "onJsonMessage");
				messageArg2.EventData.Add("event.payload", EventSockets.API.Tools.Json.Stringify(new Model.DummyObject() { Message = "Hello World!" }));
				message.MessageArgs.Add(messageArg2);

				// Setup an ApplicationConfig using your own keys (defined in web.config)
				var applicationConfig = new ApplicationConfig(
					ConfigurationManager.AppSettings["ClusterKey"],
					ConfigurationManager.AppSettings["ApplicationKey"],
					ConfigurationManager.AppSettings["SignatureKey"],
					false
				);

				// Send message using the REST API
				EventSockets.API.Tools.Rest.Send(applicationConfig, message);
			}

		}
	}

	
}