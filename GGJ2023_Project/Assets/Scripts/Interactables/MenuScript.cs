using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    
        public void Quit()
        {
            Application.Quit();
        }

    
        public void Go()
        {
            SceneManager.LoadScene(1);
        }
    }

