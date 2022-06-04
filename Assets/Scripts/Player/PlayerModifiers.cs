using UnityEngine;

public class PlayerModifiers : MonoBehaviour
{
    public static PlayerModifiers instance;

    [SerializeField] [Range(0, 100)]    private     float       _candyRequestPercent;
    [SerializeField]                    private     float       _speedModifier;
    [SerializeField]                    private     float       _currencyModifier;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    public float RequestPercent()
    {
        return _candyRequestPercent;
    }
}