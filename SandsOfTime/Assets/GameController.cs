using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Greg
{
    public class GameController : MonoBehaviour
    {
        public DeathMenu DeathMenu;
        public void GameOver()
        {
            PauseGame();
            DeathMenu.Setup();
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
        }

        public void StartGame()
        {
            Time.timeScale = 1;
        }

        public void Start()
        {
            StartGame();
            DeathMenu.Disable();
        }

    }
}
