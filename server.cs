//exec shit
exec("./scripts.cs");
exec("./sounds/sounds.cs");
exec("./src/chatbubble.cs");
exec("./src/healthsystem.cs");
exec("./src/Support_HealthDetection.cs");
exec("./src/Support_HealthSaver.cs");

//music
datablock AudioProfile(MusicCaves)
{
    filename = "./music/Caves.ogg";
    description = AudioMusicLooping3d;
    preload = true;
    uiName = "Caves";
};
datablock AudioProfile(MusicCharacterCreation)
{
    filename = "./music/Character_Creation.ogg";
    description = AudioMusicLooping3d;
    preload = true;
    uiName = "Character_Creation";
};
datablock AudioProfile(MusicFallenKnights)
{
    filename = "./music/Fallen_Knights.ogg";
    description = AudioMusicLooping3d;
    preload = true;
    uiName = "Fallen_Knights";
};
datablock AudioProfile(MusicInvader)
{
    filename = "./music/Invader.ogg";
    description = AudioMusicLooping3d;
    preload = true;
    uiName = "Invader";
};
datablock AudioProfile(MusicLimgrave)
{
    filename = "./music/Limgrave.ogg";
    description = AudioMusicLooping3d;
    preload = true;
    uiName = "Limgrave";
};
datablock AudioProfile(MusicOpening)
{
    filename = "./music/Opening.ogg";
    description = AudioMusicLooping3d;
    preload = true;
    uiName = "Opening";
};
datablock AudioProfile(MusicVolcanoManor)
{
    filename = "./music/Volcano_Manor.ogg";
    description = AudioMusicLooping3d;
    preload = true;
    uiName = "Volcano_Manor";
};
datablock AudioProfile(Tunnels)
{
    filename = "./music/Tunnels.ogg";
    description = AudioMusicLooping3d;
    preload = true;
    uiName = "Tunnels";
};