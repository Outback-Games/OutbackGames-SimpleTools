/*
 * =======================================================
 * | Created by 'Outback Games' 12-NOV-2021 @ 17:23 ACST |
 * | Covered by the MIT License, Open Source Software.   |
 * | No warranties or guarantees provided.               |
 * =======================================================
 */
#if USING_OUTBACKGAMES_VR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutbackGames.SimpleTools.VR
{
    // A simple Target Tracker. Place this on The Object That Will Guide The Rotation and Position of The
    // VR Spectator i.e. the XR Rig.
    public class SpectatorCamTracker : MonoBehaviour
    {   
        [SerializeField, Range(0.1f, 10f)] float trackerSpeed = 3f;
        [SerializeField] GameObject spectatorCameraObject;
        [SerializeField, Tooltip("The amount of frames to wait before grabbing the Spectator Camera.\nHelps with diagnosing execution order problems.")] int framesToWait = 0;
        bool hasCameraObject = false;

        private void OnEnable()
        {
            SpectatorCameraManager.OnInitialized += SetSpectatorCameraObject;
        }

        private void OnDisable()
        {
            SpectatorCameraManager.OnInitialized -= SetSpectatorCameraObject;
        }

        private IEnumerator Start()
        {
            if (hasCameraObject) { yield break; }
            int frameCounter = 0;
            while(frameCounter < framesToWait)
            {
                yield return null;
                frameCounter++;
            }
            SetSpectatorCameraObject();
        }

        public void SetSpectatorCameraObject()
        {
            spectatorCameraObject = SpectatorCameraManager.SpectatorCamera.gameObject;
            if (spectatorCameraObject == null)
            {
                hasCameraObject = false;
            }
            else
            {
                hasCameraObject = true;
            }
#if UNITY_EDITOR
            Debug.LogFormat("Does The Spec Cam Tracker Have The Spectator Camera?: {0}", hasCameraObject);
#endif
        }

        private void LateUpdate()
        {
            if (!hasCameraObject)
            {
                SetSpectatorCameraObject();
            }

            spectatorCameraObject.transform.position = Vector3.Lerp(
                spectatorCameraObject.transform.position,
                transform.position,
                trackerSpeed * Time.deltaTime
                );

            //TODO: add optional offset vector for position
            //TODO: add slerp for quaternion rotation matching.
        }
    }

}
#endif
