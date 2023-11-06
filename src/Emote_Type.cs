datablock ParticleData(TypeParticle)
{
   dragCoefficient      = 5.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;
   windCoefficient      = 0;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 0;
   useInvAlpha          = false;
   textureName          = "./EmoteType";
   sizes[0]      = 1.5;
   sizes[1]      = 1.5;
   sizes[2]      = 1.4;
   times[0]      = 0.0;
   times[1]      = 0.6;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(TypeEmitter)
{
   ejectionPeriodMS = 35;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   ejectionOffset   = 1.8;
   velocityVariance = 0.0;
   thetaMin         = 0;
   thetaMax         = 0;
   phiReferenceVel  = 0;
   phiVariance      = 0;
   overrideAdvance = false;
   lifeTimeMS = 100;
   particles = "TypeParticle";

   doFalloff = true;

   emitterNode = GenericEmitterNode; 
   pointEmitterNode = TenthEmitterNode;

   uiName = "Emote - Type";
};

datablock ExplosionData(TypeExplosion)
{
   lifeTimeMS = 2000;
   emitter[0] = TypeEmitter;
   soundProfile = "";
};

datablock ProjectileData(TypeProjectile)
{
   explosion           = TypeExplosion;

   armingDelay         = 0;
   lifetime            = 100;
   explodeOnDeath		= true;

   uiName = "Type Emote";
};