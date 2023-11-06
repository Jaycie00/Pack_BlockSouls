if(!strLen($Pref::Server::MaxHealth_SaveFile))
	$Pref::Server::MaxHealth_SaveFile = "config/server/MaxHealth.cs";

function serverCmdToggleHealthSaver(%this)
{
	if(!%this.isSuperAdmin)
		return;
	$Pref::Server::MaxHealth_Enabled = !$Pref::Server::MaxHealth_Enabled;
	if($Pref::Server::MaxHealth_Enabled)
	{
		registerOutputEvent("Player","saveHealth");
		registerOutputEvent("Player","loadHealth");	
	}
	else
	{
		unRegisterOutputEvent("Player","saveHealth");
		unRegisterOutputEvent("Player","loadHealth");
	}

	%this.chatMessage("Health saver is now " @ ($Pref::Server::MaxHealth_Enabled == false ? "disabled, removing the event from the event list." : "enabled, adding the event from the event list."));
	for(%i=0;%i<ClientGroup.getCount();%i++)
		serverCmdRequestEventTables(ClientGroup.getObject(%i)); //This might break someone if they are eventing :(
}

function Player::saveHealth(%this)
{
	if(!isObject(%client = %this.client))
		return;
	%path = $Pref::Server::MaxHealth_SaveFile;
	$MaxHealth_[%client.getBLID()] = %this.getHealth() SPC %this.getMaxHealth();
	export("$MaxHealth_*",%path);
}

function Player::loadHealth(%this)
{
	if(!isObject(%client = %this.client))
		return;
	%line = $MaxHealth_[%client.getBLID()];
	if(getWordCount(%line) == 2)
	{
		%this.setHealth(getWord(%line,0));
		%this.setMaxHealth(getWord(%line,1));
	}
}

if(!strLen($Pref::Server::MaxHealth_Enabled))
	$Pref::Server::MaxHealth_Enabled = true;

if(isFile($Pref::Server::MaxHealth_SaveFile))
	exec($Pref::Server::MaxHealth_SaveFile);
	
if($Pref::Server::MaxHealth_Enabled)
{
	registerOutputEvent("Player","saveHealth");
	registerOutputEvent("Player","loadHealth");
}