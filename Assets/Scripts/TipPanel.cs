using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TipPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private TextMeshProUGUI contentText;
    [SerializeField]
    private TextMeshProUGUI cancelBtnText;
    [SerializeField]
    private Button cancelBtn;
    [SerializeField]
    private TextMeshProUGUI confirmBtnText;
    [SerializeField]
    private Button confirmBtn;
    [SerializeField]
    private Button closeBtn;
    // Start is called before the first frame update
    void Awake()
    {
        Hide();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ShowTip(string title, string content,
        UnityAction onConfirmClick, string confirmText = "确定",
        bool isShowCancel = true, UnityAction onCancleClick = null, string cancelText = "取消")
    {
        gameObject.SetActive(true);
        titleText.text = title;
        contentText.text = content;
        confirmBtnText.text = confirmText;
        cancelBtnText.text = cancelText;

        cancelBtn.gameObject.SetActive(isShowCancel);

        confirmBtn.onClick.RemoveAllListeners();
        confirmBtn.onClick.AddListener(onConfirmClick);

        cancelBtn.onClick.RemoveAllListeners();
        cancelBtn.onClick.AddListener(onCancleClick);

        closeBtn.onClick.RemoveAllListeners();
        closeBtn.onClick.AddListener(onCancleClick);
    }
}
