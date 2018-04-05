using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;

public class SaveDataSystem
{
    //序列化对象
    private static string SerializedObject(object obj)
    {
        string serializedString = string.Empty;
        serializedString = JsonConvert.SerializeObject(obj);
        return serializedString;
    }

    //反序列化对象
    private static object DeserializedObject(string saveData, Type type)
    {
        object deserializedObj = null;
        deserializedObj = JsonConvert.DeserializeObject(saveData, type);
        return deserializedObj;
    }

    //判断文件是否存在
    private bool IsFileExists(string fileName)
    {
        return File.Exists(fileName);
    }

    //判断目录是否存在
    private bool IsDirectoryExists(string fileName)
    {
        return Directory.Exists(fileName);
    }

    //创建文件
    public void CreateFile(string fileName, string content)
    {
        if (IsFileExists(fileName))
        {
            return;
        }
        else
        {
            try
            {
                StreamWriter sw = File.CreateText(fileName);
                sw.Write(content);
                sw.Close();
            }
            catch(IOException e)
            {
                Debug.Log(e.ToString());
            }
        }
    }

    //创建目录
    public void CreateDirectionary(string directionName)
    {
        if (IsDirectoryExists(directionName))
        {
            return;
        }
        else
        {
            Directory.CreateDirectory(directionName);
        }
    }

    //储存数据
    public void SetData(string dataPath, object saveObj)
    {
        string setData = SerializedObject(saveObj);
        try
        {
            StreamWriter sw = File.CreateText(dataPath);
            sw.Write(setData);
            sw.Close();
        }
        catch (IOException e)
        {
            Debug.Log(e.ToString());
        }
    }

    //读取数据
    public object GetData(string fileName, Type type)
    {
        string readData = string.Empty;
        object dataObj = null;
        try
        {
            StreamReader sr = File.OpenText(fileName);
            readData = sr.ReadToEnd();
            dataObj = DeserializedObject(readData, type);
        }
        catch(IOException e)
        {
            Debug.Log(e.ToString());
        }
        return dataObj;
    }
}
