<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Public.aspx.cs" Inherits="EventSockets.Kickstart.Examples.Public" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title></title>
	<link type="text/css" rel="stylesheet" href="/assets/css/style.css" />
	<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
	<script type="text/javascript" src="http://dev.client.eventsockets.com/v1.0/js/eventsockets.js"></script>
	<script type="text/javascript" src="/assets/js/public.js"></script>
	<script type="text/javascript">
	<!--
		$(document).ready(function () {
			var clusterKey = "<%=ConfigurationManager.AppSettings["ClusterKey"] %>";
			var applicationKey = "<%=ConfigurationManager.AppSettings["ApplicationKey"] %>";
			var secure = <%=(ConfigurationManager.AppSettings["Secure"].ToLower() == "true" ? "true":"false") %>;

			setupConnection(clusterKey,applicationKey,secure);
			setupButtons();
		});
	-->
	</script>
</head>
<body>
	<div id="demo">
		<h2>Public channel example</h2>
		<div id="log"></div>
		<input id="connect" type="button" value="Connect" />
		<input id="subscribe" type="button" value="Subscribe" />
		<input id="rest" type="button" value="Trigger events (REST)" />
		<input id="unsubscribe" type="button" value="Unsubscribe" />
		<input id="disconnect" type="button" value="Disconnect" />
	</div>
</body>
</html>
