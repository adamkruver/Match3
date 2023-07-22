using Kruver.Mvvm.Properties;
using UnityEngine;
using UnityEngine.AI;

namespace Kruver.Mvvm.ViewBindings.NavMeshAgents
{
    public class NavMeshAgentDestinationBindable : BindableViewProperty<Vector3>
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        
        public override Vector3 BindableProperty
        {
            get => _navMeshAgent.destination;
            set => _navMeshAgent.destination = value;
        }
    }
}