using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class Candy : MonoBehaviour
{
    private     Passenger         _target;

    private void Update()
    {
        Vector3 distance = _target.transform.position - transform.position;

        if (_target != null) 
            transform.position += distance.normalized * FlyWeightPointer.candy.speed * Time.deltaTime;

        if(distance.magnitude < FlyWeightPointer.passenger.viewRadius)
        {
            Destroy(this.gameObject);
        }
    }

    public void Spawn(Passenger costumer)
    {
        _target = costumer;
    }
}