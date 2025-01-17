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
