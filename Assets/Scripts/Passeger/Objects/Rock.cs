public class Rock : ThrowingObject
{
    protected override void Collision()
    {
        base.Collision();
        GameMode.Instance.Stats.Money_Remove(5);
    }
}