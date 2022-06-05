using UnityEngine;

public class Money : ThrowingObject
{
    [SerializeField] [Range(0, 100)]    private         int         _moneyToAdd;
    [SerializeField] [Range(0, 60)]     private         int         _secondsToAdd;

    protected override void Collision()
    {
        GameMode.Instance.Stats.Money_Add(_moneyToAdd);
        GameMode.Instance.GameTimer.ModifyTime(_secondsToAdd);
        base.Collision();
    }
}