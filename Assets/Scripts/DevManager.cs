using UnityEngine;
using UnityEngine.SceneManagement;

public class DevManager : MonoBehaviour
{
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.R))
        //{
        //    Reload();
        //}

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Reload();
        }
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
