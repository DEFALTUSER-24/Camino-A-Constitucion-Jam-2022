using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public abstract class ThrowingObject : MonoBehaviour
{
    [SerializeField]    protected           AudioClip       _throwableSound;
    [SerializeField]    protected           AudioClip       _impactSound;

    [SerializeField]    protected           PlayerHit       _player;
                        private             Vector3         _distance;

    private void Update()
    {
        if (_player == null)
            return;

        _distance = _player.transform.position - transform.position;
        transform.position += _distance.normalized * FlyWeightPointer.TObject.speed * Time.deltaTime;

        if (_distance.magnitude < _player.GetViewRadius())
            Collision();
    }

    public void Spawn(PlayerHit player)
    {
        _player = player;
        AudioManager.Instance.PlaySound(_throwableSound);
    }

    protected virtual void Collision()
    {
        AudioManager.Instance.PlaySound(_impactSound);
        _player = null;
        Destroy(this.gameObject);
    }
}