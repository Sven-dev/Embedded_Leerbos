using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;

public class TextToFileIO : MonoBehaviour {

    static string _Root = Application.dataPath + Path.DirectorySeparatorChar;
    static string _Extention = ".txt";

    public static string[] Load(string subfolder, string filename) {
        string[] valueToReturn = null;
        if(!Directory.Exists(_Root + subfolder))
        {
            Directory.CreateDirectory(_Root + subfolder);
        }
        if (File.Exists(_Root + subfolder + Path.DirectorySeparatorChar + filename + _Extention))
        {
            try
            {
                valueToReturn = File.ReadAllLines(_Root + subfolder + Path.DirectorySeparatorChar + filename + _Extention);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        return valueToReturn;
    }

    public static void Save(string subfolder, string filename, string[] data) {
        if(!Directory.Exists(_Root + subfolder))
        {
            Directory.CreateDirectory(_Root + subfolder);
        }
        try
        {
            File.WriteAllLines(_Root + subfolder + Path.DirectorySeparatorChar + filename + _Extention, data);
        }
        catch(Exception ex) {
            throw new Exception(ex.ToString());
        }
    }
}