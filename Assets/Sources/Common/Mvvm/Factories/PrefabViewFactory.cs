using System;
using Kruver.Mvvm.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Kruver.Mvvm.Factories
{
    public class PrefabViewFactory
    {
        public T Create<T>(string path = "") where T : BindableView =>
            Object.Instantiate(Resources.Load<T>(path + typeof(T).Name));

        public BindableView Create(Type viewType, string path = "") =>
            (BindableView)Object.Instantiate(Resources.Load(path + viewType.Name, viewType));
    }
}