using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public static PlayerHit instance;

    [SerializeField] private    float       _viewRadius;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    public float GetViewRadius()
    {
        return _viewRadius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);
    }
}