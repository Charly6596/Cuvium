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
using System.Collections.Generic;
using UnityEngine;

namespace Overtime.FSM
{
	/// <summary>
	/// Finite State Machine of <typeparamref name="TClassType"/> with enum states <typeparamref name="TEnumState"/> and enum transition <typeparamref name="TEnumTransition"/>.
	/// </summary>
	/// <typeparam name="TClassType"></typeparam>
	/// <typeparam name="TEnumState"></typeparam>
	/// <typeparam name="TEnumTransition"></typeparam>
	[System.Serializable]
	public class StateMachine<TClassType, TEnumState, TEnumTransition> where TClassType : MonoBehaviour
	{
		protected TClassType m_Parent;

		private Dictionary<TEnumState, State<TClassType, TEnumState, TEnumTransition>> m_StatesDictionary;

		protected State<TClassType, TEnumState, TEnumTransition> m_CurrentState;

		/// <summary>
		/// Keep tracking of transitions. Must be activated through 
		/// </summary>
		private Stack<State<TClassType, TEnumState, TEnumTransition>> m_StateStack;

		private bool m_DidSetInitialState;

		private bool m_TrackStates = false;
		public bool TrackStates
		{
			get { return m_TrackStates; }

			set	
			{ 
				m_TrackStates = value; 

				if(m_TrackStates && m_StateStack == null)
				{
					m_StateStack = new Stack<State<TClassType, TEnumState, TEnumTransition>>();
				}
			}
		}

		private bool m_Debug = false;
		public bool Debug
		{
			get { return m_Debug; }

			set	{ m_Debug = value; }
		}

		public StateMachine(TClassType parent, ScriptableObject[] states, TEnumState initialState, bool debug = false, bool trackStates = false)
		{
			m_Parent = parent;
			m_StatesDictionary = new Dictionary<TEnumState, State<TClassType, TEnumState, TEnumTransition>>();
			m_Debug = debug;
			m_TrackStates = trackStates;

			if(m_TrackStates)
			{
				m_StateStack = new Stack<State<TClassType, TEnumState, TEnumTransition>>();
			}

			m_CurrentState = null;

			m_DidSetInitialState = false;

			foreach(ScriptableObject state in states)
			{
				if(state.GetType().IsSubclassOf(typeof(State<TClassType, TEnumState, TEnumTransition>)))
				{
					State<TClassType, TEnumState, TEnumTransition> stateCopy = GameObject.Instantiate(state) as State<TClassType, TEnumState, TEnumTransition>;
					AddState(stateCopy);
					stateCopy.Initialize(this, parent);
					stateCopy.BuildTransitions();
				}
				else
				{
					UnityEngine.Debug.LogError(string.Format("State {0} is not subclass of {1}", state.GetType(), typeof(State<TClassType, TEnumState, TEnumTransition>)));
				}
			}

			SetInitialState(initialState);
		}

		public State<TClassType, TEnumState, TEnumTransition> CurrentState
		{
			get { return m_CurrentState; }
		}

		public string CurrentStateName
		{
			get { return m_CurrentState.GetType().Name; }
		}

		/// <summary>
		/// Call this method on MonoBehaviour.OnDestroy
		/// </summary>
		public void Destroy()
		{
			ClearCurrentState();

			if(m_TrackStates)
				ClearHistory();

			m_Parent = null;
			m_StatesDictionary = null;
		}

		#region UPDATES
		public void Update()
		{
			if (m_CurrentState != null)
				m_CurrentState.Update();
		}

		public void FixedUpdate()
		{
			if (m_CurrentState != null)
				m_CurrentState.FixedUpdate();
		}
		#endregion

		#region TRIGGER / COLLISION
		public void OnTriggerEnter(Collider collider)
		{
			if(m_CurrentState != null)
				m_CurrentState.OnTriggerEnter(collider);
		}

		public void OnTriggerStay(Collider collider)
		{
			if(m_CurrentState != null)
				m_CurrentState.OnTriggerStay(collider);
		}

		public void OnTriggerExit(Collider collider)
		{
			if(m_CurrentState != null)
				m_CurrentState.OnTriggerExit(collider);
		}

		public void OnCollisionEnter(Collision collision)
		{
			if(m_CurrentState != null)
				m_CurrentState.OnCollisionEnter(collision);
		}

		public void OnCollisionStay(Collision collision)
		{
			if(m_CurrentState != null)
				m_CurrentState.OnCollisionStay(collision);
		}

		public void OnCollisionExit(Collision collision)
		{
			if(m_CurrentState != null)
				m_CurrentState.OnCollisionExit(collision);
		}
		#endregion

		public void SetInitialState(TEnumState enumState)
		{
			if (!m_DidSetInitialState)
			{
				ChangeState(enumState);
				m_DidSetInitialState = true;
			}
			else
			{
				UnityEngine.Debug.LogError("ERROR! StateMachine<" + m_Parent.GetType().Name + ">.SetInitialState(" + enumState.GetType().Name + "." + enumState + "): initial state already set.");
			}
		}

