public class Timer
{
    public int Minutes { get; private set; }
    public int Seconds { get; private set; }

    public Timer(int minutes, int seconds)
    {
        Minutes = minutes;
        Seconds = seconds;
        CanvasManager.Instance.UpdateTimeLeft(Get());
    }

    public string Minus()
    {
        Seconds--;
        if (Seconds < 0)
        {
            Minutes--;
            Seconds = 59;
        }

        if (Minutes < 0)
            Minutes = 0;

        return Minutes.ToString("00") + ":" + Seconds.ToString("00");
    }

    public string Get()
    {
        return Minutes.ToString("00") + ":" + Seconds.ToString("00");
    }

    public bool IsZero()
    {
        return Minutes == 0 && Seconds == 0;
    }
}
