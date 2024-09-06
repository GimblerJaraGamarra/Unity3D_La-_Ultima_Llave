using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public static UIController instance;

    public GameObject finishGamePanel;

    public Text lifeText;
    public Text bulletText;
    public Text keyText;

    public Button resetBtn;

    public GameObject bloodPanel;

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

    public void ActiveBloodPanel(bool state)
    {
        bloodPanel.SetActive(state);
    }

    public void UpdateLifePlayer(int life)
    {
        lifeText.text = life.ToString();
    }

    public void UpdateAmountBullet(int bullet)
    {
        bulletText.text = bullet.ToString();
    }

    public void UpdateAmountKey(int amount)
    {
        keyText.text = amount.ToString();
    }

    public void ActivePanelFinishGame()
    {
        finishGamePanel.SetActive(true);
    }
}
