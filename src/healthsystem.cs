$NewHealth::Enabled = 1;
$NewHealth::PreVersion = 6.1;

if($NewHealth::Version >= $NewHealth::PreVersion)
{
	warn("There is already a newer health version.");
	return;
}

$NewHealth::Version = 6.1;

if(isPackage("CustomHealth"))
	deactivatepackage("CustomHealth");

function serverCmdSuicide(%this)
{
	if(isObject(%pl = %this.player))
		if(%pl.getState() !$= "dead")
			%pl.kill();
}

//Credit to Port for the functions
//
package CustomHealth
{
	function Player::setHealth(%this, %health)
	{
		if(!isObject(%this))
			return false;

		if(!strLen(%health))
			return false;

		if(%this.getMaxHealth() == 0 || %this.getDatablock().isInvincible || %this.isInvincible)
			return true;

		if(%this.maxHealth <= 0)
		{
			Parent::setHealth(%this, %health);
			return true;
		}

		if(%health < 0)
		{
			%health = 0;
			%this.health = 0;
			%this.damage(%this, %this.getPosition(), %this.getDatablock().maxDamage * %this.getSize(), $DamageType::Default, "body", true);
			return true;
		}

		%health = mClampF(%health, 0, %this.getMaxHealth());

		%this.health = %health;
		%this.setDamageLevel(%this.getHealthLevel());

		return true;
	}

	function Player::AddHealth(%this, %health)
	{
		if(!isObject(%this))
			return false;

		if(%this.getMaxHealth() == 0 || %this.getDatablock().isInvincible || %this.isInvincible)
			return true;

		if(%this.maxHealth <= 0)
		{
			Parent::AddHealth(%this, %health);
			return true;
		}

		if(%this.health > 0)
		{
			if(%health < 0)
				%this.damage(%this, %this.getPosition(), mAbs(%health), $DamageType::Default, "body", false);
			else
				%this.setHealth(%this.getHealth() + %health);
		}

		if(%this.health <= 0)
		{
			%this.health = 0;
			%this.damage(%this, %this.getPosition(), %this.getMaxHealth() * %this.getSize(), %damageType, "body", true);
		}

		return true;
	}

	function AIPlayer::setHealth(%this,%health)
	{
		if(!isObject(%this))
			return false;

		if(!strLen(%health))
			return false;

		if(%this.getMaxHealth() == 0 || %this.getDatablock().isInvincible || %this.isInvincible)
			return true;

		if(%this.maxHealth <= 0)
		{
			Parent::setHealth(%this, %health);
			return true;
		}

		if(%health < 0)
		{
			%health = 0;
			%this.health = 0;
			%this.damage(%this, %this.getPosition(), %this.getDatablock().maxDamage * %this.getSize(), $DamageType::Default, "body", true);
			return true;
		}

		%health = mClampF(%health, 0, %this.getMaxHealth());

		%this.health = %health;
		%this.setDamageLevel(%this.getHealthLevel());

		return true;
	}

	function AIPlayer::AddHealth(%this,%health)
	{
		if(!isObject(%this))
			return false;

		if(%this.getMaxHealth() == 0 || %this.getDatablock().isInvincible || %this.isInvincible)
			return true;

		if(%this.maxHealth <= 0)
		{
			Parent::AddHealth(%this, %health);
			return true;
		}

		if(%this.health > 0)
		{
			if(%health < 0)
				%this.damage(%this, %this.getPosition(), mAbs(%health), $DamageType::Default, "body", false);
			else
				%this.setHealth(%this.getHealth() + %health);
		}

		if(%this.health <= 0)
		{
			%this.health = 0;
			%this.damage(%this, %this.getPosition(), %this.getMaxHealth() * %this.getSize(), %damageType, "body", true);
		}

		return true;
	}

	function Vehicle::setHealth(%this,%health)
	{
		if(!isObject(%this))
			return false;

		if(!strLen(%health))
			return false;

		if(%this.getMaxHealth() == 0 || %this.getDatablock().isInvincible || %this.isInvincible)
			return true;

		if(%this.maxHealth <= 0)
		{
			Parent::setHealth(%this, %health);
			return true;
		}

		if(%health < 0)
		{
			%health = 0;
			%this.health = 0;
			%this.damage(%this, %this.getPosition(), %this.getDatablock().maxDamage * %this.getSize(), $DamageType::Default, "body", true);
			return true;
		}

		%health = mClampF(%health, 0, %this.getMaxHealth());

		%this.health = %health;
		%this.setDamageLevel(%this.getHealthLevel());

		return true;
	}

	function Vehicle::AddHealth(%this,%health)
	{
		if(!isObject(%this))
			return false;

		if(%this.getMaxHealth() == 0 || %this.getDatablock().isInvincible || %this.isInvincible)
			return true;

		if(%this.maxHealth <= 0)
		{
			Parent::AddHealth(%this, %health);
			return true;
		}

		if(%this.health > 0)
		{
			if(%health < 0)
				%this.damage(%this, %this.getPosition(), mAbs(%health), $DamageType::Default, "body", false);
			else
				%this.setHealth(%this.getHealth() + %health);
		}

		if(%this.health <= 0)
		{
			%this.health = 0;
			%this.damage(%this, %this.getPosition(), %this.getMaxHealth() * %this.getSize(), %damageType, "body", true);
		}

		return true;
	}

	function WheeledVehicle::setHealth(%this,%health)
	{
		if(!isObject(%this))
			return false;

		if(!strLen(%health))
			return false;

		if(%this.getMaxHealth() == 0 || %this.getDatablock().isInvincible || %this.isInvincible)
			return true;

		if(%this.maxHealth <= 0)
		{
			Parent::setHealth(%this, %health);
			return true;
		}

		if(%health < 0)
		{
			%health = 0;
			%this.health = 0;
			%this.damage(%this, %this.getPosition(), %this.getDatablock().maxDamage * %this.getSize(), $DamageType::Default, "body", true);
			return true;
		}

		%health = mClampF(%health, 0, %this.getMaxHealth());

		%this.health = %health;
		%this.setDamageLevel(%this.getHealthLevel());

		return true;
	}

	function WheeledVehicle::AddHealth(%this,%health)
	{
		if(!isObject(%this))
			return false;

		if(%this.getMaxHealth() == 0 || %this.getDatablock().isInvincible || %this.isInvincible)
			return true;

		if(%this.maxHealth <= 0)
		{
			Parent::AddHealth(%this, %health);
			return true;
		}

		if(%this.health > 0)
		{
			if(%health < 0)
				%this.damage(%this, %this.getPosition(), mAbs(%health), $DamageType::Default, "body", false);
			else
				%this.setHealth(%this.getHealth() + %health);
		}

		if(%this.health <= 0)
		{
			%this.health = 0;
			%this.damage(%this, %this.getPosition(), %this.getMaxHealth() * %this.getSize(), %damageType, "body", true);
		}

		return true;
	}

	function FlyingWheeledVehicle::setHealth(%this,%health)
	{
		if(!isObject(%this))
			return false;

		if(!strLen(%health))
			return false;

		if(%this.getMaxHealth() == 0 || %this.getDatablock().isInvincible || %this.isInvincible)
			return true;

		if(%this.maxHealth <= 0)
		{
			Parent::setHealth(%this, %health);
			return true;
		}

		if(%health < 0)
		{
			%health = 0;
			%this.health = 0;
			%this.damage(%this, %this.getPosition(), %this.getDatablock().maxDamage * %this.getSize(), $DamageType::Default, "body", true);
			return true;
		}

		%health = mClampF(%health, 0, %this.getMaxHealth());

		%this.health = %health;
		%this.setDamageLevel(%this.getHealthLevel());

		return true;
	}

	function FlyingWheeledVehicle::AddHealth(%this, %health)
	{
		if(!isObject(%this))
			return false;

		if(%this.getMaxHealth() == 0 || %this.getDatablock().isInvincible || %this.isInvincible)
			return true;

		if(%this.maxHealth <= 0)
		{
			Parent::AddHealth(%this, %health);
			return true;
		}

		if(%this.health > 0)
		{
			if(%health < 0)
				%this.damage(%this, %this.getPosition(), mAbs(%health), $DamageType::Default, "body", false);
			else
				%this.setHealth(%this.getHealth() + %health);
		}

		if(%this.health <= 0)
		{
			%this.health = 0;
			%this.damage(%this, %this.getPosition(), %this.getMaxHealth() * %this.getSize(), %damageType, "body", true);
		}

		return true;
	}
	
	function ShapeBase::kill(%this, %damageType, %last)
	{
		if(!isObject(%this))
			return false;

		if(getSimTime() - %this.spawnTime < $Game::PlayerInvulnerabilityTime)
			return false;
		
		if(!strLen(%damageType))
			%damageType = $DamageType::Suicide;
		
		if(!isObject(%last))
			%last = %this;

		%this.health = 0;
		%this.damage(%last, %this.getPosition(), %this.getMaxHealth() * %this.getSize(), %damageType, "body", true);
	}
	
	function Armor::onNewDatablock(%data, %obj)
	{
		Parent::onNewDatablock(%data, %obj);
		if(%obj.maxHealth > 0)
			%obj.setDamageLevel(%obj.getHealthLevel());
	}

	function ShapeBase::applyImpulse(%this, %origin, %velocity)
	{
		if(%this.getMaxHealth() == 0 || %this.getDatablock().isInvincible || %this.isInvincible)
			return;

		Parent::applyImpulse(%this, %origin, %velocity);
	}

	function ShapeBase::damage(%this, %sourceObject, %position, %damage, %damageType, %damageLoc, %parent)
	{
		//This bypasses the invincibility
		if(%parent)
		{
			//MaxHP_Debug("Killing");
			Parent::damage(%this, %sourceObject, %position, %damage * %this.getSize(), %damageType, %damageLoc);
			return;
		}

		if(%this.getState() $= "dead")
		{
			Parent::damage(%this, %sourceObject, %position, %damage * %this.getSize(), %damageType, %damageLoc);
			return;
		}

		if(getSimTime() - %this.spawnTime < $Game::PlayerInvulnerabilityTime)
			return;

		if(isObject(%sourceObject))
		{
			switch$(%targetClass = %sourceObject.getClassName())
			{
				case "Player" or "AIPlayer":
					%targetObj = %sourceObject;
					%targetClient = %sourceObject.client;

				case "Projectile":
					%targetObj = %sourceObject.sourceObject;
					%targetClient = %sourceObject.client;

				case "GameConnection":
					%targetObj = %sourceObject.player;
					%targetClient = %sourceObject;
			}
		}

		if(!isObject(%targetObj))
		{
			%targetObj = %this;
			%targetClient = %this.client;
		}

		%this.lastKiller = %targetObj;
		%this.lastKillerClient = %targetClient;
		if(%this.getMaxHealth() == 0 || %this.getDatablock().isInvincible || %this.isInvincible)
			return; //They are invincible

		//If they don't have max health we will just assume they don't have custom health
		//MaxHP_Debug("Max health: \"" @ %this.maxHealth @ "\"");
		if(%this.maxHealth < 0 || %this.maxHealth $= "")
		{
			//MaxHP_Debug("No custom health detected, doing normal damage");
			return Parent::damage(%this, %sourceObject, %position, %damage, %damageType, %damageLoc, true);
		}

		%old = %this.health;
		%this.health -= %damage;
		%new = %this.health;
		%diff = %old - %new;
		%this.lastDamageType = %damageType;

		if(%diff >= 16)
			%img = %this.getDatablock().PainHighImage;
		else if(%diff >= 8 && %diff <= 15)
			%img = %this.getDatablock().PainMidImage;
		else
			%img = %this.getDatablock().PainLowImage;

		if(isObject(%img))
			%this.emote(%img, true);

		%this.health = mClampF(%this.health, 0, %this.maxHealth);
		%this.oldHealth = %this.health;

		if(%this.health <= 0)
		{
			//MaxHP_Debug("Custom health is at 0, killing");
			return Parent::damage(%this, %sourceObject, %position, (%this.getDatablock().maxDamage) * %this.getSize(), %damageType, %damageLoc, true);
		}
		else
			%this.setDamageLevel(%this.getHealthLevel());

		Parent::damage(%this, %sourceObject, %position, 0, %damageType, %damageLoc);
	}
};
activatePackage("CustomHealth");

