using System;
using UnityEngine;
using System.Collections;
using MoreMountains.Feedbacks;

public class Letter : MonoBehaviour
{
   [SerializeField] private MMF_Player feedbackPlayer;
   


   private void Awake()
   {
   }

   private void Start()
   {
      
   }

   private void OnDestroy()
   {
      
   }

   public void DestroySelf()
   {
      Destroy(gameObject);
   }
   
}
