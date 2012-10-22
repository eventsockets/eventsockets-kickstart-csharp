using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;
using EventSockets.API;
using EventSockets.API.Models;

namespace EventSockets.Kickstart.Endpoints
{
	public partial class Subscription : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			// Get application key
			var applicationKey = Request.QueryString["applicationKey"];

			// Get HTTP POST stream containing the JSON envelope request
			var httpPostStream = new System.IO.StreamReader(Request.InputStream);

			// Deserialize envelope from HTTP POST stream
			var requestEnvelope = Envelope.FromJson(httpPostStream.ReadToEnd());

			// Deserialize message from envelope
			var requestMessage = Message.FromJson(requestEnvelope.Message);

			// Prepare a message to be sent in response
			var responseMessage = new Message();

			// Add current timestamp to the message (which is mandatory when signing envelope)
			responseMessage.MessageData.Add("auth.unixtime", Convert.ToInt32((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds).ToString());


			// Iterate each subscription request
			foreach (var requestMessageArg in requestMessage.MessageArgs)
			{
				var socketId = requestMessageArg.EventData["socket.id"];
				var socketUUID = requestMessageArg.EventData["socket.uuid"];
				var channelPrefix = requestMessageArg.ChannelPrefix;
				var channelName = requestMessageArg.ChannelName;
				var eventPrefix = requestMessageArg.EventPrefix;
				var eventName = requestMessageArg.EventName;

				// Make sure that messageArg is a channel subscription request
				if (eventPrefix == "channel" && eventName == "subscribe")
				{
					// Construct a messageArg
					var responseMessageArg = new MessageArg(channelPrefix, channelName, eventPrefix, eventName);

					// Append socketId, no need to send socket.uuid)
					responseMessageArg.EventData.Add("socket.id", socketId);

					// In this example any subscription requests are allowed (set to false or ignore adding key/value to deny)
					responseMessageArg.EventData.Add("channel.subscription", "true");

					switch (channelPrefix.ToLower())
					{
						case "public":

							// Public channel subscriptions will never be sent out as channel subscription request do not need any authentication. 
							// If client subscribes to a public and private channel at the same time only the private channel request will be sent 
							// to the subscription endpoint

						case "private":

							// Allow subscriber to trigger events within the request channel (set to false or ignore adding key/value to deny)
							responseMessageArg.EventData.Add("channel.trigger", "true");

							break;

						case "presence":

							// Allow subscriber to trigger events within the requested channel (set to false or ignore adding key/value to deny)
							responseMessageArg.EventData.Add("channel.trigger", "true");

							// Describe the subscriber (socket.id is echoed in this example, in real life you might send a JSON object with username, email etc..)
							responseMessageArg.EventData.Add("socket.data", socketId);

							break;
					}

					// Add messageArg to response message
					responseMessage.MessageArgs.Add(responseMessageArg);

				}

			}

			// Setup an ApplicationConfig using your own keys (defined in web.config)
			var applicationConfig = new ApplicationConfig(
				ConfigurationManager.AppSettings["ClusterKey"],
				ConfigurationManager.AppSettings["ApplicationKey"],
				ConfigurationManager.AppSettings["SignatureKey"],
				false
			);

			// Write the signed JSON envelope to the output stream with correct status code and content-type
			Response.StatusCode = 202;
			Response.AddHeader("Content-Type", "application/json");
			Response.Write(responseMessage.Sign(applicationConfig).ToJson());
			Response.End();

		}

	}
}