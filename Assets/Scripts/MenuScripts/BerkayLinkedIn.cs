using UnityEngine;

public class BerkayLinkedIn : MonoBehaviour
{
   private string url = "https://www.linkedin.com/in/berkaykiziltug/";
   
   public void OpenProfile()
   {
      if (!string.IsNullOrEmpty(url))
      {
         Application.OpenURL(url);
      }
      else
      {
         Debug.LogWarning("URL is not set!");
      }
   }
}
