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

function ShapeBase::getHealthLevel(%this) //This is used to set their damage level, which is opposite of their health
{
	%level = %this.getDatablock().maxDamage - (%this.getDatablock().maxDamage / %this.getMaxHealth() * %this.getHealth());
	%level = mClampF(%level, 0, %this.getDatablock().maxDamage);
	return %level;
}