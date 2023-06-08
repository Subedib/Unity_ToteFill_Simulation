using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;

public static class FileHandler
{
    public static void SaveListToJSON<T> (List<T> toSave, string fileName)
    {
        //Debug.Log(fileName);
        string content = JsonHelper.ToJson<T>(toSave.ToArray());
        WriteFile(GetPath(fileName),content);
    }
    public static void SaveToJSON<T>(T toSave, string fileName)
    {
        string content = JsonUtility.ToJson(toSave);
        WriteFile(GetPath(fileName), content);
    }

    public static List<T> ReadListFromJSON<T>(string fileName)
    {
        string content = ReadFile(GetPath(fileName));
        if (string.IsNullOrEmpty(content) || content == "{}")
        {
            return new List<T>();
        }

        List<T> res = JsonHelper.FromJson<T>(content).ToList();
        return res;

    }
    public static T ReadFromJSON<T>(string fileName)
    {
        string content = ReadFile(GetPath(fileName));
        if (string.IsNullOrEmpty(content) || content == "{}")
        {
            return default(T);
        }

        T res = JsonUtility.FromJson<T>(content);
        return res;

    }

    public static string GetPath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }

    private static void WriteFile(string path, string content)
    {
        FileStream fileStream = new FileStream(path, FileMode.Create);
        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(content);
        }
    }

    private static string ReadFile(string path)
    {
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string content = reader.ReadToEnd();
                return content;
            }
        }
        return "";
        
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.items;

        }
        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.items = array;
            return JsonUtility.ToJson(wrapper);

        }
        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.items = array;
            return JsonUtility.ToJson(wrapper,prettyPrint);

        }
    }


    [Serializable]
    private class Wrapper<T>
    {
        public T[] items;
    }
}
