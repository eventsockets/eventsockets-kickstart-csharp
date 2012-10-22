<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EventSockets.Kickstart._default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title></title>
	<link href="/assets/css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
	<h2>
		Setup instructions</h2>
	<ol>
		<li>Sign up for a free sandbox account <a href="http://www.eventsockets.com">http://www.eventsockets.com</a>
			or login to your account if you have already signed up before and locate the sandbox
			application.</li>
		<li>View the security credentials and take a note of the security credentials (applicationKey,signatureKey
			and clusterKey).</li>
		<li>Configure the Subscription EndPoint url to point to this web (eg. if this web project
			is accessible at http://www.mydomain.com the url should point to http://www.mydomain.com/endpoints/subscription.aspx
			and make sure that this url is accessible from the internet (otherwise you will
			not be able to authenticate any channel subscription requests)</li>
		<li>Modify Web.config (change the appsettings value with your own keys which you found
			in step 2).</li>
		<li>Build the project and browse the examples (best experienced with more than two browser
			instances).</li>
	</ol>
	<hr />
	<h2>
		Examples</h2>
	<ul>
		<li><a href="/examples/public.aspx">Public channel example</a></li>
		<li><a href="/examples/private.aspx">Private channel example</a></li>
		<li><a href="/examples/presence.aspx">Presence channel example</a></li>
	</ul>
	<br />
</body>
</html>
