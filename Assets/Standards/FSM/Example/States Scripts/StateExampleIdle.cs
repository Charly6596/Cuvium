using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Overtime.FSM.Example
{
	public class StateExampleIdle : StateExampleBase
	{
		[SerializeField]
		private float m_IdleTime;

		public override void BuildTransitions ()
		{
			AddTransition(ExampleStateTransition.START_RUN, ExampleStateID.RUN);
		}

		public override void Enter ()
		{
			StartCoroutine(WaitAndRun());
		}

		private IEnumerator WaitAndRun()
		{
			yield return new WaitForSeconds(m_IdleTime);

			MakeTransition(ExampleStateTransition.START_RUN);
		}
	}
}