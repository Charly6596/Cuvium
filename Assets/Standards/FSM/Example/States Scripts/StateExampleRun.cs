using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Overtime.FSM.Example
{
	public class StateExampleRun : StateExampleBase
	{
		[SerializeField]
		private float m_RunTime;
		[SerializeField]
		private float m_Speed;
		[SerializeField]
		private float m_SpeedAfterTrigger;

		public override void BuildTransitions ()
		{
			AddTransition(ExampleStateTransition.STOP_RUN, ExampleStateID.IDLE);
		}

		public override void Enter ()
		{
			StartCoroutine(WaitAndIdle());
		}

		private IEnumerator WaitAndIdle()
		{
			yield return new WaitForSeconds(m_RunTime);

			MakeTransition(ExampleStateTransition.STOP_RUN);
		}

		public override void Update ()
		{
			transform.Translate(transform.forward * m_Speed * Time.deltaTime);
		}

		public override void OnTriggerEnter (Collider collider)
		{
			m_Speed = m_SpeedAfterTrigger;
		}
	}
}