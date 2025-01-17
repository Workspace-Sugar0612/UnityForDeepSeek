using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainWindow : MonoBehaviour
{
    public Button sendButton;
    public InputField usrInputField;
    public TMP_Text meesageText;

    private DeepSeekChat m_DsChat;

    private void Awake()
    {
        m_DsChat = new DeepSeekChat();
    }

    void Start()
    {
        sendButton.onClick.AddListener(SendUsrMessage2DeepSeek);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowMessage(string message)
    {
        meesageText.text += '\n' + message + '\n';
    }

    private void SendUsrMessage2DeepSeek()
    {
        string usrMessage = usrInputField.text;
        usrInputField.text = "";
        if (!string.IsNullOrEmpty(usrMessage))
        {
            ShowMessage($"USER: {usrMessage}");
            
            StartCoroutine(m_DsChat.CallDeepSeekApi(usrMessage, ShowMessage));
        }
    }
}