		public void SetInitialState(State<TClassType, TEnumState, TEnumTransition> state)
		{
			if (!m_DidSetInitialState)
			{
				ChangeState(state);
				m_DidSetInitialState = true;
			}
			else
			{
				UnityEngine.Debug.LogError("ERROR! StateMachine<" + m_Parent.GetType().Name + ">.SetInitialState(" + state.GetType().Name + "): initial state already set.");
			}
		}

		public bool MakeTransition(TEnumTransition enumTransition)
		{
			if (m_CurrentState == null)
			{
				UnityEngine.Debug.LogError("ERROR! StateMachine<" + m_Parent.GetType().Name + ">.MakeTransition(" + enumTransition.GetType().Name + "." + enumTransition + "): current state is null. Did you forget to set the initial state?");
				return false;
			}

			TEnumState transitionState = m_CurrentState.GetTransitionState(enumTransition);
			if (m_CurrentState.StateID.Equals(transitionState))
			{
				UnityEngine.Debug.LogError("ERROR! StateMachine<" + m_Parent.GetType().Name + ">.MakeTransition(" + enumTransition.GetType().Name + "." + enumTransition + "): transition leads to current state OR the transition is probably invalid.");
				return false;
			}

			ChangeState(transitionState);
			return true;
		}

		public bool AddState(State<TClassType, TEnumState, TEnumTransition> newState, bool overwriteIfExists = true)
		{
			if (newState == null)
			{
				UnityEngine.Debug.LogError("ERROR! StateMachine<" + m_Parent.GetType().Name + ">.AddState(" + newState.StateID.GetType().Name + "." + newState.StateID + ", null, " + overwriteIfExists + "): called with null state.");
				return false;
			}

			if (overwriteIfExists)
			{
				if(m_Debug)
				{
					bool didOverwrite = m_StatesDictionary.ContainsKey(newState.StateID);
					UnityEngine.Debug.Log("StateMachine<" + m_Parent.GetType().Name + ">.AddState(" + newState.StateID.GetType().Name + "." + newState.StateID + ", " + newState.GetType().Name + ", " + overwriteIfExists + "): state " + (didOverwrite ? "overwritten." : "added."));
				}
				m_StatesDictionary[newState.StateID] = newState;
			}
			else
			{
				try
				{
					if(m_Debug)
					{
						UnityEngine.Debug.Log("StateMachine<" + m_Parent.GetType().Name + ">.AddState(" + newState.StateID.GetType().Name + "." + newState.StateID + ", " + newState.GetType().Name + ", " + overwriteIfExists + "): state added.");
					}
					m_StatesDictionary.Add(newState.StateID, newState);
				}
				catch (ArgumentException)
				{
					UnityEngine.Debug.LogError("ERROR! StateMachine<" + m_Parent.GetType().Name + ">.AddState(" + newState.StateID.GetType().Name + "." + newState.StateID + ", " + newState.GetType().Name + ", " + overwriteIfExists + "): trying to add state that already exists.");
					return false;
				}
			}

			return true;
		}

		public bool RemoveState(TEnumState enumState, bool forceIfCurrent = false)
		{
			if (m_StatesDictionary.ContainsKey(enumState))
			{
				if (m_StatesDictionary[enumState] == m_CurrentState)
				{
					if (forceIfCurrent)
					{
						UnityEngine.Debug.LogWarning("WARNING! StateMachine<" + m_Parent.GetType().Name + ">.RemoveState(" + enumState.GetType().Name + "." + enumState + ", " + forceIfCurrent + "): removing current state.");

						if(m_TrackStates)
						{
							if (m_StateStack.Contains(m_StatesDictionary[enumState]))// && m_StateStack.Peek() != m_StatesDictionary[enumState])
							{
								UnityEngine.Debug.LogWarning("WARNING! StateMachine<" + m_Parent.GetType().Name + ">.RemoveState(" + enumState.GetType().Name + "." + enumState + ", " + forceIfCurrent + "): removed state is in state stack.");
							}
						}

						m_CurrentState = null;
						return m_StatesDictionary.Remove(enumState);
					}
					else
					{
						UnityEngine.Debug.LogError("ERROR! StateMachine<" + m_Parent.GetType().Name + ">.RemoveState(" + enumState.GetType().Name + "." + enumState + ", " + forceIfCurrent + "): trying to remove current state.");
						return false;
					}
				}
				else
				{
					if(m_TrackStates)
					{
						if (m_StateStack.Contains(m_StatesDictionary[enumState]))// && m_StateStack.Peek() != m_StatesDictionary[enumState])
						{
							UnityEngine.Debug.LogWarning("WARNING! StateMachine<" + m_Parent.GetType().Name + ">.RemoveState(" + enumState.GetType().Name + "." + enumState + ", " + forceIfCurrent + "): removed state is in state stack.");
						}
					}

					return m_StatesDictionary.Remove(enumState);
				}
			}

			UnityEngine.Debug.LogError("ERROR! StateMachine<" + m_Parent.GetType().Name + ">.RemoveState(" + enumState.GetType().Name + "." + enumState + ", " + forceIfCurrent + "): trying to remove a state that does not exist.");
			return false;
		}

