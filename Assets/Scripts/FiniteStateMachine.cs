using System.Collections.Generic;

public class FiniteStateMachine<Enum>
{
    private IState                      _currentState;
    private Dictionary<Enum, IState>    _allStates      = new Dictionary<Enum, IState>();

    public IState CurrentState()
    {
        return _currentState;
    }

    public void AddState(Enum name, IState action)
    {
        if (_allStates.ContainsKey(name)) return;
        else _allStates.Add(name, action);
    }

    public void ChangeState(Enum name)
    {
        if (!_allStates.ContainsKey(name))
            return;

        _currentState?.OnExit();            //el ? pregunta si es null, si no lo es, ejecuta el OnExit del anterior estado
        _currentState = _allStates[name];   //Cambia el anterior estado por el nuevo
        _currentState.OnEnter();            //Ejecuta el OnEnter del nuevo estado
    }
}