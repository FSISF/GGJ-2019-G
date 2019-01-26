using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOderInLayer : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        float sort = transform.position.y * 4;
        spriteRenderer.sortingOrder = Mathf.FloorToInt(sort*-1);
    }
}
