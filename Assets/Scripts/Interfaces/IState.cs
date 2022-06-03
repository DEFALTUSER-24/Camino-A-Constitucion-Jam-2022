public interface IState
{
    void OnEnter();     //Lo primero que hará cuando entre al estado
    void OnUpdate();    //Lo que hará constatemente
    void OnExit();      //Lo último que hará antes de salir del estado
}