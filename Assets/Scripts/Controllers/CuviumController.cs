using UnityEngine;
using Cuvium.Commands;

namespace Cuvium.Core
{
    public abstract class CuviumController : MonoBehaviour
    {
        [SerializeField]
        protected Transform highlight;
        public Player Owner;

        public void Select()
        {
            highlight.gameObject.SetActive(true);
            Owner.SelectedObjects.Add(this);
        }

        public void Deselect()
        {
            highlight.gameObject.SetActive(false);
            Owner.SelectedObjects.Add(this);
        }

        protected void OnDisable()
        {
            Owner.SelectedObjects.Remove(this);
        }
    }
}

