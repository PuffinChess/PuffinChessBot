using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, offsetColor;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject _highlight;

    public void Init(bool isOffset )
    {
        _spriteRenderer.color = isOffset ? _baseColor : offsetColor;
    }

    void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        _highlight.SetActive(false);
    }
}
