README!

Be sure to download the
msc.rsp and csc.rsp if you are using the game manager or any VR related scripts!

if you already have msc.rsp and csc.rsp files in your project from third-parties
append the following lines to those files using a text editor:

## WARNING If You are only using the Game Manager, and not any VR tools, DO NOT ADD The -define:USING_OUTBACKGAMES_VR Define Symbol.

-define:USING_OUTBACKGAMES_VR
-define:USING_OUTBACKGAMES_GMANAGER

Also be sure to add the following symbols to your Unity Player Settings in the per-platform Scripting Define Symbols, just as an added backup,
as we all know how Unity can be at times.

## WARNING If You are only using the Game Manager, and not any VR tools, DO NOT ADD The USING_OUTBACKGAMES_VR Define Symbol.

USING_OUTBACKGAMES_VR
USING_OUTBACKGAMES_GMANAGER

The msc.rsp and csc.rsp files must be in the root of the Assets folder in your project.
So usually they will be at the bottom of your folder heirarchy if you already have them and if using the one column layout.

If you need help or wish to contribute to the tools project, visit the github repo:
https://github.com/Outback-Games/OutbackGames-SimpleTools