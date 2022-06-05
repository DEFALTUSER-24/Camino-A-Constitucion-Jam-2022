using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    [HideInInspector]                   public      static      GameMode        Instance;
    [SerializeField]                    public                  Timer           GameTimer { get; private set; }

    [SerializeField] [Range(0, 10)]     private                 int             _levelMinutes;
    [SerializeField] [Range(0, 60)]     private                 int             _levelSeconds;
    [SerializeField]                    private                 int             _scoreMultiplier;
                                        private                 Coroutine       _timeCoroutine;
                                        public                  bool            GamePaused { get; private set; }
                                        public                  bool            ResettingPassengers { get; private set; }
                                        public                  GameStats       Stats { get; private set; }

    //-----------------------------------------------------------------------------

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Instance = this;
        Stats = new GameStats();
        GameTimer = new Timer(_levelMinutes, _levelSeconds);
        _timeCoroutine = StartCoroutine(TimerUpdate());
    }

    //-----------------------------------------------------------------------------

    public void ResetPassengers()
    {
        if (GameTimer.IsZero()) //Evitamos que reinicie en caso que el tiempo ya haya terminado, asi solo mostramos la pantalla de fin del juego.
            return;

        ResettingPassengers = true;
        FindObjectOfType<PlayerModifiers>().MoveToInitialPosition();

        Passenger[] passengers = FindObjectsOfType<Passenger>();
        
        foreach (Passenger p in passengers)
        {
            p.ResetCandyOnBag();
        }

        ResettingPassengers = false;
    }

    public void GameOver()
    {
        Stats.Score_Add(Stats.MoneyEarned * _scoreMultiplier);
        Stats.Score_Remove(Stats.MadClients * _scoreMultiplier);

        CanvasManager.Instance.ShowGameOverScreen();
    }

    //-----------------------------------------------------------------------------

    public bool IsGameInactive()
    {
        return GameTimer.IsZero() || GamePaused || ResettingPassengers;
    }

    //-----------------------------------------------------------------------------

    public void ResumeCountdown()
    {
        _timeCoroutine = StartCoroutine(TimerUpdate());
        GamePaused = false;
    }

    public void PauseCountdown()
    {
        StopCoroutine(_timeCoroutine);
        GamePaused = true;
    }

    IEnumerator TimerUpdate()
    {
        do
        {
            yield return new WaitForSeconds(1);
            GameTimer.Minus();
            CanvasManager.Instance.UpdateTimeLeft(GameTimer.Get());
        } while (!GameTimer.IsZero());

        GameOver();
    }

    //-----------------------------------------------------------------------------
}