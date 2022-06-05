using UnityEngine;

public class CandySpawner : MonoBehaviour
{
    public static CandySpawner instance;

    [SerializeField] private GameObject     _alfajor;
    [SerializeField] private GameObject     _mantekel;
    [SerializeField] private Transform      _spawner;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    public void RequestACandy(Passenger costumer, CandyType type)
    {
        switch (type) 
        {
            case CandyType.Alfajor:
                GameObject alfajor = Instantiate(_alfajor);
                alfajor.transform.position = _spawner.position;
                alfajor.GetComponent<Candy>().Spawn(costumer);
                break;

            case CandyType.Mantekel:
                GameObject mantekel = Instantiate(_mantekel);
                mantekel.transform.position = _spawner.position;
                mantekel.GetComponent<Candy>().Spawn(costumer);
                break;
        }

    }
}