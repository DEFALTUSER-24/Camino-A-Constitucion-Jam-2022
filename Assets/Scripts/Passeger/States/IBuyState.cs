using UnityEngine;

public class IBuyState : IState
{
    private Passenger   _agent;
    private bool        _alreadyRequest;

    public IBuyState(Passenger agent)
    {
        _agent = agent;
    }

    public void OnEnter()
    {
    }

    public void OnUpdate()
    {
        float value = Random.value * 100;

        if (!_alreadyRequest && _agent.CandyOnBag() < _agent.MaxCandyAmount)
        {
            if (value < 50) _agent.RequestACandy(CandyType.Alfajor);
            else _agent.RequestACandy(CandyType.Mantekel);

            _alreadyRequest = true;
        }

        if(_agent.MyCandy() == CandyType.Nothing) _agent._fsm.ChangeState(PassengerState.See);
    }

    public void OnExit()
    {
        _alreadyRequest = false;
    }
}