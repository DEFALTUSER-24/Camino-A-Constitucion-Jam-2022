using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Passenger : MonoBehaviour
{
    public FiniteStateMachine<PassengerState> _fsm = new FiniteStateMachine<PassengerState>();

    [SerializeField] private CandyType _iLikeThisCandy = CandyType.Nothing;

    [SerializeField] private GameObject _mantekelImage;
    [SerializeField] private GameObject _alfajorImage;
    [SerializeField] private Thrower _throw;
    [SerializeField] private int _candyMaxAmount;

    [Header("Balloon options")]
    [SerializeField] private GameObject _balloon;
    [SerializeField] private bool bAnimateToLeft;

    private Animator _balloonAnimator;
    public int MaxCandyAmount { get { return _candyMaxAmount; } private set { _candyMaxAmount = value; } }
    private int _candyAmountOnBag;

    private void Start()
    {
        _balloonAnimator = _balloon.GetComponent<Animator>();
        _throw = GetComponent<Thrower>();

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

    public void RequestACandy(CandyType type)
    {
        _iLikeThisCandy = type;

        _balloonAnimator.SetTrigger( bAnimateToLeft ? "grow-left" : "grow" );

        if (MyCandy() == CandyType.Mantekel)
            _mantekelImage.SetActive(true);
        else
            _alfajorImage.SetActive(true);
    }

    public void ReciveACandy(CandyType type)
    {
        /*
         * Los valores de las golosinas estan hardcodeados, hay que ponerles un valor después (estaría bueno determinarlo por dificultad o por inflación?).
         * Lo mismo con el tiempo que resta/suma.
         */

        if (MyCandy() != type || CandyOnBag() >= MaxCandyAmount)
        {
            _throw.Throw(PlayerHit.instance, PassengerObjects.Rocks);

            GameMode.Instance.Stats.MadClient_Add();
            //GameMode.Instance.Stats.Money_Remove(10);
            GameMode.Instance.GameTimer.ModifyTime(-15);
        }
        else if(MyCandy() == type)
        {
            GameMode.Instance.Stats.HappyClient_Add();
            GameMode.Instance.Stats.Money_Add(MyCandy() == CandyType.Alfajor ? 20 : 40);
            //GameMode.Instance.GameTimer.ModifyTime(15);
            _throw.Throw(PlayerHit.instance, PassengerObjects.Money);
        }

        //Cantidad de golosinas dadas siempre tiene que sumar un valor, lo haya pedido o no.
        _candyAmountOnBag++;
        GameMode.Instance.Stats.Candy_Add();

        if (MyCandy() != CandyType.Nothing)
        {
            _balloonAnimator.SetTrigger("hide");
            _mantekelImage.SetActive(false);
            _alfajorImage.SetActive(false);
        }

        _iLikeThisCandy = CandyType.Nothing;
    }

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
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, FlyWeightPointer.passenger.viewRadius);
    }
}