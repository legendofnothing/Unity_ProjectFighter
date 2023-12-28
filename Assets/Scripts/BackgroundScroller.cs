using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundScroller : MonoBehaviour
{
    public float speed;

    private Renderer renderer;
    private Vector2 savedOffset;

    #region Unity Methods

    void Start() {
        renderer = GetComponent<Renderer>();
    }

    void Update() {
        float y = Mathf.Repeat(Time.time * speed, 1);
        Vector2 offset = new Vector2(0 ,y);
        renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
    #endregion
}
    
