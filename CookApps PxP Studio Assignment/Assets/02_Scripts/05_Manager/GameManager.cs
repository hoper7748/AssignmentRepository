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
        public List<Character.Character> Playables;

        private float _curDeadCount = 0;
        private float _maxDeadCount = 0;

        [Space(10), Header("End Panel"), Space(5)]
        public GameObject EndGamePanel;
        public TextMeshProUGUI RestartText;

        [Space(10), Header("Shop Panel"), Space(5)]
        public GameObject ShopPanel;

        public TextMeshProUGUI GoldText;

        [Space(10) ,Header("Stage Panel"), Space(5)]

        public TextMeshProUGUI StageCountText;

        [SerializeField] private int StageCount = 1;
        public int GetStageCount
        {
            get
            {
                return StageCount;
            }
        }

        private int _holdingGold = 0;

        private void UpdateGoldText()
        {
            GoldText.text = _holdingGold.ToString();
        }

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
            ShopPanel.SetActive(false);
            UpdateGoldText();
            StageUIUpdate();
            _maxDeadCount = Playables.Count;
            _curDeadCount = 0;
            Time.timeScale = 1f;
        }

        public void StageUIUpdate()
        {
            StageCountText.text = StageCount.ToString();
        }

        public void GetResult()
        {
            for(int i = 0; i < Playables.Count; i++)
            {
                Playables[i].GetEXP(10);
            }
            // °ñµå È¹µæ. 
            GetGold(10);
        }

        public void DeadCheck()
        {
            _curDeadCount++;
            if(_maxDeadCount <= _curDeadCount)
            {
                Debug.Log("ÇÃ·¹ÀÌ¾î Àü¿ø »ç¸Á");
                Time.timeScale = 0;
                EndGamePanel.SetActive(true);
                StartCoroutine(RestartCoroutine());
            }
        }

        public void GetGold(int _value)
        {
            _holdingGold += _value;
            UpdateGoldText();
        }

        public bool UseGold(int _value)
        {
            if (_holdingGold > _value)
            {
                _holdingGold -= _value;
                UpdateGoldText();
                return true;
            }
            return false;
        }

        public void ActiveShopPanel(bool _value)
        {
            ShopPanel.SetActive(_value);
        }

        public void AliveCheck()
        {
            _curDeadCount--;
        }

        public void NextStage()
        {
            StageCount++;
            StageUIUpdate();

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