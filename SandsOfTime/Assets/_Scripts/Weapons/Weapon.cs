using System;
using UnityEngine;
using Greg.Utilities;
using Greg.CoreSystem;

namespace Greg.Weapons
{
    public class Weapon : MonoBehaviour
    {        
        [SerializeField] private float attackCounterResetCooldown;

        public WeaponDataSO Data { get; private set; }

        public int CurrentAttackCounter
        {
            get => currentAttackCounter;
            private set => currentAttackCounter = value >= Data.NumberOfAttacks ? 0 : value; // if value is = to number of attacks it = 0 if not = value
        }

        public event Action OnEnter;
        public event Action OnExit;

        private Animator anim;
        public GameObject BaseGameObject { get; private set; }
        public GameObject WeaponSpriteGameObject { get; private set; }

        public WeaponAnimationEventHandler EventHandler { get; private set; }

        public Core Core { get; private set; }

        private int currentAttackCounter;

        private Timer attackCounterResetTimer;

        public void Enter()
        {
            print($"{transform.name} enter");

            attackCounterResetTimer.StopTimer();

            anim.SetBool("active", true);
            anim.SetInteger("counter", CurrentAttackCounter);

            OnEnter?.Invoke();
        }

        public void SetCore(Core core)
        {
            Core = core;
        }

        public void SetData(WeaponDataSO data)
        {
            Data = data;
        }

        private void Exit()
        {
            anim.SetBool("active", false);

            CurrentAttackCounter++;
            attackCounterResetTimer.StartTimer();

            OnExit?.Invoke();
        }

        private void Awake()
        {
            BaseGameObject = transform.Find("Base").gameObject;
            WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;
            anim = BaseGameObject.GetComponent<Animator>();

            EventHandler = BaseGameObject.GetComponent<WeaponAnimationEventHandler>();

            attackCounterResetTimer = new Timer(attackCounterResetCooldown);
        }

        private void Update()
        {
            attackCounterResetTimer.Tick();
        }

        private void ResetAttackCounter() => CurrentAttackCounter = 0;

        private void OnEnable()
        {
            EventHandler.OnFinish += Exit;
            attackCounterResetTimer.OnTimerDone += ResetAttackCounter;
        }

        private void OnDisable()
        {
            EventHandler.OnFinish -= Exit;
            attackCounterResetTimer.OnTimerDone -= ResetAttackCounter;
        }
    }
}
