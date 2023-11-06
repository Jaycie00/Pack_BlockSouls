//im just gonna throw all the scripting shit in here so i dont have a billion cs files

datablock ProjectileData(damageprojectile)
{
   directDamage        = 0;
   muzzleVelocity      = 0;
   velInheritFactor    = 0;
   armingDelay         = 0;
   lifetime            = 100;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;
   uiName = "";
};

package damagesystem
{
    function gameConnection::onDeath(%client,%source,%killer,%type,%location)
	{
        if(%client.minigame > 0)
            %client.rotdebuff();
        parent::onDeath(%client,%source,%killer,%type,%location);
    }
    function ProjectileData::damage(%this,%obj,%col,%fade,%pos,%normal)
    {
        if(%obj.sourceobject.maxdamage)
        {
            %p = new Projectile()
            {
                sourceObject = %obj.sourceobject;
                initialPosition = %col.gethackposition();
                initialVelocity = "0 0 0";
                sourceSlot = 0;
                datablock = damageprojectile;
                client = %obj.client;
            };
            %damage = getrandom(%obj.sourceobject.mindamage, %obj.sourceobject.maxdamage) - %col.armorclass;
            if(%damage < 0)
                %damage = 0;
            %col.damage(%p, %pos, %damage, %this.directdamagetype);
            %p.delete();
        }
        else
            parent::damage(%this,%obj,%col,%fade,%pos,%normal);
    }
};
activatepackage(damagesystem);

function aiplayer::enemyscale(%this, %dist, %armor, %min, %max)
{
    if(!%this.defaultarmorclass)
        %this.defaultarmorclass = %this.armorclass;
    if(!%this.defaultmindamage)
        %this.defaultmindamage = %this.mindamage;
    if(!%this.defaultmaxdamage)
        %this.defaultmaxdamage = %this.maxdamage;
    initContainerRadiusSearch(%this.position, %dist, $typemasks::playerobjecttype);
    while(isobject(%search=containersearchnext()))
    {
        if(%search.getclassname() $= "player")
            %totalpeople++;
    }
    %this.armorclass = %this.defaultarmorclass + mfloor(%totalpeople/%armor);
    %this.mindamage = %this.defaultmindamage + mfloor(%totalpeople/%min);
    %this.maxdamage = %this.defaultmaxdamage + mfloor(%totalpeople/%max);
}
registeroutputevent("Bot", "enemyScale", "int 0 999999999 0" TAB "int 0 10 0" TAB "int 0 10 0" TAB "int 0 10 0");

function aiplayer::setarmorclass(%this, %amount)
{
    %this.armorclass = %amount;
}
function aiplayer::setmindamage(%this, %amount)
{
    %this.mindamage = %amount;
}
function aiplayer::setmaxdamage(%this, %amount)
{
    %this.maxdamage = %amount;
}
function aiplayer::addarmorclass(%this, %amount)
{
    if(%this.defaultarmorclass)
        %this.defaultarmorclass += %amount;
    %this.armorclass += %amount;
}
function aiplayer::addmindamage(%this, %amount)
{
    if(%this.defaultmindamage)
        %this.defaultmindamage += %amount;
    %this.mindamage += %amount;
}
function aiplayer::addmaxdamage(%this, %amount)
{
    if(%this.defaultmaxdamage)
        %this.defaultmaxdamage += %amount;
    %this.maxdamage += %amount;
}
registeroutputevent("Bot", "setArmorClass", "int -999999999 999999999 0");
registeroutputevent("Bot", "setMaxDamage", "int -999999999 999999999 0");
registeroutputevent("Bot", "setMinDamage", "int -999999999 999999999 0");
registeroutputevent("Bot", "addArmorClass", "int -999999999 999999999 0");
registeroutputevent("Bot", "addMaxDamage", "int -999999999 999999999 0");
registeroutputevent("Bot", "addMinDamage", "int -999999999 999999999 0");

function player::setarmorclass(%this, %amount)
{
    %this.armorclass = %amount;
}
function player::setmindamage(%this, %amount)
{
    %this.mindamage = %amount;
}
function player::setmaxdamage(%this, %amount)
{
    %this.maxdamage = %amount;
}
function player::addarmorclass(%this, %amount)
{
    %this.armorclass += %amount;
}
function player::addmindamage(%this, %amount)
{
    %this.mindamage += %amount;
}
function player::addmaxdamage(%this, %amount)
{
    %this.maxdamage += %amount;
}
registeroutputevent("Player", "setArmorClass", "int -999999999 999999999 0");
registeroutputevent("Player", "setMaxDamage", "int -999999999 999999999 0");
registeroutputevent("Player", "setMinDamage", "int -999999999 999999999 0");
registeroutputevent("Player", "addArmorClass", "int -999999999 999999999 0");
registeroutputevent("Player", "addMaxDamage", "int -999999999 999999999 0");
registeroutputevent("Player", "addMinDamage", "int -999999999 999999999 0");

