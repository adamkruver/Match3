using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Kruver.Mvvm.Exceptions;
using Kruver.Mvvm.Methods;
using Kruver.Mvvm.Methods.Attributes;
using Kruver.Mvvm.Methods.Factories;
using Kruver.Mvvm.Properties;
using Kruver.Mvvm.Properties.Attributes;
using Kruver.Mvvm.ViewModels;
using Kruver.Mvvm.Views;
using UnityEngine;

namespace Kruver.Mvvm
{
    public class Binder
    {
        private static readonly BindingFlags _fieldBindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;

        private readonly BindableMethodFactory _bindableMethodFactory = new BindableMethodFactory();
        private readonly List<Type> _notFoundTypes = new List<Type>();

        public void Bind(BindableView view, IViewModel viewModel)
        {
            _notFoundTypes.Clear();

            ApplyToFields(view, viewModel, BindField);
            ApplyToMethods(view, viewModel, BindMethod);

            viewModel.Destroyed += view.Unbind;

            if (_notFoundTypes.Count > 0)
                throw new NotFoundBindableViewException(view.GetType(), _notFoundTypes);
        }

        public void Unbind(BindableView view, IViewModel viewModel)
        {
            viewModel.Destroyed -= view.Unbind;
            viewModel.Disable();
            
            _notFoundTypes.Clear();

            ApplyToFields(view, viewModel, UnbindField);
            ApplyToMethods(view, viewModel, UnbindMethod);

            if (_notFoundTypes.Count > 0)
                throw new NotFoundBindableViewException(view.GetType(), _notFoundTypes);
        }

        private void ApplyToFields(BindableView view, IViewModel viewModel,
            Action<IViewModel, FieldInfo, IBindableViewProperty> action)
        {
            foreach (FieldInfo fieldInfo in viewModel.GetType().GetFields(_fieldBindingFlags))
            {
                foreach (object attribute in fieldInfo.GetCustomAttributes(true))
                {
                    if (attribute is PropertyBindingAttribute bindingAttribute)
                    {
                        IBindableViewProperty bindableViewProperty = GetBindableProperty(view, bindingAttribute);

                        if (bindableViewProperty == null)
                        {
                            _notFoundTypes.Add(bindingAttribute.ComponentType);
                            continue;
                        }

                        action.Invoke(viewModel, fieldInfo, bindableViewProperty);
                    }
                }
            }
        }

        private void ApplyToMethods(BindableView view, IViewModel viewModel,
            Action<IViewModel, MethodInfo, IBindableViewMethod> action)
        {
            foreach (MethodInfo methodInfo in viewModel.GetType().GetMethods(_fieldBindingFlags))
            {
                foreach (object attribute in methodInfo.GetCustomAttributes(true))
                {
                    if (attribute is MethodBindingAttribute bindingAttribute)
                    {
                        IBindableViewMethod bindableViewMethod = GetBindableMethod(view, bindingAttribute);

                        if (bindableViewMethod == null)
                        {
                            _notFoundTypes.Add(bindingAttribute.ComponentType);
                            continue;
                        }

                        action.Invoke(viewModel, methodInfo, bindableViewMethod);
                    }
                }
            }
        }

        private void BindField(IViewModel viewModel, FieldInfo fieldInfo, IBindableViewProperty bindableViewProperty)
        {
            fieldInfo.SetValue(viewModel, bindableViewProperty.OnBind());
        }

        private void UnbindField(IViewModel viewModel, FieldInfo fieldInfo, IBindableViewProperty bindableViewProperty)
        {
            (fieldInfo.GetValue(viewModel) as IDisposable)?.Dispose();
            fieldInfo.SetValue(viewModel, null);
        }

        private void BindMethod(IViewModel viewModel, MethodInfo methodInfo, IBindableViewMethod bindableViewMethod)
        {
            bindableViewMethod.OnBind(_bindableMethodFactory.Create(viewModel, methodInfo));
        }

        private void UnbindMethod(IViewModel viewModel, MethodInfo methodInfo, IBindableViewMethod bindableViewMethod)
        {
            bindableViewMethod.Unbind();
        }

        private IBindableViewMethod GetBindableMethod(BindableView view, MethodBindingAttribute bindingAttribute)
        {
            return view
                           .GetComponents(bindingAttribute.ComponentType)
                           .FirstOrDefault(childComponent => childComponent.name ==
                                                             (bindingAttribute.ComponentName ?? childComponent.name))
                       as IBindableViewMethod
                   ?? view
                           .GetComponentsInChildren(bindingAttribute.ComponentType, true)
                           .FirstOrDefault(childComponent => childComponent.name ==
                                                             (bindingAttribute.ComponentName ?? childComponent.name))
                       as IBindableViewMethod;
        }

        private IBindableViewProperty GetBindableProperty(BindableView view, PropertyBindingAttribute bindingAttribute)
        {
            return view
                           .GetComponents(bindingAttribute.ComponentType)
                           .FirstOrDefault(childComponent => childComponent.name ==
                                                             (bindingAttribute.ComponentName ?? childComponent.name))
                       as IBindableViewProperty
                   ?? view
                           .GetComponentsInChildren(bindingAttribute.ComponentType, true)
                           .FirstOrDefault(childComponent => childComponent.name ==
                                                             (bindingAttribute.ComponentName ?? childComponent.name))
                       as IBindableViewProperty;
        }
    }
}