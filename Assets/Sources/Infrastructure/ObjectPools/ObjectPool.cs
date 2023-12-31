﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sources.Infrastructure.ObjectPools
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly List<T> _objects = new List<T>();

        private Transform _parent;

        public ObjectPool(string parentName = "")
        {
            if (string.IsNullOrEmpty(parentName))
                return;

            _parent = new GameObject(parentName).transform;
        }

        public T Get(Type type)
        {
            if (_objects.Count == 0)
                return null;
            
            return _objects.FirstOrDefault(obj => obj.gameObject.activeSelf == false && obj.GetType() == type);
        }

        public void Add(T @object)
        {
            Transform transform = @object.GetComponent<Transform>();
            transform.parent = _parent;
            
            _objects.Add(@object);
        }
    }
}