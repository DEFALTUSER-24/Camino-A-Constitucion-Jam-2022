using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [HideInInspector]           public      static      CanvasManager           Instance;

    [Header("Cursor options")]
    [SerializeField]            private                 Texture2D               _cursorNormal;
    [SerializeField]            private                 Texture2D               _cursorHover;
    [SerializeField]            private                 Vector2                 _cursorOffset;

    [Header("Fade options")]
    [SerializeField]            private                 GameObject              _fadePanel;

    [Header("HUD options")]
    [SerializeField]            private                 GameObject              _hudPanel;
    [SerializeField]            private                 Text                    _gameTime;
    [SerializeField]            private                 Text                    _money;
    [SerializeField]            private                 Text                    _clientsAttended;

    [Header("Pause menu")]
    [SerializeField]            private                 GameObject              _pausePanel;

    [Header("Game Over screen")]
    [SerializeField]            private                 GameObject              _gameOverPanel;
    [SerializeField]            private                 Text                    _candySold;
    [SerializeField]            private                 Text                    _moneyEarned;
    [SerializeField]            private                 Text                    _IVAPaid;
    [SerializeField]            private                 Text                    _finalAmountSold;
    [SerializeField]            private                 Text                    _madClients;
    [SerializeField]            private                 Text                    _happyClients;
    [SerializeField]            private                 Text                    _finalScore;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (Instance != null)
            Destroy(Instance);
        else
            Instance = this;
    }

    public void SetCursorHoveringState(bool bIsHovering)
    {
        //Cursor.SetCursor(bIsHovering ? _cursorHover : _cursorNormal, _cursorOffset, CursorMode.ForceSoftware);
    }

    public void SetCursorTexture(Texture2D _customTexture, Vector2 _offset)
    {
        //Cursor.SetCursor(_customTexture, _offset, CursorMode.ForceSoftware);
    }

    public void Fade()
    {
        _fadePanel.GetComponent<Animator>().SetTrigger("BeginFade");
    }

    //-----------------------------------------------------------------------------

    public void ResumeGame()
    {
        GameMode.Instance.ResumeCountdown();
        _hudPanel.SetActive(true);
        _pausePanel.SetActive(false);
    }

    public void PauseGame()
    {
        GameMode.Instance.PauseCountdown();
        _hudPanel.SetActive(false);
        _pausePanel.SetActive(true);
        Debug.Log("game should be paused");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    //-----------------------------------------------------------------------------

    public void UpdateTimeLeft(string timeLeft)
    {
        _gameTime.text = timeLeft;
    }

    public void UpdateMoneyAmount(int amount)
    {
        _money.text = "$" + amount.ToString();
    }

    public void UpdateClientsAttended(int amount)
    {
        _clientsAttended.text = amount.ToString();
    }

    //-----------------------------------------------------------------------------

    public void ShowGameOverScreen()
    {
        int money = GameMode.Instance.Stats.MoneyEarned;
        int iva = Mathf.CeilToInt(money * 0.21f);
        int final = money - iva;
        
        if (money <= 0)
        {
            money = 0;
            iva = 0;
            final = 0;
        }

        Debug.Log("game over");
        _hudPanel.SetActive(false);
        _pausePanel.SetActive(false);
        _gameOverPanel.SetActive(true);

        _candySold.text = GameMode.Instance.Stats.CandyDelivered.ToString();
        _moneyEarned.text = money.ToString();
        _IVAPaid.text = iva.ToString();
        _finalAmountSold.text = final.ToString();
        //Las facturas declaradas ante AFIP siempre van a ser 0, es un "chiste" porque trabaja en negro.

        _madClients.text = GameMode.Instance.Stats.MadClients.ToString();
        _happyClients.text = GameMode.Instance.Stats.HappyClients.ToString();
        _finalScore.text = GameMode.Instance.Stats.Score.ToString();
    }
}
