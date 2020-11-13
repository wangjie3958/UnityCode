using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemToPrefab",menuName ="ItemToPrefab")]
public class ItemToGameObject:ScriptableObject
{
    public Item item;
    public GameObject gameObject;
    public Vector3 position;

    public ItemToGameObject(Item item, GameObject gameObject, Vector3 position)
    {
        this.item = item;
        this.gameObject = gameObject;
        this.position = position;
    }
}
