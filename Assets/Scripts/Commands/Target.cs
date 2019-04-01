using UnityEngine;
using Cuvium.Core;

namespace Cuvium.Commands
{
    public class Target
    {
        public Vector3 MousePosition { get; private set; }
        public RaycastHit Hit { get; private set; }
        public bool TargetExists { get; private set; }
        public CuviumController Controller { get; private set; }

        public Target()
        {
            MousePosition = Input.mousePosition;
            TargetExists = TryGetTarget(out var hit);
            if(TargetExists)
            {
                Hit = hit;
            }
            //Controller = Transform.GetComponent<CuviumController>();
        }

        private bool TryGetTarget(out RaycastHit hit)
        {
            var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            return Physics.Raycast(camRay, out hit);
        }
    }
}

