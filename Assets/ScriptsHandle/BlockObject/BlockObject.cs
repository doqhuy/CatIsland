using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockObject : MonoBehaviour
{
    public string RequiredToolName;
    SaveSystem SaveSystem = SaveSystem.Instance;
    private void Update()
    {
        Transform player = GameObject.Find("Player").transform;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < 2f && (Input.GetKeyDown(KeyCode.J) || ActionButton.Instance.IsPress))
        {
            ActionButton.Instance.ResetIsPress();
            if(BreakObject())
            {
                Destroy(gameObject);
            }    
        }
    }

    bool BreakObject()
    {
        for(int i = 0; i < SaveSystem.saveLoad.inventory.KeyItems.Count; i++) 
        {
            if (SaveSystem.saveLoad.inventory.KeyItems[i].Item.Name == RequiredToolName) return true;
        }
        return false;
    }    
}
