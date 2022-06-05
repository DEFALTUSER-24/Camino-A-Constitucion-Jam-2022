public class GameStats
{
    //-------------------------------------------------------

    public int Score { get; private set; }

    public int Score_Add(int amount)
    {
        Score += amount;
        return Score;
    }

    public int Score_Remove(int amount)
    {
        Score -= amount;
        return Score;
    }

    //-------------------------------------------------------

    public int CandyDelivered { get; private set; }

    public int Candy_Add()
    {
        CandyDelivered++;
        return CandyDelivered;
    }

    //-------------------------------------------------------

    public int MoneyEarned { get; private set; }

    public int Money_Add(int amount)
    {
        MoneyEarned += amount;
        return MoneyEarned;
    }

    public int Money_Remove(int amount)
    {
        MoneyEarned -= amount;
        return MoneyEarned;
    }

    //-------------------------------------------------------

    public int MadClients { get; private set; }

    public int MadClient_Add()
    {
        MadClients++;
        return MadClients;
    }

    //-------------------------------------------------------

    public int HappyClients { get; private set; }

    public int HappyClient_Add()
    {
        HappyClients++;
        return HappyClients;
    }
}
