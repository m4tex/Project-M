using UnityEngine;
using UnityEngine.UI;

public class MagUIItem : MonoBehaviour
{
    public TMPro.TMP_Text IndexText;
    public Image MagIcon;

    public void Setup(int index, int magTypeIndex)
    {
        IndexText.text = index.ToString();
        
    }
}