using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class UIConsole : MonoBehaviour
{
    public TMP_Text showText;
    private DeepSeekChat m_DsChat;
    private DeepSeekR1 _deepseekr1;

    private void Awake()
    {
        _deepseekr1 = (DeepSeekR1)FindObjectOfType(typeof(DeepSeekR1));
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

            StartCoroutine(_deepseekr1.OnSendMesss2DeepSeekR1(usrMessage, ShowMessage));
        }
    }
}
