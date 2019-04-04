using UnityEngine;
using Cuvium.Core;
using Cuvium.Behaviours;

namespace Cuvium.Commands
{
    [CreateAssetMenu(menuName = "Cuvium/Move Command")]
    public class MoveScriptableCommand : ScriptableCommand
    {
        private void OnEnable()
        {
            Name = "Move";
        }
 
        public override void Execute(CommandContext context, CuviumController controller)
        {
            var middle = context.Player.SelectedObjects.GetMiddlePoint();
            Debug.Log("Middle: " + middle);
            var destination = context.Target.Hit.point;
            Debug.Log("Destination: " + destination);
            var offset = controller.transform.position - middle;
            var moveable = controller as IMoveable;
            moveable.Move(destination + offset);
        }
    }
}

