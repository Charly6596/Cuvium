using UnityEngine;

namespace Cuvium.Commands
{
    public class MoveCommand : Command
    {
        public MoveCommand(CommandContext context) : base(context)
        {
        }

        public void Execute()
        {
            var middle = Context.Player.SelectedObjects.GetMiddlePoint();
            var objs = Context.Player.SelectedObjects.ToArray();
            var destination = Context.Target.MousePosition;

            foreach(var obj in objs)
            {
                var offset = obj.transform.position - middle;
               // obj.Move(destination + offset);
            }
        }
    }
}

