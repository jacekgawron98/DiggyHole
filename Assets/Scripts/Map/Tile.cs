using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName="Tiles")]
public class Tile : ScriptableObject
{
    public Sprite sprite;
    public float spawnChance;
}
