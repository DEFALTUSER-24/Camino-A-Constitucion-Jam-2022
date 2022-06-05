using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Animator))]
public class Passenger : MonoBehaviour
{
    public FiniteStateMachine<PassengerState> _fsm = new FiniteStateMachine<PassengerState>();

    [SerializeField]    private         CandyType       _iLikeThisCandy = CandyType.Nothing;

    [SerializeField]    private         GameObject      _mantekelImage;
    [SerializeField]    private         GameObject      _alfajorImage;
    [SerializeField]    private         Thrower         _throw;
    [SerializeField]    private         int             _candyMaxAmount;

    [Header("Balloon options")]
    [SerializeField]    private         GameObject      _balloon;
    [SerializeField]    private         bool            bAnimateToLeft;

    [Header("Candy timeout options")]
    [SerializeField]    private         int             _minTimeoutValue = 3;
    [SerializeField]    private         int             _maxTimeoutValue = 10;

                        private         Animator        _animator;
                        private         Animator        _balloonAnimator;
                        private         Coroutine       _candyTimeoutCoroutine;
                        private         SkinCall        _skin;
                        public          int             MaxCandyAmount { get { return _candyMaxAmount; } private set { _candyMaxAmount = value; } }
                        private         int             _candyAmountOnBag;

    //--------------------------------------------------------------------------------

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _balloonAnimator = _balloon.GetComponent<Animator>();
        _throw = GetComponent<Thrower>();
        _skin = GetComponent<SkinCall>();

        _fsm.AddState(PassengerState.See, new SeeState(this));
        _fsm.AddState(PassengerState.IDontWantToBuy, new IDontBuyState(this));
        _fsm.AddState(PassengerState.IWantToBuy, new IBuyState(this));

        _fsm.ChangeState(PassengerState.See);
    }

    private void Update()
    {
        if (GameMode.Instance.IsGameInactive())
            return;

        _fsm.CurrentState().OnUpdate();
    }

    //--------------------------------------------------------------------------------

    #region MouseMethods
    private void OnMouseEnter()
    {
        if (GameMode.Instance.IsGameInactive())
            return;

        CanvasManager.Instance.SetCursorHoveringState(true);
    }

    private void OnMouseOver()
    {
        if (GameMode.Instance.IsGameInactive())
            return;

        if (Input.GetMouseButtonDown(0))
            CandySpawner.instance.RequestACandy(this, CandyType.Alfajor);
        else if (Input.GetMouseButtonDown(1))
            CandySpawner.instance.RequestACandy(this, CandyType.Mantekel);
    }

    private void OnMouseExit()
    {
        CanvasManager.Instance.SetCursorHoveringState(false);
    }
    #endregion

    //--------------------------------------------------------------------------------

    public void RequestACandy(CandyType type)
    {
        _iLikeThisCandy = type;

        _balloonAnimator.SetTrigger( bAnimateToLeft ? "grow-left" : "grow" );

        if (MyCandy() == CandyType.Mantekel)
            _mantekelImage.SetActive(true);
        else
            _alfajorImage.SetActive(true);

        if (_candyTimeoutCoroutine != null)
            StopCoroutine(_candyTimeoutCoroutine);

        _candyTimeoutCoroutine = StartCoroutine(CandyTimeout());
    }

    public void ReciveACandy(CandyType type, int value)
    {
        if (MyCandy() != type || CandyOnBag() >= MaxCandyAmount)
        {
            _throw.Throw(PlayerHit.instance, PassengerObjects.Rocks);
            GameMode.Instance.Stats.MadClient_Add();
            _animator.SetTrigger("gotMad");
        }
        else if(MyCandy() == type)
        {
            _throw.Throw(PlayerHit.instance, PassengerObjects.Money);
            GameMode.Instance.Stats.HappyClient_Add();
            GameMode.Instance.Stats.Money_Add(value);
        }

        //Cantidad de golosinas dadas siempre tiene que sumar un valor, lo haya pedido o no.
        _candyAmountOnBag++;
        GameMode.Instance.Stats.Candy_Add();

        if (MyCandy() != CandyType.Nothing)
            HideCandyBalloons();

        if (_candyTimeoutCoroutine != null)
            StopCoroutine(_candyTimeoutCoroutine);

        _iLikeThisCandy = CandyType.Nothing;
    }

    IEnumerator CandyTimeout()
    {
        yield return new WaitForSeconds(Random.Range(_minTimeoutValue, _maxTimeoutValue));
        GameMode.Instance.Stats.MadClient_Add();
        _iLikeThisCandy = CandyType.Nothing;
        HideCandyBalloons();
        _animator.SetTrigger("gotMad");
    }

    //--------------------------------------------------------------------------------

    public CandyType MyCandy()
    {
        return _iLikeThisCandy;
    }

    public int CandyOnBag()
    {
        return _candyAmountOnBag;
    }

    public void ResetCandyOnBag()
    {
        _candyAmountOnBag = 0;
        if (MyCandy() != CandyType.Nothing)
        {
            HideCandyBalloons();
            _iLikeThisCandy = CandyType.Nothing;
        }

        _skin.ChangeSkin(PassagersSkinsManager.instance.GetMyASkin());
    }

    private void HideCandyBalloons()
    {
        _balloonAnimator.SetTrigger("hide");
        _mantekelImage.SetActive(false);
        _alfajorImage.SetActive(false);
    }

    //--------------------------------------------------------------------------------

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, FlyWeightPointer.passenger.viewRadius);
    }
}