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

datablock ExplosionData(dbDazeExplosion)
{
   lifeTimeMS = 350;

   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "1.0 1.0 1.0";
   camShakeAmp = "1.0 2.0 1.0";
   camShakeDuration = 35;
   camShakeRadius = 0.1;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 0;

   damageRadius = 1;
   radiusDamage = 0;

   impulseRadius = 1;
   impulseForce = 10;
};

datablock ProjectileData(dbDazeProjectile)
{
    projectileShapename = "base/data/shapes/empty.dts";
    directDamage = 0;

    explosion = dbDazeExplosion;
    hasLight = false;

    lifetime = 100;
};

datablock ParticleData(dbBleedParticle)
{
	dragCoefficient      = 3;
	gravityCoefficient   = 1;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 35;
	lifetimeVarianceMS   = 15;
	textureName          = "base/data/particles/dot";
	spinSpeed		= 10.0;
	spinRandomMin		= -500.0;
	spinRandomMax		= 500.0;
	colors[0]     = "0.7 0.1 0.2 0.9";
	colors[1]     = "0.9 0.0 0.0 0.0";
	sizes[0]      = 1;
	sizes[1]      = 0.5;

	useInvAlpha = false;
};
datablock ParticleEmitterData(dbBleedEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 1.0;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "dbBleedParticle";

   uiName = "Debuff Bleed";
};


datablock ExplosionData(dbBleedExplosion)
{
   lifeTimeMS = 350;

   particleEmitter = dbBleedEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = false;
   camShakeFreq = "1.0 1.0 1.0";
   camShakeAmp = "1.0 2.0 1.0";
   camShakeDuration = 35;
   camShakeRadius = 0.1;

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius = 0;

   damageRadius = 1;
   radiusDamage = 0;
};

datablock ProjectileData(dbBleedProjectile)
{
    projectileShapename = "base/data/shapes/empty.dts";
    directDamage = 0;

    explosion = dbBleedExplosion;
    hasLight = false;

    lifetime = 100;
};

