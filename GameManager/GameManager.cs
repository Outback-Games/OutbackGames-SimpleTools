/*
 * =======================================================
 * | Created by 'Outback Games' 12-NOV-2021 @ 17:09 ACST |
 * | Covered by the MIT License, Open Source Software.   |
 * | No warranties or guarantees provided.               |
 * =======================================================
 */
#if USING_OUTBACKGAMES_GMANAGER
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if USING_OUTBACKGAMES_VR
using OutbackGames.SimpleTools.VR;
#endif
namespace OutbackGames.SimpleTools.GameManagement
{
    public class GameManager : MonoBehaviour
    {

#if USING_OUTBACKGAMES_VR
        [Space(8f)]
		[Header("Spectator Camera Settings")]
		[SerializeField] bool useSpectatorCam = false;
        [SerializeField] Camera spectatorCamera;
        [SerializeField, Tooltip("The Layers for the Spectator Cam to Render")] LayerMask specCamCullLayers;
        [SerializeField, Tooltip("This is used to determine the 'depth' aka 'render order' of the Spectator Cam.")] int specCamRenderOrder;
        [SerializeField, Tooltip("This is used to render the camera on a specific display.")] SpecCamMonitor specCamMonitor;
#endif

        void Start()
        {



#if USING_OUTBACKGAMES_VR
            InitializeVROptions();
#endif
        }


        private void OnDisable()
        {
            DeInitializeSpectatorCam();
        }


#if USING_OUTBACKGAMES_VR
        /// <summary>
        /// Initializes The VR Spectator Camera Manager Singleton.
        /// </summary>
        void InitializeVROptions()
        {
            if(useSpectatorCam) SpectatorCameraManager.InitializeSpectatorCam(spectatorCamera, specCamCullLayers, specCamMonitor, specCamRenderOrder);
        }

        /// <summary>
        /// Disables The VR Spectator Camera
        /// Will do nothing if the spectator cam is not initialized or the spec cam is null.
        /// </summary>
        public void DisableVRSpectatorCam()
        {
            SpectatorCameraManager.DisableSpecCam();
        }

        /// <summary>
        /// Enables The VR Spectator Camera
        /// Will do nothing if the spectator cam is not initialized or the spec cam is null.
        /// </summary>
        public void EnableVRSpectatorCam()
        {
            SpectatorCameraManager.EnableSpecCam();
        }

        /// <summary>
        /// Cleans Up The Spectator Camera Manager Singleton.
        /// </summary>
        void DeInitializeSpectatorCam()
        {
            SpectatorCameraManager.DeInitializeSpectatorCam();
        }

#endif


    }
}

#endif
