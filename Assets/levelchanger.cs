using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelchanger : MonoBehaviour
{
    public int levelid;
    public void changeLevel()
    {
        SceneManager.LoadScene(levelid);
    }
}
