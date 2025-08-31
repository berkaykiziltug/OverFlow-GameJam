using System;
using UnityEngine;

public class GroundWinCollider : MonoBehaviour
{
   [SerializeField] private GameObject winPanel;
   private void OnCollisionEnter2D(Collision2D other)
   {
      if (other.gameObject.TryGetComponent(out LetterCollision letterCollision))
      {
         GameManager.canPlay = false;
         winPanel.gameObject.SetActive(true);
         Debug.Log($"YOU WIN!");
      }
   }
}
