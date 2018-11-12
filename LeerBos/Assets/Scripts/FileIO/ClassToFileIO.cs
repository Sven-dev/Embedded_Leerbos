using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// This class is for reading and writing a class type to and from a file.
/// This can be used to save things from levels to Config classes as long as they are serializable.
/// Take note that only public non-static variables will be serialized.
/// The methods of the class are static so they can be used without having an instance of this class.
/// </summary>
public class ClassToFileIO
{
    static string _Root = Application.dataPath + Path.DirectorySeparatorChar;

    public static T Load<T>(string subFolder, string filename)
    {
        if (!Directory.Exists(_Root + subFolder))
        {
            Directory.CreateDirectory(_Root + subFolder);
        }
        if (File.Exists(_Root + subFolder + Path.DirectorySeparatorChar + filename))
        {
            try
            {
                using (Stream stream = File.OpenRead(_Root + subFolder + Path.DirectorySeparatorChar + filename))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (T)formatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        return default(T);
    }

    public static void Save<T>(string subFolder, string filename, T data)
    {
        if (!Directory.Exists(_Root + subFolder))
        {
            Directory.CreateDirectory(_Root + subFolder);
        }
        try
        {
            using (Stream stream = File.OpenWrite(_Root + subFolder + Path.DirectorySeparatorChar + filename))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, data);
            }
        }
        catch (Exception ex) {
            throw new Exception(ex.ToString());
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
        if (File.Exists(_Root + subFolder + Path.DirectorySeparatorChar + filename))
        {
            File.Delete(_Root + subFolder + Path.DirectorySeparatorChar + filename);
        }
    }

    /// <summary>
    /// Gets all the files in folder, filters out the .meta files unity makes for every file.
    /// </summary>
    /// <param name="subFolder">The folder to look foor files in.</param>
    /// <returns>List of all the files with the .meta files filtered out.</returns>
    static public string[] GetFilesInFolder(string subFolder)
    {
        string[] valueToReturn;
        DirectoryInfo dirInfo = new DirectoryInfo(_Root + subFolder);
        FileInfo[] infoOnFiles = dirInfo.GetFiles();
        infoOnFiles = cleanMetadataFromList(infoOnFiles);
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
    /// Cleans the metadata files from the list.
    /// Unity creates a .meta file for every other file.
    /// </summary>
    /// <param name="fileinfos">The fileinfos.</param>
    /// <returns>File info array with all the .meta files removed.</returns>
    static FileInfo[] cleanMetadataFromList(FileInfo[] fileinfos)
    {
        ArrayList valueToReturn = new ArrayList();
        foreach (FileInfo fi in fileinfos)
        {
            if (!fi.Name.Contains(".meta"))
            {
                valueToReturn.Add(fi);
            }
        }
        return valueToReturn.ToArray(typeof(FileInfo)) as FileInfo[];
    }
}
