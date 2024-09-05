using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public static UIController instance;

    public GameObject finishGamePanel;

    public Text lifeText;
    public Text bulletText;

    public Button resetBtn;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        resetBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
    }


    public void UpdateLifePlayer(int life)
    {
        lifeText.text = life.ToString();
    }

    public void UpdateBullet(int bullet)
    {
        bulletText.text = bullet.ToString();
    }

    public void ActivePanelFinishGame()
    {
        finishGamePanel.SetActive(true);
    }
}
