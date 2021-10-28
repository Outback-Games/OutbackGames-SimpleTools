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
using UnityEngine.UI;
using TMPro;
namespace OutbackGames.SimpleTools.SimpleSave
{
    /// <summary>
    /// This is just a quick example script showing the basic usage of the SimpleSave tool.
    /// </summary>
    public class SimpleSaveTester : MonoBehaviour
    {
        SimpleSaveContainer data = new SimpleSaveContainer(100f, 100f, 100f, Vector3.zero);

        [SerializeField] TMP_InputField hValue;
        [SerializeField] TMP_InputField mValue;
        [SerializeField] TMP_InputField sValue;
        [SerializeField] TMP_InputField wpXValue;
        [SerializeField] TMP_InputField wpYValue;
        [SerializeField] TMP_InputField wpZValue;

        public void SetHealth()
        {
            if (string.IsNullOrEmpty(hValue.text) || string.IsNullOrWhiteSpace(hValue.text)) { return; }
            float temp;
            float.TryParse(hValue.text.ToString(), out temp);
            data.HP = temp;
        }

        public void SetMana()
        {
            if (string.IsNullOrEmpty(mValue.text) || string.IsNullOrWhiteSpace(mValue.text)) { return; }
            float temp;
            float.TryParse(mValue.text.ToString(), out temp);
            data.MP = temp;
        }

        public void SetStamina()
        {
            if (string.IsNullOrEmpty(sValue.text) || string.IsNullOrWhiteSpace(sValue.text)) { return; }
            float temp;
            float.TryParse(sValue.text.ToString(), out temp);
            data.STAM = temp;
        }

        public void SetWorldPos()
        {
            if (string.IsNullOrEmpty(wpXValue.text) || string.IsNullOrWhiteSpace(wpXValue.text)) { return; }
            if (string.IsNullOrEmpty(wpYValue.text) || string.IsNullOrWhiteSpace(wpYValue.text)) { return; }
            if (string.IsNullOrEmpty(wpZValue.text) || string.IsNullOrWhiteSpace(wpZValue.text)) { return; }
            float tempX;
            float.TryParse(wpXValue.text.ToString(), out tempX);
            float tempY;
            float.TryParse(wpYValue.text.ToString(), out tempY);
            float tempZ;
            float.TryParse(wpZValue.text.ToString(), out tempZ);
            Vector3 newPos = new Vector3(tempX, tempY, tempZ);
#if UNITY_EDITOR
            Debug.Log($"New Position: {newPos}");
#endif
            data.WorldPosition = newPos;
#if UNITY_EDITOR
            Debug.Log($"Data's Position: {data.WorldPosition}");
#endif
        }

        public void SaveToFile(TMP_InputField inputFieldValue)
        {
            if (string.IsNullOrEmpty(inputFieldValue.text) || string.IsNullOrWhiteSpace(inputFieldValue.text)) { return; }
            SerializationManager.Save<SimpleSaveContainer>(inputFieldValue.text.ToString(), data);
        }

        public void SaveToXML(TMP_InputField inputFieldValue)
        {
            if (string.IsNullOrEmpty(inputFieldValue.text) || string.IsNullOrWhiteSpace(inputFieldValue.text)) { return; }
            SetHealth();
            SetMana();
            SetStamina();
            SetWorldPos();
            SerializationManager.SaveAsXML<SimpleSaveContainer>(inputFieldValue.text.ToString(), data);
        }

        public void LoadFromFile(TMP_InputField inputFieldValue)
        {
            if (string.IsNullOrEmpty(inputFieldValue.text) || string.IsNullOrWhiteSpace(inputFieldValue.text)) { return; }
            data = SerializationManager.Load<SimpleSaveContainer>(inputFieldValue.text.ToString(), new SimpleSaveContainer());
            hValue.text = data.HP.ToString();
            mValue.text = data.MP.ToString();
            sValue.text = data.STAM.ToString();
            wpXValue.text = data.WorldPosition.x.ToString();
            wpYValue.text = data.WorldPosition.y.ToString();
            wpZValue.text = data.WorldPosition.z.ToString();
        }

        public void LoadFromXML(TMP_InputField inputFieldValue)
        {
            if (string.IsNullOrEmpty(inputFieldValue.text) || string.IsNullOrWhiteSpace(inputFieldValue.text)) { return; }
            data = SerializationManager.LoadFromXML<SimpleSaveContainer>(inputFieldValue.text.ToString(), new SimpleSaveContainer());
            hValue.text = data.HP.ToString();
            mValue.text = data.MP.ToString();
            sValue.text = data.STAM.ToString();
            wpXValue.text = data.WorldPosition.x.ToString();
            wpYValue.text = data.WorldPosition.y.ToString();
            wpZValue.text = data.WorldPosition.z.ToString();
        }

    }

}
