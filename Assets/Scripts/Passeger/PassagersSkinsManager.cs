using System.Collections.Generic;
using UnityEngine;

public class PassagersSkinsManager : Passenger
{
    public static PassagersSkinsManager instance;
    [SerializeField] public List<Sprite> _allSkins = new List<Sprite>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);

        //
    }

    public Sprite GetMyASkin()
    {
        return _allSkins[Random.Range(0, _allSkins.Count)];
    }
}