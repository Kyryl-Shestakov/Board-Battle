using UnityEngine;
using UnityEngine.UI;

// ReSharper disable PossibleNullReferenceException

namespace Battle
{
    public class WeaponControl : MonoBehaviour
    {
        private int _impact;

        public int Impact
        {
            get { return _impact; }
            set
            {
                _impact = value;
                UpdateImpactIndicator();
            }
        }

        public float SpeedMultiplier;
        public Text HealthIndicator;
        public Text ImpactIndicator;

        private int _health;
        public int Health
        {
            get { return _health; }
            private set
            {
                _health = value;
                UpdateHealthIndicator();
            }
        }

        public GameObject ShotBallPrefab;

        void Start()
        {
            Health = 100;
        }

        public void OnCollisionEnter(Collision collision)
        {
            var shotBallDestructor = collision.gameObject.GetComponent<ShotBallDestruction>();
            if (shotBallDestructor != null)
            {
                shotBallDestructor.HandleHit(this);
            }
        }

        public void HandleHit(int hitImpact)
        {
            int resultingHealth = Health - hitImpact;
            if (resultingHealth < 0)
            {
                resultingHealth = 0;
            }

            Health = resultingHealth;

            if (Health == 0)
            {
                var battleController = GameObject.Find("Game Controller").GetComponent<BattleControl>();
                GetComponent<BattleResolution>().ConcludeBattle(battleController);
            }
        }

        public void Shoot()
        {
            var shotBall = Instantiate(ShotBallPrefab, transform.position + Camera.main.transform.forward, Quaternion.identity) as GameObject;
            shotBall.GetComponent<MeshRenderer>().material =
                       Resources.Load<Material>("Materials/" + BattleControl.BattleBootstrapper.ShotBallMaterial);
            shotBall.GetComponent<ParticleSystem>().startColor = BattleControl.BattleBootstrapper.BattleColor;
            shotBall.GetComponent<ShotBallDestruction>().Impact = Impact;
            shotBall.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * SpeedMultiplier;
        }

        public void UpdateHealthIndicator()
        {
            HealthIndicator.text = Health.ToString();
        }

        public void UpdateImpactIndicator()
        {
            ImpactIndicator.text = Impact.ToString();
        }
    }
}
