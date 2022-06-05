using UnityEngine;

public class PlayerModifiers : MonoBehaviour
{
    public static PlayerModifiers instance;

    [SerializeField] [Range(0, 100)]    private     float           _candyRequestPercent;
    [SerializeField]                    private     float           _speedModifier;
    [SerializeField]                    private     Transform       _nextWagonPoint;
    [SerializeField]                    private     float           _currencyModifier;

    private Vector3 _initialPosition;
    private Vector3 _distanceToNextWagon;
    private bool _alreadyReachedPosition;
    private Animator _anim;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        _initialPosition = transform.position;
    }

    public float RequestPercent()
    {
        return _candyRequestPercent;
    }

    private void Update()
    {
        if (GameMode.Instance.IsGameInactive() || _alreadyReachedPosition)
            return;

        _distanceToNextWagon = _nextWagonPoint.position - transform.position;

        transform.position += _distanceToNextWagon.normalized * _speedModifier * Time.deltaTime;

        if (_distanceToNextWagon.magnitude < 0.5f)
        {
            _alreadyReachedPosition = true;
            CanvasManager.Instance.Fade();
        }
    }


    public void MoveToInitialPosition()
    {
        transform.position = _initialPosition;
        _alreadyReachedPosition = false;
    }
}