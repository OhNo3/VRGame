using UnityEngine;
using System.Collections;

public class ScroolArrows : MonoBehaviour
{
    [SerializeField]
    private float speed;

    void Update()
    {
        float scroll = Mathf.Repeat(Time.time * speed, 1);
        Vector2 offset = new Vector2(-scroll, 0);
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}