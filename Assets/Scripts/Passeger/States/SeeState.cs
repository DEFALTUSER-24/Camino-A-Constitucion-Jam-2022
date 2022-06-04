using UnityEngine;

public class SeeState : IState
{
    private     Passenger       _agent;
    private     float           _timer;

    public SeeState(Passenger agent)
    {
        _agent = agent;
    }

    public void OnEnter()
    {
        Debug.Log("Veo");
    }

    public void OnUpdate()
    {
        _timer += Time.deltaTime;

        if(_timer > FlyWeightPointer.passenger.seeTime)
        {
            float value = Random.value * 100;

            if (value < PlayerModifiers.instance.RequestPercent()) _agent._fsm.ChangeState(PassengerState.IWantToBuy);
            else _agent._fsm.ChangeState(PassengerState.IDontWantToBuy);
        }
    }

    public void OnExit()
    {
        _timer = 0;
    }
}