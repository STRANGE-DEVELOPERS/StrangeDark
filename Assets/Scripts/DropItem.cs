using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] public List<GameObject> dropList;
    [SerializeField] public List<float> chances;

    public static DropItem Instance { get; private set; }

    public DropItem()
    {
        Instance = this; 
    }

    public void SpawnItem(Vector3 position)
    {
        float rng = Random.Range(0, 100);
        Debug.Log(rng);
        float cummulativeChance = 0;

        GameObject rolledItem = null;
        for (int i = 0; i < dropList.Count; i++)
        {
            float chance = chances[i];
            cummulativeChance += chance;
            if (rng <= cummulativeChance)
            {
                rolledItem = dropList[i];
                break;
            }
        }

        if (rolledItem != null)
        {
            var drop_item = Instantiate(rolledItem);
            drop_item.transform.position = position;
        }
    }
}
