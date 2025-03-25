using System.Collections;
using UnityEngine;

public class MeshTrail : MonoBehaviour
{
    public float activeTime = 2f;

    private bool isTrailActive;

    public float meshRefreshRate = 0.1f;

    public float meshDestroyDelay = 3f;

    [Header("Shader")]
    public Material mat;
    public float shaderVarRate = 0.1f;
    public float shaderRefreshRate = 0.05f;

    public SkinnedMeshRenderer[] meshRenderers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateTrail()
    {
        if (meshRenderers == null)
        {
            meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        }

        StartCoroutine(ActivateTrail(activeTime));
    }

    private IEnumerator ActivateTrail(float timeActive)
    {
        isTrailActive = true;

        while (timeActive > 0)
        {
            timeActive -= meshRefreshRate;

            for (int i = 0; i < meshRenderers.Length; i++)
            {
                GameObject g = new GameObject();

                MeshRenderer mr = g.AddComponent<MeshRenderer>();
                MeshFilter mf = g.AddComponent<MeshFilter>();

                Mesh mesh = new Mesh();
                meshRenderers[i].BakeMesh(mesh);

                mf.mesh = mesh;
                mr.material = mat;

                g.transform.position = transform.position;
                g.transform.rotation = transform.rotation;

                StartCoroutine(AnimateAlpha(mr.material, 0f, shaderVarRate, shaderRefreshRate));

                Destroy(g, meshDestroyDelay);
            }

            yield return new WaitForSeconds(meshRefreshRate);
        }

        isTrailActive = false;
    }

    private IEnumerator AnimateAlpha(Material mat, float goal, float rate, float refreshRate)
    {
        float valueToAnimate = mat.GetFloat("_Alpha");

        while (valueToAnimate > goal)
        {
            valueToAnimate -= rate;
            mat.SetFloat("_Alpha", valueToAnimate);
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
