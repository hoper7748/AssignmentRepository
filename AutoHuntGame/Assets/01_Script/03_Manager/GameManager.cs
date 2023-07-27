using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    public bool isGame = false;

    [SerializeField] private bool Load = false;

    public bool GetLoad
    {
        get { return Load; }
        set { Load = value; }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != null)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    public void OnLoad()
    {
        Load = true;
    }
}
