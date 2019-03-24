using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Overtime.FSM;

namespace Overtime.FSM.Example
{
	public enum ExampleStateID
	{
		IDLE,
		RUN,
	}

	public enum ExampleStateTransition
	{
		START_RUN,
		STOP_RUN,
	}

	public abstract class StateExampleBase : State<FSMExample, ExampleStateID, ExampleStateTransition>
	{
		public override void BuildTransitions ()
		{
			
		}

		public override void Enter ()
		{
			
		}

		public override void Exit ()
		{
			
		}

		public override void FixedUpdate ()
		{
			
		}

		public override void Update ()
		{
			
		}
	}
}