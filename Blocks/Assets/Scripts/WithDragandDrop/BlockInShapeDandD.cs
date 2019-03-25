using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockInShapeDandD : MonoBehaviour
{
    [SerializeField] RawImage border;
    [SerializeField] RawImage mainSprite;


    public void SetActiveBorder(bool value)
    {
        border.enabled = value;
    }

    public void SetActiveMainSprite(bool value)
    {
        mainSprite.enabled = value;
    }

    public void SetSpriteForMainSpriteByStatic()
    {

    }
}