//Gets the size of the object
function ShapeBase::getSize(%this)
{
	if(!isObject(%this))
		return -1;

	return getWord(%this.getScale(), 2);
}

function ShapeBase::addMaxHealth(%this, %maxHealth, %bool)
{
	if(!isObject(%this))
		return -1;

	if(!strLen(%maxHealth))
		return false;

	//If we are adding max health, make sure we have it first
	if(%this.maxHealth < 0 || %this.maxHealth $= "")
	{
		%this.maxHealth = %this.getDatablock().maxDamage;
		%this.health = %this.getDatablock().maxDamage - %this.getDamageLevel();
	}

	%this.maxHealth = mClampF(%this.maxHealth + %maxHealth, 1, 999999);
	if(%bool)
		%this.health = %this.maxHealth;
	else
		%this.health += %maxHealth;

	%this.oldMaxHealth = %this.maxHealth;
	%this.oldHealth = %this.health;

	%this.setDamageLevel(%this.getHealthLevel());

	return true;
}

function ShapeBase::setMaxHealth(%this, %maxHealth)
{
	if(!isObject(%this))
		return -1;

	if(%maxHealth <= 0)
		return false;

	%this.maxHealth = mClampF(%maxHealth, 1, 999999);

	%this.health = %this.maxHealth;
	%this.oldMaxHealth = %this.maxHealth;
	%this.oldHealth = %this.health;
	%this.setDamageLevel(0);

	return true;
}

