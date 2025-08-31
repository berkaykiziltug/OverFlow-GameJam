using System;
using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class GroundWinCollider : MonoBehaviour
{
   [SerializeField] private CinemachineCamera winCamera;
   [SerializeField] private GameObject winPanel;
   [SerializeField] private GameObject overflowTMP;
   [SerializeField] private float fadeInTime = 0.2f;
   [SerializeField] private float visibleTime = 1.5f;
   [SerializeField] private float fadeOutTime = 0.5f;
   [SerializeField] private Vector3 offset = new Vector3(0, 1f, 0);
   private bool hasWon = false;
   private void OnCollisionEnter2D(Collision2D other)
   {
      if (hasWon) return;
      if (other.gameObject.TryGetComponent(out LetterCollision letterCollision))
      {
         StartCoroutine(SpawnFloatingText("Overflow!",letterCollision.gameObject.transform.position ));
         hasWon = true;
         GameManager.canPlay = false;
         winCamera.Follow = letterCollision.gameObject.transform;
         winCamera.LookAt = letterCollision.gameObject.transform;
         winCamera.Priority = 120;
      }
   }
   private IEnumerator SpawnFloatingText(string text, Vector3 startPosition)
   {
      GameObject textObj = Instantiate(overflowTMP, startPosition, Quaternion.identity);
      TextMeshPro tmp = textObj.GetComponent<TextMeshPro>();
      tmp.text = text;

      Color baseColor = tmp.color;
      baseColor.a = 0f;
      tmp.color = baseColor;

      float t = 0f;
      Vector3 endPosition = startPosition + offset;

      
      while (t < fadeInTime)
      {
         t += Time.deltaTime;
         float alpha = Mathf.Lerp(0f, 1f, t / fadeInTime);
         tmp.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
         textObj.transform.position = Vector3.Lerp(startPosition, endPosition, t / fadeInTime);
         yield return null;
      }
      
      yield return new WaitForSeconds(visibleTime);

      t = 0f;
      Vector3 fadeOutStart = textObj.transform.position;
      Vector3 fadeOutEnd = fadeOutStart + new Vector3(0, 0.3f, 0);

      while (t < fadeOutTime)
      {
         t += Time.deltaTime;
         float alpha = Mathf.Lerp(1f, 0f, t / fadeOutTime);
         tmp.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
         textObj.transform.position = Vector3.Lerp(fadeOutStart, fadeOutEnd, t / fadeOutTime);
         yield return null;
      }
      Destroy(textObj);
      StartCoroutine(EnableWinPanel());
   }

   private IEnumerator EnableWinPanel()
   {
      yield return new WaitForSeconds(1.5f);
      winPanel.SetActive(true);
   }
}
