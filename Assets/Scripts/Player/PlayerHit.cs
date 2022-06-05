using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public static PlayerHit instance;

    [SerializeField] private    float       _viewRadius;
    private Animator _anim;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        _anim = GetComponent<Animator>();
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

    public void GotHit()
    {
        _anim.SetTrigger("gotDamaged");
    }
}