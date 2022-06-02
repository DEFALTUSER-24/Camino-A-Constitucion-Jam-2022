using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SkinCall : MonoBehaviour
{
    [SerializeField] private SpriteRenderer     _sr;

    private void Awake()
    {
        if(_sr == null) _sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _sr.sprite = PassagersSkinsManager.instance.GetMyASkin();
    }
}