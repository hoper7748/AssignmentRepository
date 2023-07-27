using UnityEngine;

public class LoadManager : MonoBehaviour
{
    // n분마다 저장하기

    static LoadManager instance;
    public static LoadManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(this.gameObject);
    }

    

    public void SavePlayerInfo()
    {
        // 체력과 레벨을 저장
        PlayerPrefs.SetInt("Lv", Player.Instance.GetLv);
        PlayerPrefs.SetInt("Hp", Player.Instance.HP);
        PlayerPrefs.SetFloat("Exp", Player.Instance.GetEXP);
    }

    public bool isPlayerData()
    {
        try
        {
            if (PlayerPrefs.HasKey("Hp") && PlayerPrefs.HasKey("Lv") && PlayerPrefs.HasKey("Exp"))
            {
                GameManager.Instance.OnLoad();
                return true;
            }
        }
        catch
        {

        }
        return false;
            
    }

    public int LoadPlayerHp()
    {
        if (PlayerPrefs.HasKey("Hp"))
        {
            Debug.Log("A");
            return PlayerPrefs.GetInt("Hp");
        }
        return 0;
    }

    public int LoadPlayerLv()
    {
        if (PlayerPrefs.HasKey("Lv"))
            return PlayerPrefs.GetInt("Lv");
        return 0;
    }

    public float LoadPlayerEXP()
    {
        if (PlayerPrefs.HasKey("Exp"))
            return PlayerPrefs.GetFloat("Exp");
        return 0;
    }
}
