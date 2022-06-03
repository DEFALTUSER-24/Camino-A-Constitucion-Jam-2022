using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Cam { get; private set; }

    [Header("Cursor options")]
    [SerializeField]
    private Texture2D                       _cursorNormal;
    [SerializeField]
    private Texture2D                       _cursorHover;
    [SerializeField]
    private Vector2                         _cursorOffset;
    
    [Header("Fade options")]
    [SerializeField]
    private GameObject                      _fadePanel;

    private void Awake()
    {
        Cam = this;
    }

    private void Start()
    {
        //StartCoroutine(FadeTest());
    }

    public void SetCursorHoveringState(bool bIsHovering)
    {
        Cursor.SetCursor(bIsHovering ? _cursorHover : _cursorNormal, _cursorOffset, CursorMode.ForceSoftware);
    }

    public void SetCursorTexture(Texture2D _customTexture, Vector2 _offset)
    {
        Cursor.SetCursor(_customTexture, _offset, CursorMode.ForceSoftware);
    }

    public void Fade()
    {
        _fadePanel.GetComponent<Animator>().SetTrigger("BeginFade");
    }

    /*IEnumerator FadeTest()
    {
        yield return new WaitForSeconds(5);
        Fade();
    }*/
}
