using System;
using System.Linq;
using System.Reflection;
using Kruver.Mvvm.ViewModels;

namespace Kruver.Mvvm.Methods.Factories
{
    public class BindableMethodFactory
    {
        public object Create(IViewModel viewModel, MethodInfo methodInfo)
        {
            Type actionGenericType = typeof(BindableMethod<>);

            Type[] parameterTypes = methodInfo
                .GetParameters()
                .Select(info => info.ParameterType)
                .ToArray();

            Type actionType = actionGenericType.MakeGenericType(parameterTypes);

            return Activator.CreateInstance(actionType, new object[] { viewModel, methodInfo });
        }
    }
}