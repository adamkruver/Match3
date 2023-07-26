using System;

namespace Kruver.Mvvm.ViewModels
{
    public interface IViewModel
    {
        event Action Destroyed;
    
        void Enable();
        void Disable();
    }
}