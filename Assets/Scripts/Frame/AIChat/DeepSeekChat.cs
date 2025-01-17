using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.Networking;
using System.Text;
using UnityEditor.PackageManager.Requests;
using System;

public class DeepSeekChat
{
    public string apiKey = "sk-9991b1a05cf246ac81bf94a82d63005d";

    // 多轮对话 URL
    private string apiUrl = "https://api.deepseek.com/chat/completions";
  
    private List<Dictionary<string, string>> messages;

    public DeepSeekChat() { messages = new List<Dictionary<string, string>>(); }

    public IEnumerator CallDeepSeekApi(string message, Action<string> callback)
    {
        Debug.Log("DeepSeek API Request Begin..");
        messages.Add(new Dictionary<string, string> { { "role", "user" }, { "content", message } });
        
        DSReqData dsrd = new DSReqData() 
        {
            model = "deepseek-chat",
            messages = messages,
            stream = false
        };

        string chatJson = JsonMapper.ToJson(dsrd);
        UnityWebRequest uwr = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(chatJson);
        uwr.uploadHandler = new UploadHandlerRaw(bodyRaw);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        uwr.SetRequestHeader("Authorization", "Bearer " + apiKey);

        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.Success)
        {
            // 解析响应
            var response = JsonMapper.ToObject<DeepSeekResponse>(uwr.downloadHandler.text);
            string botMessage = response.choices[0].message.content;

            // 显示响应
            string aiResponse = "DeepSeek: " + botMessage;
            callback(aiResponse);

            // 添加 AI 消息到对话历史
            messages.Add(new Dictionary<string, string> { { "role", "assistant" }, { "content", botMessage } });
        }
        else
        {
            Debug.LogError("Error: " + uwr.error);
        }
    }
}
