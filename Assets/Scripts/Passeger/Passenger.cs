using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Passenger : MonoBehaviour
{
    public FiniteStateMachine<PassengerState> _fsm = new FiniteStateMachine<PassengerState>();

    [SerializeField] private CandyType _iLikeThisCandy = CandyType.Nothing;

    private void Start()
    {
        _fsm.AddState(PassengerState.See, new SeeState(this));
        _fsm.AddState(PassengerState.IDontWantToBuy, new IDontBuyState(this));
        _fsm.AddState(PassengerState.IWantToBuy, new IBuyState(this));

        _fsm.ChangeState(PassengerState.See);
    }

    private void Update()
    {
        _fsm.CurrentState().OnUpdate();
    }

    #region MouseMethods
    private void OnMouseEnter()
    {
        CameraManager.Cam.SetCursorHoveringState(true);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
            CandySpawner.instance.RequestACandy(this, CandyType.Alfajor);
        else if (Input.GetMouseButtonDown(1))
            CandySpawner.instance.RequestACandy(this, CandyType.Mantekel);
    }

    private void OnMouseExit()
    {
        CameraManager.Cam.SetCursorHoveringState(false);
    }
    #endregion

    public void RequestACandy(CandyType type)
    {
        Debug.Log("Quiero un " + type);
        _iLikeThisCandy = type;
    }

    public void ReciveACandy(CandyType type)
    {
        if(_iLikeThisCandy == type)
        {
            Debug.Log("Gracias!");
            _iLikeThisCandy = CandyType.Nothing;
        }
        else if (_iLikeThisCandy != type)
        {
            Debug.Log("No queria esto!");
            _iLikeThisCandy = CandyType.Nothing;
        }
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