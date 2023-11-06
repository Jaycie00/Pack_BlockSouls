$CustomCDN::clientVersion = 1;
//$CustomCDN::CDN_default
$CustomCDN::CDN_custom = "";

package CustomCDN
{
	function GameConnection::setConnectArgs(%this, %LANname, %blid, %clanPrefix, %clanSuffix, %clientNonce, %g, %h, %i, %j, %k, %l, %m, %n, %o, %p)
	{
		if($CustomCDN::CDN_default $= "")
			$CustomCDN::CDN_default = getCDNUrl();

		setCDNUrl($CustomCDN::CDN_default);

		warn("[CustomCDN] Set CDN URL to default: "@ getCDNUrl());

		%hNew = "CustomCDN" TAB $CustomCDN::clientVersion;
		%ret = Parent::setConnectArgs(%this, %LANname, %blid, %clanPrefix, %clanSuffix, %clientNonce, %g, %hNew NL %h, %i, %j, %k, %l, %m, %n, %o, %p);
		return %ret;
	}
};
activatePackage(CustomCDN);

function clientCmdSetCustomCDN(%cdn)
{
	setCDNUrl(%cdn);
	warn("[CustomCDN] Set CDN URL on instruction from server: "@ %cdn);
	$CustomCDN::CDN_custom = %cdn;

	commandToServer('ackCustomCDN');
}


