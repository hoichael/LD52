using UnityEngine;
using UnityEngine.SceneManagement;

public class DevManager : MonoBehaviour
{
    [SerializeField] Camera baseCam;
    [SerializeField] MeshRenderer rtQuadRenderer;
    [SerializeField] RenderTexture renTex128, renTex256, renTex384, renTex512, renTex640, renText768;
    [SerializeField] Material m128, m256, m384, m512, m640, m768;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Backspace))
        {
            Reload();
        }

        /*
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            baseCam.targetTexture = renTex128;
            rtQuadRenderer.material = m128;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            baseCam.targetTexture = renTex256;
            rtQuadRenderer.material = m256;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            baseCam.targetTexture = renTex384;
            rtQuadRenderer.material = m384;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            baseCam.targetTexture = renTex512;
            rtQuadRenderer.material = m512;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            baseCam.targetTexture = renTex640;
            rtQuadRenderer.material = m640;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            baseCam.targetTexture = renText768;
            rtQuadRenderer.material = m768;
        }
        */
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
