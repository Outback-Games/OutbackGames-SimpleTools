/*
 * =======================================================
 * | Created by 'Outback Games' 28-OCT-2021 @ 17:42 ACST |
 * | Covered by the MIT License, Open Source Software.   |
 * | No warranties or guarantees provided.               |
 * =======================================================
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;


namespace OutbackGames.SimpleTools.SimpleSave
{
    /// <summary>
    /// This is an example of how to setup a serializable container for your data to be used with the SimpleSave tool.
    /// </summary>
    [System.Serializable()]
    public class SimpleSaveContainer : ISerializable
    {
        float health = 0f;
        float mana = 0f;
        float stamina = 0f;
        Vector3 worldPosition = Vector3.zero;

        public float HP { get => health; set => health = value; }
        public float MP { get => mana; set => mana = value; }
        public float STAM { get => stamina; set => stamina = value; }
        public Vector3 WorldPosition { get => worldPosition; set => worldPosition = value; }

        public SimpleSaveContainer()
        {
            health = 100f;
            mana = 100f;
            stamina = 100f;
            worldPosition = Vector3.zero;
        }

        public SimpleSaveContainer(float Health, float Mana, float Stamina, Vector3 WorldPos)
        {
            health = Health;
            mana = Mana;
            stamina = Stamina;
            worldPosition = WorldPos;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("health", health);
            info.AddValue("mana", mana);
            info.AddValue("stamina", stamina);
            info.AddValue("worldPosition", worldPosition);
        }

        public SimpleSaveContainer(SerializationInfo info, StreamingContext context)
        {
            health = (float)info.GetValue("health", typeof(float));
            mana = (float)info.GetValue("mana", typeof(float));
            stamina = (float)info.GetValue("stamina", typeof(float));
            worldPosition = (Vector3)info.GetValue("worldPosition", typeof(Vector3));
        }

    }
}

