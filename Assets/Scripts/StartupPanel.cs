using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartupPanel : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private TextMeshProUGUI messageText;
    // Start is called before the first frame update
    void Start()
    {
        slider.gameObject.SetActive(false);
        SetMessage(string.Empty);
    }

    /// <summary>
    /// 设置进度条进度
    /// </summary>
    /// <param name="progress"></param>
    public void SetProgress(float progress)
    {
        slider.gameObject.SetActive(true);
        slider.value = progress;
    }
    /// <summary>
    /// 设置提示信息
    /// </summary>
    /// <param name="message"></param>
    public void SetMessage(string message)
    {
        messageText.text = message;
    }
}
