using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class DSReqData
{
    public string model = "";
    public List<Dictionary<string, string>> messages = new List<Dictionary<string, string>>() { };
    public bool stream = false;
}

public class B70Data
{
    public string prompt = "";
    public int max_token = 100;
    public float temperature = 0.7f;
    public float top_p = 1.0f;
    public int n = 1;
}

[System.Serializable]
public class DeepSeekResponse
{
    public Choice[] choices;
}

[System.Serializable]
public class Choice
{
    public Message message;
}

[System.Serializable]
public class Message
{
    public string content;
}
