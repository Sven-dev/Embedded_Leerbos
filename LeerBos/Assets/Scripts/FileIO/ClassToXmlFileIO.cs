using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using System;

public class ClassToXmlFileIO {

    static string _Root = Application.dataPath + Path.DirectorySeparatorChar;

    public static T Load<T>(string subFolder, string filename) {
        if (!Directory.Exists(_Root + subFolder))
        {
            Directory.CreateDirectory(_Root + subFolder);
        }
        if (File.Exists(_Root + subFolder + Path.DirectorySeparatorChar + filename + ".xml"))
        {
            try
            {
                using (FileStream stream = new FileStream(_Root + subFolder + Path.DirectorySeparatorChar + filename + ".xml", FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        return default(T);
    }

    public static void Save<T>(string subFolder, string filename, T data) {
        if (!Directory.Exists(_Root + subFolder))
        {
            Directory.CreateDirectory(_Root + subFolder);
        }
        using (FileStream stream = new FileStream(_Root + subFolder + Path.DirectorySeparatorChar + filename + ".xml", FileMode.Create))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stream, data);
        }
    }

    public static void DeleteFile(string subFolder, string filename)
    {
        if (!Directory.Exists(_Root + subFolder))
        {
            Directory.CreateDirectory(_Root + subFolder);
        }
        if (File.Exists(_Root + subFolder + Path.DirectorySeparatorChar + filename + ".meta"))
        {
            File.Delete(_Root + subFolder + Path.DirectorySeparatorChar + filename + ".meta");
        }
        if (File.Exists(_Root + subFolder + Path.DirectorySeparatorChar + filename + ".xml"))
        {
            File.Delete(_Root + subFolder + Path.DirectorySeparatorChar + filename + ".xml");
        }
    }

    /// <summary>
    /// Gets all the xml files in folder, filters out the .meta files unity makes for every file.
    /// </summary>
    /// <param name="subFolder">The folder to look foor files in.</param>
    /// <returns>List of all the files with the .meta files filtered out.</returns>
    static public string[] GetFilesInFolder(string subFolder)
    {
        string[] valueToReturn;
        DirectoryInfo dirInfo = new DirectoryInfo(_Root + subFolder);
        FileInfo[] infoOnFiles = dirInfo.GetFiles();
        infoOnFiles = cleanNonXmlFromList(infoOnFiles);
        valueToReturn = new string[infoOnFiles.Length];
        int index = 0;
        foreach (FileInfo fi in infoOnFiles)
        {
            valueToReturn[index] = fi.Name;
            index++;
        }
        Array.Sort(valueToReturn);
        return valueToReturn;
    }

    /// <summary>
    /// Cleans all non XML files from the list.
    /// </summary>
    /// <param name="fileinfos">The fileinfos.</param>
    /// <returns>File info array with all the non XML files removed.</returns>
    static FileInfo[] cleanNonXmlFromList(FileInfo[] fileinfos)
    {
        ArrayList valueToReturn = new ArrayList();
        foreach (FileInfo fi in fileinfos)
        {
            if (fi.Name.Contains(".xml"))
            {
                valueToReturn.Add(fi);
            }
        }
        return valueToReturn.ToArray(typeof(FileInfo)) as FileInfo[];
    }
}
