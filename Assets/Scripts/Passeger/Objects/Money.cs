public class Money : ThrowingObject
{
    protected override void Collision()
    {
        base.Collision();
        GameMode.Instance.Stats.Money_Add(10);
    }
}