function player::statusschedule(%this)
{
    if(%this.acid)
    {
	%this.addHealth(-0.5);
    }
    if(%this.bleeding)
    {
        %this.addHealth(-0.5);
	%this.spawnExplosion(dbBleedProjectile, 1);
    }
    if(%this.burning)
    {
        %this.addHealth(-0.5);
    }
    if(%this.chill)
    {
	%this.addHealth(-0.5);
	if(!%this.wasChill)
	{
	    %data = %this.getDatablock();
	    %this.setMaxForwardSpeed(%data.MaxForwardSpeed - 2);
	    %this.setMaxSideSpeed(%data.MaxSideSpeed - 2);
	    %this.setMaxBackwardSpeed(%data.MaxBackwardSpeed - 1);
	    %this.setMaxCrouchForwardSpeed(%data.MaxForwardCrouchSpeed - 1);
	    %this.setMaxCrouchSideSpeed(%data.MaxSideCrouchSpeed - 1);
	    %this.setMaxCrouchBackwardSpeed(%data.MaxBackwardCrouchSpeed - 1);
	    %this.wasChill = 1;
	}
    }
    else if(%this.wasChill)
    {
	%data = %this.getDatablock();
	%this.setMaxForwardSpeed(%data.MaxForwardSpeed);
	%this.setMaxSideSpeed(%data.MaxSideSpeed);
	%this.setMaxBackwardSpeed(%data.MaxBackwardSpeed);
	%this.setMaxCrouchForwardSpeed(%data.MaxForwardCrouchSpeed);
	%this.setMaxCrouchSideSpeed(%data.MaxSideCrouchSpeed);
	%this.setMaxCrouchBackwardSpeed(%data.MaxBackwardCrouchSpeed);
	%this.wasChill = 0;
    }
    if(%this.crippled)
    {
	if(!%this.wasCrippled)
	{
	    %data = %this.getDatablock();
	    %this.setMaxForwardSpeed(%data.MaxForwardSpeed - 4);
	    %this.setMaxSideSpeed(%data.MaxSideSpeed - 4);
	    %this.setMaxBackwardSpeed(%data.MaxBackwardSpeed - 2);
	    %this.setMaxCrouchForwardSpeed(%data.MaxForwardCrouchSpeed - 2);
	    %this.setMaxCrouchSideSpeed(%data.MaxSideCrouchSpeed - 1);
	    %this.setMaxCrouchBackwardSpeed(%data.MaxBackwardCrouchSpeed - 1);
	    %this.wasCrippled = 1;
	}
    }
    else if(%this.wasCrippled)
    {
	%data = %this.getDatablock();
	%this.setMaxForwardSpeed(%data.MaxForwardSpeed);
	%this.setMaxSideSpeed(%data.MaxSideSpeed);
	%this.setMaxBackwardSpeed(%data.MaxBackwardSpeed);
	%this.setMaxCrouchForwardSpeed(%data.MaxForwardCrouchSpeed);
	%this.setMaxCrouchSideSpeed(%data.MaxSideCrouchSpeed);
	%this.setMaxCrouchBackwardSpeed(%data.MaxBackwardCrouchSpeed);
        %this.wasCrippled = 0;
    }
    if(%this.dazed)
    {
	%this.spawnExplosion(dbDazeProjectile, 0.5);
    }
    else if(%this.wasDazed)
    {
	%this.wasDazed = 0;
    }
    if(%this.immobilized)
    {
	if(!%this.wasImmobilized)
	{
	    %this.setMaxForwardSpeed(0);
	    %this.setMaxBackwardSpeed(0);
	    %this.setMaxSideSpeed(0);
	    %this.setMaxCrouchForwardSpeed(0);
	    %this.setMaxCrouchBackwardSpeed(0);
	    %this.setMaxCrouchSideSpeed(0);
	    %this.wasImmobilized = 1;
	}
    }
    else if(%this.wasImmobilized)
    {
	%data = %this.getDatablock();
	%this.setMaxForwardSpeed(%data.MaxForwardSpeed);
	%this.setMaxSideSpeed(%data.MaxSideSpeed);
	%this.setMaxBackwardSpeed(%data.MaxBackwardSpeed);
	%this.setMaxCrouchForwardSpeed(%data.MaxForwardCrouchSpeed);
	%this.setMaxCrouchSideSpeed(%data.MaxSideCrouchSpeed);
	%this.setMaxCrouchBackwardSpeed(%data.MaxBackwardCrouchSpeed);
        %this.wasImmobilized = 0;
    }
    if(%this.poisoned)
    {
        %this.addHealth(-0.5);
    }
    if(%this.stunned)
    {
	if(!%this.wasStunned)
	{
	    %this.setMaxForwardSpeed(0);
	    %this.setMaxBackwardSpeed(0);
	    %this.setMaxSideSpeed(0);
	    %this.setMaxCrouchForwardSpeed(0);
	    %this.setMaxCrouchBackwardSpeed(0);
	    %this.setMaxCrouchSideSpeed(0);

	    %client = %this.getControllingClient();
	    %client.setControlObject(%client.camera);
	    %client.camera.unmountImage (0);
	    %client.camera.setOrbitMode (%this, %this.getTransform(), 0, 8, 8);
	    %client.camera.mode = "SPIN";
	    %client.isSpying = 0;
            %this.wasStunned = 1;
	}
    } 
    else if(%this.wasStunned)
    {
	%data = %this.getDatablock();
	%this.setMaxForwardSpeed(%data.MaxForwardSpeed);
	%this.setMaxSideSpeed(%data.MaxSideSpeed);
	%this.setMaxBackwardSpeed(%data.MaxBackwardSpeed);
	%this.setMaxCrouchForwardSpeed(%data.MaxForwardCrouchSpeed);
	%this.setMaxCrouchSideSpeed(%data.MaxSideCrouchSpeed);
	%this.setMaxCrouchBackwardSpeed(%data.MaxBackwardCrouchSpeed);

	%client = %this.client;
	%client.setControlObject(%this);
	%client.camera.setControlObject(0);
	%client.camera.mode = "";
        %this.wasStunned = 0;
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
registeroutputevent("Player", "applyDebuff", "list acid 0 bleeding 1 burning 2 chill 3 cripple 4 daze 5 immobilize 6 poison 7 stun 8");

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
        %this.stunned = 10;
}
registeroutputevent("Player", "removeDebuff", "list acid 0 bleeding 1 burning 2 chill 3 cripple 4 daze 5 immobilize 6 poison 7 stun 8");