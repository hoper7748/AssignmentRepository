using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CookAppsPxPAssignment.Character;
using UnityEngine.SceneManagement;
using TMPro;

namespace CookAppsPxPAssignment.Manager
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;

        public static GameManager Instance
        {
            get
            {
                return instance;
            }
        }
        [SerializeField] private List<Character.Character> playables;

        private float _curDeadCount = 0;
        private float _maxDeadCount = 0;

        public GameObject EndGamePanel;
        public TextMeshProUGUI RestartText;

        // Start is called before the first frame update
        void Start()
        {
            if (instance != null)
            {
                Destroy(instance);
                return;
            }
            instance = this;

            EndGamePanel.SetActive(false);
            _maxDeadCount = playables.Count;
            _curDeadCount = 0;
        }

        public void DeadCheck()
        {
            _curDeadCount++;
            if(_maxDeadCount <= _curDeadCount)
            {
                Debug.Log("플레이어 전원 사망");
                Time.timeScale = 0;
                EndGamePanel.SetActive(true);
                StartCoroutine(RestartCoroutine());
            }
        }

        public void AliveCheck()
        {
            _curDeadCount--;
        }
        
        public void ReStartGame()
        {
            SceneManager.LoadScene(0);
        }

        IEnumerator RestartCoroutine()
        {
            for (int i = 5; i >= 0; --i)
            {
                RestartText.text = $"Restart {i.ToString()}s"; 
                yield return new WaitForSecondsRealtime(1f);
            }
            ReStartGame();
        }

        public void ExitGame()
        {
            Application.Quit();
        }
        

    }

}