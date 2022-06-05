using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] private GameObject      _rock;
    [SerializeField] private GameObject      _money;

    public void Throw(PlayerHit player, PassengerObjects type)
    {
        switch (type)
        {
            case PassengerObjects.Rocks:
                GameObject rock = Instantiate(_rock);
                rock.transform.position = transform.position;
                rock.GetComponent<ThrowingObject>().Spawn(player);
                break;

            case PassengerObjects.Money:
                GameObject money = Instantiate(_money);
                money.transform.position = transform.position;
                money.GetComponent<ThrowingObject>().Spawn(player);
                break;
        }
    }
}