using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeywordPanle : MonoBehaviour
{
    public Button tempButton;
    public Transform parentTrans;
    public List<string> keyWords;

    private UIConsole m_UIConsole;
    private List<Button> m_Items = new List<Button>(); // 关键词按钮列表
  
    private void Awake()
    {
        m_UIConsole = FindObjectOfType<UIConsole>();
    }

    void Start()
    {
        foreach (string keyWord in keyWords)
        {
            Button button = Instantiate(tempButton, parentTrans);
            button.name = keyWord;
            button.GetComponentInChildren<TMP_Text>().text = keyWord;
            button.onClick.AddListener(() => { m_UIConsole.SendUsrMessage2DeepSeek(keyWord); });
            button.gameObject.SetActive(true);
            m_Items.Add(button);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
