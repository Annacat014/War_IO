using LearnGame.Shooting;
using System;
using UnityEngine;

namespace LearnGame.PickUp
{
    public abstract class PickUpItem : MonoBehaviour
    {
       public event Action <PickUpItem> OnPickeUp;

        public virtual void PickUp(BaseCharacter character)
        {
            /*if (OnPickUp != null)
                OnPickUp(this);*/
            OnPickeUp?.Invoke(this);
        }
    }
}