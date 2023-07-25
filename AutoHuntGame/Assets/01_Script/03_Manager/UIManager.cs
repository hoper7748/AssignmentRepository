using TMPro;
using UnityEngine;
using UnityEngine.UI;   

public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public static UIManager Instance {  get { return instance; } }

    [Header("Lv Inspector")]
    public TextMeshProUGUI TmpText;
    public Image LvGage;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
            instance = this;
        UpdateLv();
        GetExp();
    }
    public void UpdateLv()
    {
        TmpText.text = Player.Instance.GetLv.ToString();
    }

    public void GetExp()
    {
        LvGage.fillAmount = Player.Instance.GetEXP / Player.Instance.MaxEXP;

    }

}
