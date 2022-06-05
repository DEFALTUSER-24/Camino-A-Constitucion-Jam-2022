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
        ChangeSkin(PassagersSkinsManager.instance.GetMyASkin());
    }

    public void ChangeSkin(Sprite skin)
    {
        _sr.sprite = skin;
    }
}