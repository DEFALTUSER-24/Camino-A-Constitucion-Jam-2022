using UnityEngine;

public class IDontBuyState : IState
{
    private     Passenger       _agent;
    private     float           _timer;

    public IDontBuyState(Passenger agent)
    {
        _agent = agent;
    }

    public void OnEnter()
    {
        Debug.Log("No voy a comprar");
    }

    public void OnUpdate()
    {
        _timer += Time.deltaTime;
        
        if(_timer > FlyWeightPointer.passenger.iDontBuyTime)
        {
            _agent._fsm.ChangeState(PassengerState.See);
        }
    }

    public void OnExit()
    {
        _timer = 0;
    }
}