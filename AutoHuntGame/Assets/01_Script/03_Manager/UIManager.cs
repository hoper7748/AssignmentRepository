using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public static UIManager Instance {  get { return instance; } }

    [Header("Title")]
    public GameObject TitleUI;

    [Header("InGameUI")]
    public GameObject PlayerInfoUI;
    public GameObject SettingPanel;
    [Space(10f)]
    public TextMeshProUGUI TmpText;
    public Image LvGage;
    public Image HpGage;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(this.gameObject);
        TitleUI.SetActive(false);
        SettingPanel.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }

    public void LoadGameScene()
    {
        TitleUI.SetActive(false);
        SceneManager.LoadScene("GameScene");
        ShowPlayerInfoUI();
        GameManager.Instance.isGame = true;
    }

    public void StartBtn()
    {
        LoadGameScene();
    }

    public void SaveBtn()
    {
        LoadManager.Instance.SavePlayerInfo();
        OffSettingUI();
    }

    public void LoadBtn()
    {
        if(LoadManager.Instance.isPlayerData())
        {
            GameManager.Instance.GetLoad = true;

            LoadGameScene();
        }    
        else
        {
            Debug.Log("Load ºÒ°¡  ");
        }
    }
    
    public void GoTitle()
    {
        Time.timeScale = 1;
        if (PlayerInfoUI != null)
        {
            PlayerInfoUI.SetActive(false);
            Debug.Log("PlayerInfoUI Off");
        }
        if(SettingPanel != null)
        {
            SettingPanel.SetActive(false);
            Debug.Log("SettingPanel Off");
        }
        SceneManager.LoadScene("TitleScene");
        TitleUI.SetActive(true);
        GameManager.Instance.GetLoad = false;
    }

    public void ExitBtn()
    {
        Application.Quit();
    }

    public void OnSettingUI()
    {
        Time.timeScale = 0;
        SettingPanel.SetActive(true);
    }

    public void OffSettingUI()
    {
        Time.timeScale = 1;
        SettingPanel.SetActive(false);
    }

    //public void HidePlayerInfoUI()
    //{
    //    PlayerInfoUI.SetActive(false);
    //}

    public void ShowPlayerInfoUI()
    {
        PlayerInfoUI.SetActive(true);
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
