#if USING_OUTBACKGAMES_VR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutbackGames.SimpleTools.VR
{
    /// <summary>
    /// The desired monitor to render your spectator camera on.
    /// </summary>
    public enum SpecCamMonitor { Display1, Display2, Display3, Display4, Display5, Display6, Display7, Display8 }

    //This is a very simple spectator camera setup for VR, modify to see fit.
    public static class SpectatorCameraManager
    {
        public static Camera spectatorCamera { get; private set; }
        public static bool isSpecCamInitialized { get; private set; } = false;

        /// <summary>
        /// Initializes the VR spectator camera settings, if you wish to have a VR spectator camera
        /// that is disabled by default or created via script.
        /// </summary>
        /// <param name="specCam"></param>
        /// <param name="cullingMask"></param>
        /// <param name="desiredMonitor"></param>
        /// <param name="renderOrder"></param>
        public static void InitializeSpectatorCam(Camera specCam, LayerMask cullingMask, SpecCamMonitor desiredMonitor, int renderOrder)
        {
            if (isSpecCamInitialized) { return; } //if already configured, return;
            spectatorCamera = specCam;
            if (spectatorCamera.enabled)
            {
                spectatorCamera.enabled = false; //turn off the camera while we set it up.
            }
            spectatorCamera.cullingMask = cullingMask;
            spectatorCamera.targetDisplay = (int)desiredMonitor;
            spectatorCamera.depth = renderOrder;
            spectatorCamera.enabled = true;
            isSpecCamInitialized = true;
            spectatorCamera.Render(); //process a render.
            //Ideally you want this depth, to be Camera.main.depth + 1 however, 
            //you may have other cameras rendering after camera main so this will allow you to customize your rendering
        }

        /// <summary>
        /// This clears the linked spectator cam
        /// </summary>
        
        // You should not have Singleton Cameras; they do not like being a singleton.
        // The exception is when you multi-load scenes.
        // If you are multi-loading scenes for cameras, for VR spectating then you already have a better solution, however, 
        // there may be instances where this script setup will be optimal for your
        // project.
        // Primarily, to use this, it's just quicker and easier for a game jam style project or if you just don't want to get too complicated.
        public static void DeInitializeSpectatorCam()
        {
            if (spectatorCamera != null) { spectatorCamera.enabled = false; return; }
            spectatorCamera = null;
            isSpecCamInitialized = false;
        }

        public static void EnableSpecCam()
        {
            if (!isSpecCamInitialized) { return; }
            if (spectatorCamera == null) { return; }
            spectatorCamera.enabled = true;
            spectatorCamera.Render(); //just force rendering to ensure it sets back up properly.
        }

        public static void DisableSpecCam()
        {
            if (!isSpecCamInitialized) { return; }
            if (spectatorCamera == null) { return; }
            spectatorCamera.enabled = false;
        }

    }
}
#endif

