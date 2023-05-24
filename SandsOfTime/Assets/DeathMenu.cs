using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Greg
{
    public class DeathMenu : MonoBehaviour
    {
        public void Setup()
        {
            Debug.Log("Test2");
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            Debug.Log("Test3");
            gameObject.SetActive(false);
        }

        public void Restart()
        {
            SceneManager.LoadScene(0);
        }
        public void Quit()
        {
            Debug.Log("Quit!");
            Application.Quit();
        }
    }
}
