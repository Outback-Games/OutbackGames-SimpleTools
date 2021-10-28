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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
namespace OutbackGames.SimpleTools.SimpleSave
{
    [DefaultExecutionOrder(-10)]
    //This attribute is so that the serialization manager is initialized before all your monobehaviours to ensure there's no execution order issues.
    //If you need to run the script earlier to facilitate 3rd Party plugins, lower the value in the brackets e.g (-100 is less than -10).
    //Zero (0) is default runtime and greater than 0 is later runtime.
    public static class SerializationManager
    {
        private static BinaryFormatter bFormatter = new BinaryFormatter();
        public static string saveDirectory { get; private set; } = string.Format("{0}/Save Data", Application.persistentDataPath);
        public static string defaultFileName { get; private set; } = "save.dat";

        /// <summary>
        /// Object and sub objects (data types within classes/structs) to Save must be Serializable, i.e. Dictionaries, by default are not Serializable!
        /// Each Object is saved to its own file (designed to only handle save/load and for you to create your own Serializable Classes/Objects).
        /// Will revisit in the future.
        /// Use this particular method if you have more than one class/object to save.
        /// Will overwrite everything already saved in the file specified.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">The File Name you wish to use, without any extensions like .xml, .save or .dat</param>
        /// <param name="saveObject">The Serializable Object You Wish To Save (Serialize)</param>
        /// <returns></returns>
        public static bool Save<T>(string fileName, T saveObject)
        {
            try
            {
                if (!CheckDirectory())
                {
                    Directory.CreateDirectory(saveDirectory);
                }

                string saveFilePath = string.Format("{0}/{1}.dat", saveDirectory, fileName);

                if (bFormatter == null)
                {
                    bFormatter = new BinaryFormatter();
                }

                if (CheckFile(fileName))
                {
                    File.Delete(saveFilePath);
                }

                //by using a 'using block' we'll simplify and secure the flush and close of the file stream. Ensures writing to file correctly.
                //however, may incur higher GC usage if many many files are created.
                using (FileStream saveFileStream = File.Create(saveFilePath))
                {
                    bFormatter.Serialize(saveFileStream, saveObject);
                }

                return true;
            }
            catch
            {
#if UNITY_EDITOR
                Debug.Log("Fail TO Save");
#endif
                return false;
            }
            
        }

        /// <summary>
        /// Saves in the default Filename, useful for singluar Serializable class/object designs.
        /// Will overwrite anything saved in the file if you use this for multiple classes/objects.
        /// -- Save.dat --
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="saveObject">The Serializable Object You Wish To Save (Serialize)</param>
        /// <returns></returns>
        public static bool Save<T>(T saveObject)
        {
            if (!CheckDirectory())
            {
                Directory.CreateDirectory(saveDirectory);
            }

            return Save(defaultFileName, saveObject);
        }

        /// <summary>
        /// Save as a plain text XML file for easy external editing.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">The File Name you wish to use, without any extensions like .xml, .save or .dat</param>
        /// <param name="saveObject">The Serializable Object You Wish To Save (Serialize)</param>
        /// <returns></returns>
        public static bool SaveAsXML<T>(string fileName, T saveObject)
        {
            try
            {
                if (!CheckDirectory())
                {
                    Directory.CreateDirectory(saveDirectory);
                }

                string saveFilePath = string.Format("{0}/{1}.xml", saveDirectory, fileName);

                if (CheckFile(fileName))
                {
                    File.Delete(saveFilePath);
                }

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

                using (TextWriter textWriter = new StreamWriter(saveFilePath))
                {
                    xmlSerializer.Serialize(textWriter, saveObject);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Loads data stored inside the specified filename in the save directory. See OutbackGames.SimpleTools.SimpleSave.SerializationManager.cs for details.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">The File Name you wish to use, without any extensions like .xml, .save or .dat</param>
        /// <param name="defaultValue">The Default value you wish to assign if the return is null. Reduces Null checks & increases data safety.</param>
        /// <returns></returns>
        public static T Load<T>(string fileName, T defaultValue)
        {
            if (!CheckFile(fileName))
            {
                return defaultValue;
            }

            string saveFilePath = string.Format("{0}/{1}.dat", saveDirectory, fileName);
            object storedData = null;
            using (FileStream loadedData = File.Open(saveFilePath, FileMode.Open))
            {
                try
                {
                    storedData = bFormatter.Deserialize(loadedData);
                }
                catch
                {
#if UNITY_EDITOR
                    Debug.Log("Fail TO Load");
#endif
                    return defaultValue;
                }
            }

            if(storedData != null)
            {
                return (T)storedData;
            }
            else
            {
                return defaultValue;
            }

        }

        /// <summary>
        /// Loads from the default save file. See OutbackGames.SimpleTools.SimpleSave.SerializationManager.cs for details.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defaultValue">The Default value you wish to assign if the return is null. Reduces Null checks & increases data safety.</param>
        /// <returns></returns>
        public static T Load<T>(T defaultValue)
        {
            if (!CheckFile(defaultFileName))
            {
                return defaultValue;
            }

            return Load(defaultFileName, defaultValue);
        }


        /// <summary>
        /// Loads from your specified XML file within the save directory. See OutbackGames.SimpleTools.SimpleSave.SerializationManager.cs for details.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">The File Name you wish to use, without any extensions like .xml, .save or .dat</param>
        /// <param name="defaultValue">The Default value you wish to assign if the return is null. Reduces Null checks & increases data safety.</param>
        /// <returns></returns>
        public static T LoadFromXML<T>(string fileName, T defaultValue)
        {
            string saveFilePath = string.Format("{0}/{1}.xml", saveDirectory, fileName);

            if (!CheckFile(fileName, true))
            {
                return defaultValue;
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            object storedData = null;

            using (FileStream loadedData = File.Open(saveFilePath, FileMode.Open))
            {
                try
                {
                    storedData = xmlSerializer.Deserialize(loadedData);
                }
                catch
                {
#if UNITY_EDITOR
                    Debug.Log("Fail TO Load");
#endif
                    return defaultValue;
                }
                
            }

            if (storedData != null)
            {
                return (T)storedData;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Stored Data is null");
#endif
                return defaultValue;
            }
            
        }

        /// <summary>
        /// Checks if a Directory Exists.
        /// </summary>
        /// <returns></returns>
        private static bool CheckDirectory()
        {
            if(string.IsNullOrEmpty(saveDirectory) || string.IsNullOrWhiteSpace(saveDirectory)) { saveDirectory = string.Format("{0}/Save Data", Application.persistentDataPath); }
            if(Directory.Exists(saveDirectory))
            {
                return true;
            }else if (!Directory.Exists(saveDirectory))
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// Checks if a File Exists within the saveDirectory.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static bool CheckFile(string fileName, bool isXML = false)
        {
            if (string.IsNullOrEmpty(saveDirectory) || string.IsNullOrWhiteSpace(saveDirectory)) { saveDirectory = string.Format("{0}/Save Data", Application.persistentDataPath); }
            if (!File.Exists(string.Format("{0}/{1}{2}", saveDirectory, fileName, (isXML == true ? ".xml" : ".dat"))))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


    }
}

