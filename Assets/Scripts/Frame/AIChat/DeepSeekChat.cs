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
    public string apiKey = "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";

    // 多轮对话 URL
    private string apiUrl = "https://api.openrouter.ai/v1/models/deepseek-b70/completions";
  
    private List<Dictionary<string, string>> messages;

    public DeepSeekChat() { messages = new List<Dictionary<string, string>>(); }

    public IEnumerator CallDeepSeekApi(string message, Action<string> callback)
    {
        Debug.Log("DeepSeek API Request Begin..");
        //messages.Add(new Dictionary<string, string> { { "role", "user" }, { "content", message } });

        //DSReqData dsrd = new DSReqData() 
        //{
        //    model = "deepseek-chat",
        //    messages = messages,
        //    stream = false
        //};
        B70Data b70Data = new B70Data()
        {
            prompt = message,
            max_token = 100,
            temperature = 0.7f,
            top_p = 1.0f,
            n = 1
        };
        string chatJson = JsonMapper.ToJson(b70Data);
        UnityWebRequest uwr = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(chatJson);
        uwr.uploadHandler = new UploadHandlerRaw(bodyRaw);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        uwr.SetRequestHeader("Authorization", "Bearer " + apiKey);

        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Successed! response mess: " + uwr.downloadHandler.text);
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
