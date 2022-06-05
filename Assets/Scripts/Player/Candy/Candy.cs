using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class Candy : MonoBehaviour
{
    [SerializeField] private        Passenger           _target;
    [SerializeField] private        CandyType           _myType;

    private void Update()
    {
        Vector3 distance = _target.transform.position - transform.position;

        if (_target != null) transform.position += distance.normalized * FlyWeightPointer.TObject.speed * Time.deltaTime;

        if(distance.magnitude < FlyWeightPointer.passenger.viewRadius)
        {
            _target.ReciveACandy(_myType);
            Destroy(this.gameObject);
        }
    }

    public void Spawn(Passenger costumer)
    {
        _target = costumer;
    }
}