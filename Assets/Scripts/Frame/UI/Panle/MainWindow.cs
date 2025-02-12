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

    public Button clearButton;

    private DeepSeekChat m_DsChat;
    private UIConsole _uiConsole;

    private void Awake()
    {
        _uiConsole = FindObjectOfType<UIConsole>();
    }

    void Start()
    {
        clearButton.onClick.AddListener(_uiConsole.ClearMessage);
        sendButton.onClick.AddListener(delegate { _uiConsole.SendUsrMessage2DeepSeek(usrInputField.text); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        

}