function gameconnection::rotDebuff(%this)
{
    if(!%this.oldmaxhealth)
        %this.oldmaxhealth = %this.player.getmaxhealth();
    if(%this.rotdebuff < 5)
        %this.rotdebuff++;
    %this.player.setmaxhealth(%this.oldmaxhealth / (0.025 * %this.rotdebuff));
}
function gameconnection::clearRotDebuff(%this)
{
    %this.rotdebuff = 0;
    %this.player.setmaxhealth(%this.oldmaxhealth);
    %this.oldmaxhealth = 0;
}

registeroutputevent("Player", "rotDebuff");
registeroutputevent("Player", "clearRotDebuff");

function GameConnection::updatesavefile(%client)
{
    %fw = new FileObject();
	%fw.openForWrite("config/server/testgame/" @ %client.getblid() @ "/slot" SPC %client.slotselected @ "/save.txt");
    %fw.writeLine(%client.player.getposition());
	%fw.writeLine(%client.armorclass);
    %fw.writeLine(%client.mindamage);
	%fw.writeLine(%client.maxdamage);
    %fw.close();
	%fw.delete();
}
function GameConnection::readsavefile(%client)
{
	%fw = new FileObject();
	%fw.openForRead("config/server/testgame/" @ %client.getblid() @ "/slot" SPC %client.slotselected @ "/save.txt");
    %client.position = (%fw.readLine());
	%client.armorclass = (%fw.readLine());
    %client.mindamage = (%fw.readLine());
    %client.maxdamage = (%fw.readLine());
	%fw.close();
	%fw.delete();
    %client.player.settransform(%client.position);
}
function gameconnection::setslotnumber(%client, %number)
{
    %client.slotselected = %number;
    %client.readsavefile();
}

registeroutputevent("GameConnection", "updateSaveFile");
registeroutputevent("GameConnection", "readSaveFile");
registeroutputevent("GameConnection", "setSlotNumber", "int 1 100 1");

package botvalue
{
    function armor::ondisabled(%this, %obj, %state)
    {
        if(%obj.getclassname() $= "aiplayer")
        {
            for(%i=0;%i<clientgroup.getcount();%i++)
            {
                %cl=clientgroup.getobject(%i);
                if(vectordist(%cl.player.position, %obj.position) < 10)
                    %totalpeople++;
            }
            for(%i=0;%i<clientgroup.getcount();%i++)
            {
                %cl=clientgroup.getobject(%i);
                if(vectordist(%cl.player.position, %obj.position) < 10)
                    %cl.score += mfloatlength(%obj.value/%totalpeople, 0);
            }
        }
        parent::ondisabled(%this, %obj, %state);
    }
};
activatepackage(botvalue);

function aiplayer::setvalue(%this, %amount)
{
    %this.value = %amount;
}
registeroutputevent("Bot", "setValue", "int -999999999 999999999 0");

//debuff stuff

package statuseffects
{
    function gameconnection::spawnplayer(%this)
    {
        parent::spawnplayer(%this);
        %this.player.statusschedule();
    }
};
activatepackage(statuseffects);

function player::statusschedule(%this)
{
    if(%this.acid)
    {

    }
    if(%this.bleeding)
    {
        
    }
    if(%this.burning)
    {
        
    }
    if(%this.chill)
    {
        
    }
    if(%this.crippled)
    {
        
    }
    if(%this.dazed)
    {
        
    }
    if(%this.immobilized)
    {
        
    }
    if(%this.poisoned)
    {
        
    }
    if(%this.stunned)
    {
        
    }
    %this.statusScheduler = %this.schedule(100, statusschedule);
}

function player::applyDebuff(%this, %number)
{
    if(%number == 0)
        %this.acid = 1;
    else if(%number == 1)
        %this.bleeding = 1;
    else if(%number == 2)
        %this.burning = 1;
    else if(%number == 3)
        %this.chill = 1;
    else if(%number == 4)
        %this.crippled = 1;
    else if(%number == 5)
        %this.dazed = 1;
    else if(%number == 6)
        %this.immobilized = 1;
    else if(%number == 7)
        %this.poisoned = 1;
    else if(%number == 8)
        %this.stunned = 1;
}
registeroutputevent("Player", "applyDebuff", "TAB acid 0 bleeding 1 burning 2 chill 3 cripple 4 daze 5 immobilize 6 poison 7 stun 8");

function player::removeDebuff(%this, %number)
{
    if(%number == 0)
        %this.acid = 0;
    else if(%number == 1)
        %this.bleeding = 0;
    else if(%number == 2)
        %this.burning = 0;
    else if(%number == 3)
        %this.chill = 0;
    else if(%number == 4)
        %this.crippled = 0;
    else if(%number == 5)
        %this.dazed = 0;
    else if(%number == 6)
        %this.immobilized = 0;
    else if(%number == 7)
        %this.poisoned = 0;
    else if(%number == 8)
        %this.stunned = 10
}
registeroutputevent("Player", "removeDebuff", "TAB acid 0 bleeding 1 burning 2 chill 3 cripple 4 daze 5 immobilize 6 poison 7 stun 8");