function ShapeBase::setInvulnerbilityTime(%this, %time)
{
	%this.setInvulnerbility(true);
	if(%time > 0)
		%this.vulWasteSch = %this.schedule(%time * 1000, setInvulnerbility, false);
}

function ShapeBase::setInvulnerbility(%this, %val)
{
	cancel(%this.vulWasteSch);
	%this.isInvincible = %val;
}

registerOutputEvent("player", "setMaxHealth" ,"int 1 999999 100");
registerOutputEvent("player", "addMaxHealth" ,"int -999999 999999 100" TAB "bool");
registerOutputEvent("player", "setInvulnerbilityTime" ,"int 1 300 5");

registerOutputEvent("vehicle", "setMaxHealth" ,"int 1 999999 100");
registerOutputEvent("vehicle", "addMaxHealth" ,"int -999999 999999 100" TAB "bool");
registerOutputEvent("vehicle", "setInvulnerbilityTime" ,"int 1 300 5");

registerOutputEvent("bot", "setMaxHealth" ,"int 1 999999 100");
registerOutputEvent("bot", "addMaxHealth" ,"int -999999 999999 100" TAB "bool");
registerOutputEvent("bot", "setInvulnerbilityTime" ,"int 1 300 5");

function MaxHP_Debug(%msg)
{
	if($NewHealth::Debug)
		talk("[NewHealthMod | Debug] " @ %msg);
}

