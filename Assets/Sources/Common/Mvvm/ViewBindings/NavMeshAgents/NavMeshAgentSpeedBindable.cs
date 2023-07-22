using Kruver.Mvvm.Methods;
using UnityEngine;
using UnityEngine.AI;

namespace Kruver.Mvvm.ViewBindings.NavMeshAgents
{
    public class NavMeshAgentSpeedBindable : BindableViewMethod<float>
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;

        private void Update()
        {
            BindingCallback.Invoke(_navMeshAgent.velocity.magnitude);
        }
    }
}