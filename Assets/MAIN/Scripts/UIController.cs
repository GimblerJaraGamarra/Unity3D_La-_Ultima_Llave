using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public static UIController instance;

    [Header("GAME PANEL")]
    public Text targetKeyText;
    public Text amountKeyText;

    public Text bulletText;

    public Image lifePlayerSlider;

    public GameObject finishGamePanel;
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

    public void UpdateLifePlayer(float life)
    {

        lifePlayerSlider.fillAmount = life;
    }

    public void UpdateAmountBullet(int bullet)
    {
        bulletText.text = bullet.ToString();
    }

    public void UpdateAmountKey(int amount)
    {
        amountKeyText.text = amount.ToString();
    }

    public void UpdateTargetAmountKey(int amount)
    {
        targetKeyText.text = amount.ToString();
    }

    public void ActivePanelFinishGame()
    {
        finishGamePanel.SetActive(true);
    }
}