		// ChangeState methods used to be public but we decided to no longer expose them to the user, since we now have MakeTransition method.
		private void ChangeState(TEnumState enumState)
		{
			if (!m_StatesDictionary.ContainsKey(enumState))
			{
				UnityEngine.Debug.LogError("ERROR! StateMachine<" + m_Parent.GetType().Name + ">.ChangeState(" + enumState.GetType().Name + "." + enumState + "): state not found in FSM.");
				return;
			}

			if (m_StatesDictionary[enumState] == m_CurrentState)
			{
				UnityEngine.Debug.LogError("ERROR! StateMachine<" + m_Parent.GetType().Name + ">.ChangeState(" + enumState.GetType().Name + "." + enumState + "): new state is the same as the current state.");
				return;
			}

			InternalChangeState(m_StatesDictionary[enumState], true);
		}

		private void ChangeState(State<TClassType, TEnumState, TEnumTransition> newState)
		{
			if (newState == null)
			{
				UnityEngine.Debug.LogError("ERROR! StateMachine<" + m_Parent.GetType().Name + ">.ChangeState(null): called with null state.");
				return;
			}

			if (newState == m_CurrentState)
			{
				UnityEngine.Debug.LogError("ERROR! StateMachine<" + m_Parent.GetType().Name + ">.ChangeState(" + newState.GetType().Name + "): new state is the same as the current state.");
				return;
			}

			InternalChangeState(newState, true);
		}

		private void InternalChangeState(State<TClassType, TEnumState, TEnumTransition> newState, bool isNewState)
		{
			if(m_Debug)
			{
				UnityEngine.Debug.Log("StateMachine<" + m_Parent.GetType().Name + ">.InternalChangeState(" + newState.GetType().Name + ").");
			}

			if (m_CurrentState != null)
			{
				if(m_Debug)
				{
					UnityEngine.Debug.Log("StateMachine<" + m_Parent.GetType().Name + ">.InternalChangeState(" + newState.GetType().Name + "): leaving current state " + m_CurrentState.GetType().Name + ".");
				}

				m_CurrentState.Exit();
			}

			m_CurrentState = newState;

			if(m_Debug)
			{
				UnityEngine.Debug.Log("StateMachine<" + m_Parent.GetType().Name + ">.InternalChangeState(" + newState.GetType().Name + "): entering new state " + m_CurrentState.GetType().Name + ".");
			}

			m_CurrentState.Enter();

			// When isNewState is false, we are reverting to a previous state and so we don't push the state on the stack.
			if (isNewState)
			{
				if(m_TrackStates)
				{
					m_StateStack.Push(m_CurrentState);
				}
			}
		}

		public void ClearCurrentState()
		{
			if (m_CurrentState != null)
				m_CurrentState.Exit();

			m_CurrentState = null;
		}

		public void RevertToPreviousState()
		{
			if(m_TrackStates)
			{
				// Pop the first state on the stack (which is the current state).
				if (m_StateStack.Count > 0)
				{
					m_StateStack.Pop();
				}

				// If we still have a state in the stack, revert to it.
				if (m_StateStack.Count > 0)
				{
					// Get the previous state.
					State<TClassType, TEnumState, TEnumTransition> previousState = m_StateStack.Peek();

					if(m_Debug)
					{
						UnityEngine.Debug.Log("StateMachine<" + m_Parent.GetType().Name + ">.RevertToPreviousState(): " + previousState.GetType().Name);
					}

					InternalChangeState(previousState, false);
				}
				else
				{
					UnityEngine.Debug.LogWarning("WARNING! StateMachine<" + m_Parent.GetType().Name + ">.RevertToPreviousState(): state stack is empty. Current state: " + m_CurrentState.GetType().Name + ".");
				}
			}
			else
			{
				UnityEngine.Debug.LogWarning("WARNING! StateMachine<" + m_Parent.GetType().Name + ">.RevertToPreviousState(): stack is not enabled. Please enable before calling this method.");
			}
		}

		public bool HasHistory()
		{
			return m_StateStack != null && m_StateStack.Count > 0;
		}

		public void ClearHistory()
		{
			if(m_TrackStates)
			{
				m_StateStack.Clear();
			}
			else
			{
				UnityEngine.Debug.LogWarning("WARNING! StateMachine<" + m_Parent.GetType().Name + ">.ClearHistory(): stack is not enabled. No changes.");
			}
		}

		public bool IsInState(TEnumState enumState)
		{
			if (m_CurrentState == null)
				return false;

			return m_CurrentState == m_StatesDictionary[enumState];
		}

		public bool IsInState(System.Type stateType)
		{
			if (m_CurrentState == null)
				return false;

			return m_CurrentState.GetType() == stateType;
		}
	}
}