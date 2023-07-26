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
    public Image HpGage;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
            instance = this;
        UpdateLvGage();
        GetExp();
    }
    public void UpdateLvGage()
    {
        TmpText.text = Player.Instance.GetLv.ToString();
    }

    public void GetExp()
    {
        LvGage.fillAmount = Player.Instance.GetEXP / Player.Instance.MaxEXP;
    }
    
    public void UpdateHpGage()
    {
        HpGage.fillAmount = ((float)Player.Instance.GetHP / (float)Player.Instance.HP);
    }

}
