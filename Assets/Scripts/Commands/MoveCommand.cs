using UnityEngine;

namespace Cuvium.Commands
{
    [CreateAssetMenu(menuName = "Cuvium/Move Command")]
    public class MoveCommand : Command
    {
        private void OnEnable()
        {
            Name = "Move";
        }

        public override ExecutionResult Execute(CommandContext context)
        {
            var middle = context.Player.SelectedObjects.GetMiddlePoint();
            var destination = context.Target.Hit.point;
            var offset = context.Controller.transform.position - middle;
            if(context.Controller is IMoveable moveable)
            {
                moveable.Move(destination + offset);
                return ExecutionResult.Suscess();
            }
            return ExecutionResult.InvalidOperation(Context);
        }
    }
}

