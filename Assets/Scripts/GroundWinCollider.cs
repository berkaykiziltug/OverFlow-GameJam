using System;
using UnityEngine;

public class GroundWinCollider : MonoBehaviour
{
   private void OnCollisionEnter2D(Collision2D other)
   {
      if (other.gameObject.TryGetComponent(out LetterCollision letterCollision))
      {
         Debug.Log($"YOU WIN!");
      }
   }
}
