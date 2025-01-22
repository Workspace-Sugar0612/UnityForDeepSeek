using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class UIConsole : MonoBehaviour
{
    public TMP_Text showText;
    private DeepSeekChat m_DsChat;

    private void Awake()
    {
        m_DsChat = new DeepSeekChat();
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ClearMessage()
    {
        showText.text = "";
    }

    private void ShowMessage(string message)
    {
        showText.text += '\n' + message + '\n';
    }

    public void SendUsrMessage2DeepSeek(string usrMessage)
    {
        if (!string.IsNullOrEmpty(usrMessage))
        {
            ShowMessage($"USER: {usrMessage}");

            StartCoroutine(m_DsChat.CallDeepSeekApi(usrMessage, ShowMessage));
        }
    }
}
