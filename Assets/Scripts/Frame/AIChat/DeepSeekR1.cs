using HuggingFace.API;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class DeepSeekR1 : MonoBehaviour
{
    private string apiKey = "{your hf apikey}";
    private string apiUrl = "https://huggingface.co/api/inference-proxy/together/v1/chat/completions";

    // ���ڴ洢�Ի���ʷ
    private List<Dictionary<string, string>> messages = new List<Dictionary<string, string>>();

    void Start()
    {
        // ��ʼ��ϵͳ��Ϣ
        messages.Add(new Dictionary<string, string> { { "role", "system" }, { "content", "You are a helpful assistant." } });
    }

    public IEnumerator OnSendMesss2DeepSeekR1(string mess, Action<string> callback)
    {
        string userMessage = mess;//userInputField.text;
        if (string.IsNullOrEmpty(userMessage)) yield return null;

        // ����û���Ϣ���Ի���ʷ
        messages.Add(new Dictionary<string, string> { { "role", "user" }, { "content", userMessage } });

        // ���� DeepSeek API
        StartCoroutine(CallDeepSeekAPI(callback));
    }

    private IEnumerator CallDeepSeekAPI(Action<string> callback)
    {
        // ������������
        var requestData = new
        {
            model = "deepseek-ai/DeepSeek-R1",
            messages = messages,
            stream = false
        };

        string jsonData = JsonConvert.SerializeObject(requestData);

        // ���� UnityWebRequest
        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);

        // ��������
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // ������Ӧ
            var response = JsonConvert.DeserializeObject<DeepSeekResponse>(request.downloadHandler.text);
            string botMessage = response.choices[0].message.content;
            callback(botMessage);

            // ��� AI ��Ϣ���Ի���ʷ
            messages.Add(new Dictionary<string, string> { { "role", "assistant" }, { "content", botMessage } });
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
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

}
