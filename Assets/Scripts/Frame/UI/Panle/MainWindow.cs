using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainWindow : MonoBehaviour
{
    //public Button sendButton;
    //public InputField usrInputField;
    // public TMP_Text meesageText;

    public Button clearButton;

    private DeepSeekChat m_DsChat;
    private UIConsole m_UIConsole;

    private void Awake()
    {
        m_UIConsole = FindObjectOfType<UIConsole>();
    }

    void Start()
    {
        clearButton.onClick.AddListener(m_UIConsole.ClearMessage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        

}
