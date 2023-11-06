exec("./Emote_Type.cs");

package TypeEmote
{
   function serverCmdStartTalking(%client)
   {
      parent::serverCmdStartTalking(%client);
      
      if(!isObject(%client.player))
      {
         return;
      }
      
      %player = %client.player;
      %player.emote(TypeProjectile, true);
   }
   
   function serverCmdMessageSent(%client, %message)
   {
      parent::serverCmdmessageSent(%client, %message);
      
      if(!isObject(%client.player))
      {
         return;
      }
      
      %player = %client.player;
      %player.emote(TypeProjectile, true);
   }
};
activatePackage(TypeEmote);
