//==============================================================================
//Written by Jose Joao de Oliveira Junior, November 2017
//
//This is a modified version of the FSM implementation kindly provided by Kishimoto Studios 
//which, they said, was heavily based on Mat Buckland's book 'Programming Game AI By Example', 
//Brian Schwab's 'AI Game Engine Programming' and 'Game Programming Gems' and 'AI Game Programming Wisdom' series.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
//INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE 
//AND NON-INFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
//DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//==============================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Overtime.FSM
{
	/// <summary>
	/// Abstract class that defines a state for the FSM of <typeparamref name="TClassType"/> with enum states <typeparamref name="TEnumState"/> and enum transition <typeparamref name="TEnumTransition"/>.
	/// </summary>
	/// <typeparam name="TClassType"></typeparam>
	/// <typeparam name="TEnumState"></typeparam>
	/// <typeparam name="TEnumTransition"></typeparam>
	public abstract class State<TClassType, TEnumState, TEnumTransition> : ScriptableObject where TClassType : MonoBehaviour
	{
		public abstract void Enter();
		public abstract void Update();
		public abstract void FixedUpdate();
		public abstract void Exit();
		public abstract void BuildTransitions();
		public virtual void OnTriggerEnter(Collider collider) {}
		public virtual void OnTriggerStay(Collider collider) {}
		public virtual void OnTriggerExit(Collider collider) {}
		public virtual void OnCollisionEnter(Collision collision) {}
		public virtual void OnCollisionStay(Collision collision) {}
		public virtual void OnCollisionExit(Collision collision) {}

		private TClassType m_Parent;
		protected TClassType Parent
		{
			get { return m_Parent; }
		}

		private StateMachine<TClassType, TEnumState, TEnumTransition> m_FSM;
		public StateMachine<TClassType, TEnumState, TEnumTransition> FSM
		{
			get { return m_FSM; }
		}

		[SerializeField]
		private TEnumState m_StateID;
		public TEnumState StateID
		{
			get { return m_StateID; }
		}

		protected Dictionary<TEnumTransition, TEnumState> m_TransitionDictionary;

		#region monobehaviour properties
		protected GameObject gameObject
		{
			get { return Parent.gameObject; }
		}

		protected Transform transform
		{
			get { return Parent.transform; }
		}
		#endregion

		public void Initialize(StateMachine<TClassType, TEnumState, TEnumTransition> fsm, TClassType parent)
		{
			m_Parent = parent;
			m_FSM = fsm;

			m_TransitionDictionary = new Dictionary<TEnumTransition, TEnumState>();
		}

		public bool AddTransition(TEnumTransition enumTransition, TEnumState enumState, bool overwriteIfExists = true)
		{
			if (overwriteIfExists)
			{
				if(m_FSM.Debug)
				{
					bool didOverwrite = m_TransitionDictionary.ContainsKey(enumTransition);
					Debug.Log(m_StateID + " - State.AddTransition(" + enumTransition.GetType().Name + "." + enumTransition + ", " + enumState.GetType().Name + "." + enumState + ", " + overwriteIfExists + "): transition " + (didOverwrite ? "overwritten." : "added."));
				}
				m_TransitionDictionary[enumTransition] = enumState;
			}
			else
			{
				try
				{
					if(m_FSM.Debug)
					{
						Debug.Log(m_StateID + " - State.AddTransition(" + enumTransition.GetType().Name + "." + enumTransition + ", " + enumState.GetType().Name + "." + enumState + ", " + overwriteIfExists + "): transition added.");
					}
					m_TransitionDictionary.Add(enumTransition, enumState);
				}
				catch (ArgumentException)
				{
					Debug.LogError("ERROR! " + m_StateID + " - State.AddTransition(" + enumTransition.GetType().Name + "." + enumTransition + ", " + enumState.GetType().Name + "." + enumState + ", " + overwriteIfExists + "): trying to add transition that already exists.");
					return false;
				}
			}

			return true;
		}

		public bool RemoveTransition(TEnumTransition enumTransition)
		{
			if (m_TransitionDictionary.ContainsKey(enumTransition))
			{
				return m_TransitionDictionary.Remove(enumTransition);
			}

			Debug.LogError("ERROR! " + m_StateID + " State.RemoveTransition(" + enumTransition.GetType().Name + "." + enumTransition + "): trying to remove a transition that does not exist.");
			return false;
		}

		public TEnumState GetTransitionState(TEnumTransition enumTransition)
		{
			return m_TransitionDictionary.ContainsKey(enumTransition) ? m_TransitionDictionary[enumTransition] : m_StateID;
		}

		protected bool MakeTransition(TEnumTransition transition)
		{
			return FSM.MakeTransition(transition);
		}

		#region monobehaviour methods
		public Coroutine StartCoroutine(IEnumerator routine)
		{
			return Parent.StartCoroutine(routine);
		}

		public Coroutine StartCoroutine(string methodName)
		{
			return Parent.StartCoroutine(methodName);
		}

		public Coroutine StartCoroutine(string methodName, object value)
		{
			return Parent.StartCoroutine(methodName, value);
		}

		public void StopCoroutine(string methodName)
		{
			Parent.StopCoroutine(methodName);
		}

		public void StopCoroutine(IEnumerator routine)
		{
			Parent.StopCoroutine(routine);
		}

		public void StopCoroutine(Coroutine routine)
		{
			Parent.StopCoroutine(routine);
		}

		public void StopAllCoroutines()
		{
			Parent.StopAllCoroutines();
		}
		#endregion
	}
}