//Only use this for mods that use and modify health.
//Support_HealthDetection.cs
function ShapeBase::getHealth(%this)
{
	if(!isObject(%this))
		return -1;

	if(%this.maxHealth > 0)
		return %this.health;

	return %this.getDatablock().maxDamage - %this.getDamageLevel();
}

function ShapeBase::getMaxHealth(%this)
{
	if(!isObject(%this))
		return -1;

	if(%this.maxHealth > 0)
		return %this.maxHealth;

	return %this.getDatablock().maxDamage;
}

function ShapeBase::getHealthLevel(%this) //This is used to set their damage level, which is opposite of their health, aka ShapeBase::getDamageLevel()
{
	%level = %this.getDatablock().maxDamage - (%this.getDatablock().maxDamage / %this.getMaxHealth() * %this.getHealth());
	//Make sure we don't break the scale
	%level = mClampF(%level, 0, %this.getDatablock().maxDamage);

	return %level;
}
//End of Support_HealthDetection.cs

//Overwrite the VCE variables
function MaxHealth_registerVariables()
{
	if(isPackage("VCE_Main"))
	{
		registerSpecialVar("Player", "health", "%this.getHealth()", "setHealth");
		registerSpecialVar("Player", "maxhealth", "%this.getMaxHealth()", "setMaxHealth");
		registerSpecialVar("Vehicle", "health", "%this.getHealth()", "setHealth");
		registerSpecialVar("Vehicle", "maxhealth", "%this.getMaxHealth()", "setMaxHealth");
		//Eventually for a new VCE
		//registerSpecialVar("Bot", "health", "%bot.getHealth()", "setHealth");
		//registerSpecialVar("Bot", "maxhealth", "%bot.getMaxHealth()", "setMaxHealth");
	}
}
schedule(5000, 0, "MaxHealth_registerVariables");