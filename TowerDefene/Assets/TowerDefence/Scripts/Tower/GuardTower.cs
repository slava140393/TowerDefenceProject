using UnityEngine;

namespace TowerDefence.Scripts.Tower
{
    public class GuardTower : MonoBehaviour
    {
        [SerializeField] private GuardLauncher _guardLauncher;

        private void Start()
        {
            _guardLauncher.Initialize();
        }
    }
}
