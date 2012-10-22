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
	public partial class Authentication : System.Web.UI.Page
	{
		// In this authentication example everyone gets authenticated by default, in production mode you would verify the 
		// user by a session cookie or any other method to identify the user to either accept or deny the connection.

		protected void Page_Load(object sender, EventArgs e)
		{
			// Fetch applicationKey and socketId
			var applicationKey = Request.QueryString["applicationKey"];
			var socketId = Request.QueryString["socketId"];

			// Construct a message for your authentication response
			var message = new Message();

			// Add current timestamp to the message (which is mandatory when signing envelope)
			message.MessageData.Add("auth.unixtime", Convert.ToInt32((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds).ToString());

			// Construct a messageArg
			var messageArg = new MessageArg("server", "channel", "connection", "authenticate");

			// Append socket.id
			messageArg.EventData.Add("socket.id", socketId);

			// Append socket.uuid 
			// In this example we echo the socket.id, normally you would pass your unique id for the current user, eg. primary key or username from your database
			messageArg.EventData.Add("socket.uuid", socketId);

			// Add messageArg to message
			message.MessageArgs.Add(messageArg);

			// Setup an ApplicationConfig using your own keys (defined in web.config)
			var applicationConfig = new ApplicationConfig(
				ConfigurationManager.AppSettings["ClusterKey"],
				ConfigurationManager.AppSettings["ApplicationKey"],
				ConfigurationManager.AppSettings["SignatureKey"],
				false
			);

			// To allow the request you need to send the correct http status code (any other code than 202 will automatically deny the request, even if the signed envelope is valid)
			Response.StatusCode = 202;

			// Write the signed JSON envelope to page output
			Response.Write(message.Sign(applicationConfig).ToJson());
		}
		
	}
}