using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    public List<GeneratedTile> objectsList;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        objectsList = new List<GeneratedTile>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        Vector2 boxSize = new Vector2(1 + 2 * PlayerData.dynamiteRange, 1 + 2 * PlayerData.dynamiteRange);
        boxCollider.size = boxSize;
        StartCoroutine(Detonate());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tile")
        {
            objectsList.Add(collision.GetComponent<GeneratedTile>());
        }
    }

    IEnumerator Detonate()
    {
        yield return new WaitForSeconds(2);
        foreach (GeneratedTile o in objectsList)
        {
            o.SetHole();
        }
        Destroy(gameObject);
    }
}
