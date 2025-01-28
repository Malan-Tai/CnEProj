using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class SmoothLayoutElement : MonoBehaviour
    {
        public bool HasSpawned { get; private set; } = false;

        [SerializeField] private UnityEvent _spawned;

        public void Spawn()
        {
            HasSpawned = true;
            _spawned.Invoke();
        }
    }
}
