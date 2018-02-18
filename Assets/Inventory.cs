using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IHasChanged {
    [SerializeField] Transform slots;
    [SerializeField] Text inventoryText;
    public string s;
    public int count = 0;
    public void HasChanged()
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        builder.Append("-");
        foreach (Transform slotTransform in slots){
            GameObject item = slotTransform.GetComponent<slot>().item;
            if (item)
            {
                if ((item.name.Equals("ans1")||item.name.Equals("ans2")||item.name.Equals("ans3"))){
                    count = count + 1;
                    s = builder.ToString();
                    builder.Replace(s,"Good Job");
                }
               
                else{
                    s = builder.ToString();
                    builder.Replace(s,"Try Again");
                }

            }
        }
        inventoryText.text = builder.ToString();
    }

    private void Start()
    {
        HasChanged();
    }

}

namespace UnityEngine.EventSystems{
    public interface IHasChanged : IEventSystemHandler{
        void HasChanged();
    }
}