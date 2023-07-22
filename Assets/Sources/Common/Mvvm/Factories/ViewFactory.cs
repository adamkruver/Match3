using System;
using System.Reflection;
using Kruver.Mvvm.Views;

namespace Kruver.Mvvm.Factories
{
    public class ViewFactory
    {
        private static readonly string _cunstructorMethodName = "Construct";

        private static readonly BindingFlags _bindingFlags =
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        private readonly PrefabViewFactory _prefabViewFactory = new PrefabViewFactory();
        private readonly Binder _binder = new Binder();

        public TView Create<TView>(string viewPath = "")
            where TView : IBindableView
        {
            return (TView)Create(typeof(TView), viewPath);
        }

        public IBindableView Create(Type viewType, string viewPath = "")
        {
            BindableView view = _prefabViewFactory.Create(viewType, viewPath);
            Construct(view);

            return view;
        }

        private void Construct(BindableView view)
        {
            MethodInfo methodInfo = typeof(BindableView).GetMethod(_cunstructorMethodName, _bindingFlags);
            methodInfo.Invoke(view, new object[] { _binder });
        }
    }
}