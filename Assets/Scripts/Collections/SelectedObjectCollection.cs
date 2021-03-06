﻿using UnityEngine;
using System.Collections.Generic;
using Cuvium.Behaviours;

namespace Cuvium.Core
{
    [CreateAssetMenu(menuName = "Cuvium/Selected Objects Collection")]
    public class SelectedObjectCollection : Collection<CuviumController>
    {
        private void OnEnable()
        {
            Clear();
        }

        private void OnDisable()
        {
            Clear();
        }

        public void Select(CuviumController obj, bool multiSelect = false)
        {
            if(List.Contains(obj)) { return; }

            if(!multiSelect)
            {
                DeselectAll();
            }
            obj.Select();
        }

        public void Select(CuviumController[] objs, bool multiSelect = false)
        {
            if(!multiSelect)
            {
                DeselectAll();
            }

            foreach(var obj in objs)
            {
                Select(obj, true);
            }
        }

        public void DeselectAll()
        {
            var objs = List as List<CuviumController>;
            foreach(var obj in objs)
            {
                obj.Deselect();
            }
            Clear();
        }

        public void MoveAll(Vector3 destination)
        {
            var middle = GetMiddlePoint();
            var controllers = ToList();
            foreach(var controller in controllers)
            {
                if(controller is IMoveable moveable)
                {
                    var offset = controller.transform.position - middle;
                    moveable.Move(destination + offset);
                }
            }

        }

        public List<IMoveable> GetMoveables()
        {
            var moveables = new List<IMoveable>();
            var objs = ToList();
            foreach(var obj in objs)
            {
                if(obj is IMoveable moveable)
                {
                    moveables.Add(moveable);
                }
            }
            return moveables;
        }

        public List<CuviumController> ToList()
        {
            return List as List<CuviumController>;
        }

        public CuviumController[] ToArray()
        {
            return List as CuviumController[];
        }

        public Vector3 GetMiddlePoint()
        {
            Vector3 posSum = Vector3.zero;
            var objs = List as List<CuviumController>;
            foreach(var obj in objs)
            {
                posSum += obj.transform.position;
            }
            Vector3 middle = posSum / List.Count;
            return middle;
        }
    }
}

