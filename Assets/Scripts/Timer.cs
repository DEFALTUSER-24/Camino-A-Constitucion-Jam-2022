using UnityEngine;

public class Timer
{
    public int Minutes { get; private set; }
    public int Seconds { get; private set; }

    private bool _bTimeIsBeingModified = false;

    public Timer(int minutes, int seconds)
    {
        Minutes = minutes;
        Seconds = seconds;
        CanvasManager.Instance.UpdateTimeLeft(Get());
    }

    public string Minus()
    {
        if (_bTimeIsBeingModified)
            return Get();

        Seconds--;
        if (Seconds < 0)
        {
            Minutes--;
            Seconds = 59;
        }

        if (Minutes < 0)
            Minutes = 0;

        return Get();
    }

    public string ModifyTime(int amountInSeconds)
    {
        _bTimeIsBeingModified = true;
        Seconds += amountInSeconds;

        if (amountInSeconds > 0)
        {
            if (Seconds < 60)
                return DisableIsBeingModified();

            Minutes++;
            Seconds -= 60;
        }
        else
        {
            if (Minutes <= 0 && Seconds <= 0)
            {
                Seconds = 0;
                Minutes = 0;
                GameMode.Instance.GameOver();
                return DisableIsBeingModified();
            }

            if (Seconds > 0)
                return DisableIsBeingModified();

            Minutes--;
            Seconds = 60 - Seconds * -1;
        }
        return DisableIsBeingModified();
    }

    private string DisableIsBeingModified()
    {
        _bTimeIsBeingModified = false;
        CanvasManager.Instance.UpdateTimeLeft(Get());
        return Get();
    }

    public string Get()
    {
        return Minutes.ToString("00") + ":" + Seconds.ToString("00");
    }

    public bool IsZero()
    {
        return Minutes <= 0 && Seconds <= 0;
    }
}
