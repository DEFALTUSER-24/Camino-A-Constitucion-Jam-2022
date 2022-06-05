using UnityEngine;

public class Rock : ThrowingObject
{
    [SerializeField] [Range(0, 100)]    private         int         _moneyToRemove;
    [SerializeField] [Range(0, 60)]     private         int         _secondsToRemove;

    protected override void Collision()
    {
        _player.GotHit();
        GameMode.Instance.Stats.Money_Remove(_moneyToRemove);
        GameMode.Instance.GameTimer.ModifyTime(-_secondsToRemove);
        base.Collision();
    }
}