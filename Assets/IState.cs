public interface IState
{
    void OnEnter();     //Lo primero que har� cuando entre al estado
    void OnUpdate();    //Lo que har� constatemente
    void OnExit();      //Lo �ltimo que har� antes de salir del estado
}