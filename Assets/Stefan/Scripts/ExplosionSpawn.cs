using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSpawn : MonoBehaviour
{
    [SerializeField] private Renderer billboardRenderer;
    [SerializeField] private float fadeSpeed = 3.7f;

    // Start is called before the first frame update
    void Start()
    {
        Color explosionColor = billboardRenderer.material.color;
        explosionColor = new Color(explosionColor.r, explosionColor.g, explosionColor.b, 0);
        billboardRenderer.material.color = explosionColor;
    }

    // Update is called once per frame
    void LateUpdate() {
        Color explosionColor = billboardRenderer.material.color;
        float fadeAmount = explosionColor.a + (Time.deltaTime * fadeSpeed);
        explosionColor = new Color(explosionColor.r, explosionColor.g, explosionColor.b, fadeAmount);
        billboardRenderer.material.color = explosionColor;
        Debug.Log(fadeAmount);

        if (explosionColor.a >= 1) {
            Destroy(gameObject);
        }
    }
}
