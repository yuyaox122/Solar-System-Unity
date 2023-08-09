using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System;

public static class SaveAndLoad
{
    
    public static void SaveName(string name)
    {
        if (!PlayerPrefs.HasKey("!SolarSystemNames!"))
        {
            PlayerPrefs.SetString("!SolarSystemNames!", name);
        }
        else {
            string SolarSystemNames = PlayerPrefs.GetString("!SolarSystemNames!");
            SolarSystemNames = SolarSystemNames + "/" + name;
            PlayerPrefs.SetString("!SolarSystemNames!", SolarSystemNames);
        }
    }

    public static string[] GetNames()
    {
        if (!PlayerPrefs.HasKey("!SolarSystemNames!"))
        {
            return new string[0];
        }
        Debug.Log(PlayerPrefs.GetString("!SolarSystemNames!"));
        string SolarSystemNames = PlayerPrefs.GetString("!SolarSystemNames!");
        string[] names = SolarSystemNames.Split("/");
        Debug.Log("PlayerPrefs Names: " + SolarSystemNames);
        return names;
    }
    public static void SaveSolarSystem(SolarSystem newSolarSystem)
    {
        var formatter = new BinaryFormatter();
        byte[] buf;
        using (MemoryStream stream = new MemoryStream())
        {
            formatter.Serialize(stream, newSolarSystem);
            buf = new byte[stream.Length];
            buf = stream.ToArray();
        }
        string binary = "";
        // Debug.Log(newSolarSystem.name);
        for (int i = 0; i < buf.Length; i++)
            binary += Convert.ToString(buf[i], 2).PadLeft(8, '0'); ;
        // Debug.Log(binary);
        SaveName(newSolarSystem.name);
        PlayerPrefs.SetString(newSolarSystem.name, binary);
    }

    public static SolarSystem LoadSolarSystem(string name)
    {
        string binary = PlayerPrefs.GetString(name);
        byte[] bytes = new byte[binary.Length / 8];
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] = Convert.ToByte(binary.Substring(8 * i, 8), 2);
        }

        var formatter = new BinaryFormatter();
        SolarSystem loadedSolarSystem;
        using (Stream stream = new MemoryStream(bytes))
        {
            loadedSolarSystem = (SolarSystem)formatter.Deserialize(stream);
        }
        PlayerPrefs.SetString("!ActiveSolarSystem!", name);
        return loadedSolarSystem;
    }

    public static void DestroySolarSystem(string name) {
        string SolarSystemNames = PlayerPrefs.GetString("!SolarSystemNames!");
        // for(int i = 0; i < SolarSystemNames.Length; i++)
        // {
        //     Debug.Log(SolarSystemNames[i]);
        // }
        string[] names = SolarSystemNames.Split('/');
        
        string[] newNames = new string[names.Length-1];
        int index = Array.IndexOf(names, name);
        int spot = 0;
        Debug.Log(index);

        for(int i = 0; i < names.Length; i++)
        {
            Debug.Log(names[i]);
        }
        for (int l = 0; l < names.Length; l++)
        {
            if (l != index){
                newNames[spot] = names[l];
                spot++;
                // Debug.Log(newNames[index]);
            names = newNames;
            }
        }
        PlayerPrefs.DeleteKey(name);
    
        if (names.Length != 0) {
            string finishedNames = string.Join("/", names);
            Debug.Log("Finished names: " + finishedNames);
            PlayerPrefs.SetString("!SolarSystemNames!", finishedNames);
        }
    }
}