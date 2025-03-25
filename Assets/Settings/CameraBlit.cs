using UnityEngine;

public class CameraBlit : MonoBehaviour
{
    public Material blitMaterial;

    public Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        blitMaterial.SetTexture("_RenderTexture", cam.targetTexture);
    }
}
