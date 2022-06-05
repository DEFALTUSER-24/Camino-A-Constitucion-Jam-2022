using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Passenger : MonoBehaviour
{
    public FiniteStateMachine<PassengerState> _fsm = new FiniteStateMachine<PassengerState>();

    [SerializeField] private CandyType _iLikeThisCandy = CandyType.Nothing;

    [SerializeField] private GameObject _balloon;
    [SerializeField] private GameObject _mantekelImage;
    [SerializeField] private GameObject _alfajorImage;
    [SerializeField] private Thrower    _throw;

    private Animator _balloonAnimator;

    private void Start()
    {
        _balloonAnimator = _balloon.GetComponent<Animator>();

        _fsm.AddState(PassengerState.See, new SeeState(this));
        _fsm.AddState(PassengerState.IDontWantToBuy, new IDontBuyState(this));
        _fsm.AddState(PassengerState.IWantToBuy, new IBuyState(this));

        _fsm.ChangeState(PassengerState.See);
    }

    private void Update()
    {
        if (GameMode.Instance.GameTimer.IsZero() || GameMode.Instance.GamePaused)
            return;

        _fsm.CurrentState().OnUpdate();
    }

    #region MouseMethods
    private void OnMouseEnter()
    {
        CanvasManager.Instance.SetCursorHoveringState(true);
    }

    private void OnMouseOver()
    {
        if (GameMode.Instance.GameTimer.IsZero() || GameMode.Instance.GamePaused)
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
        Debug.Log("Quiero un " + type);
        _iLikeThisCandy = type;

        _balloonAnimator.SetTrigger("grow");

        if (MyCandy() == CandyType.Mantekel)
            _mantekelImage.SetActive(true);
        else
            _alfajorImage.SetActive(true);
    }

    public void ReciveACandy(CandyType type)
    {
        if(_iLikeThisCandy == type)
        {
            _throw.Throw(PlayerHit.instance, PassengerObjects.Money);
        }
        else if (_iLikeThisCandy != type)
        {
            _throw.Throw(PlayerHit.instance, PassengerObjects.Rocks);
        }

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, FlyWeightPointer.passenger.viewRadius);
    }
}