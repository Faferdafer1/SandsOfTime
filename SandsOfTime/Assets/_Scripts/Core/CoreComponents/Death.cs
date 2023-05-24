using UnityEditor.Tilemaps;
using UnityEngine;

namespace Greg.CoreSystem
{
    public class Death : CoreComponent
    {
        [SerializeField] private GameObject[] deathParticles;

        public GameObject gameController;
        private GameController GameController;

        private GameObject player;

        private ParticleManager ParticleManager => particleManager ? particleManager : core.GetCoreComponent(ref particleManager);
        private ParticleManager particleManager;

        public Stats Stats => stats ? stats : core.GetCoreComponent(ref stats);
        public Stats stats;

        public void Die()
        { 
            foreach(var particle in deathParticles)
            {
                ParticleManager.StartParticles(particle);
            }

            core.transform.parent.gameObject.SetActive(false);


            //player dies game over
            if (!player.activeSelf)
            {
                Debug.Log("test1");
                GameController.GameOver();
            }
        }

        private void Start()
        {
            player = GameObject.Find("Player");
            GameController = gameController.GetComponent<GameController>();
        }

        private void OnEnable()
        {
            Stats.Health.OnCurrentValueZero += Die;
        }

        private void OnDisable()
        {
            Stats.Health.OnCurrentValueZero -= Die;
        }
    }
}
