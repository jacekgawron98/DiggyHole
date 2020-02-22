using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedTile : MonoBehaviour
{
    public Tile tile;
    public Sprite holeSprite;

    public SpriteRenderer spriteRenderer;

    public Tile normalTile;
    public Tile goldTile;
    public Tile bombTile;

    public GameObject shadow;

    public ParticleSystem explosion;

    bool isTarget = false;
    public bool IsTarget
    {
        get { return isTarget; }
        set { isTarget = value; }
    }

    public bool isSky = false;

    public Player player;

    public void SetTile(Tile tile)
    {
        if (isSky)
        {
            this.tile = normalTile;
        }
        else
        {
            this.tile = tile;
        }
        spriteRenderer.sprite = tile.sprite;
    }

    private void Awake()
    {
        shadow = transform.GetChild(0).gameObject;
    }

    private void OnMouseOver()
    {
        if (isTarget)
        {
            spriteRenderer.color = Color.green;
            shadow.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            spriteRenderer.color = Color.red;
            shadow.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = Color.white;
        shadow.GetComponent<SpriteRenderer>().color = Color.white;
    }


    private void OnMouseUpAsButton()
    {
        if (isTarget && !player.hasFinished && !player.isDead)
        {
            if (player.isPlantingDynamite)
            {
                player.PlantDynamite(transform.position);
            }
            else
            {
                if (tile == bombTile)
                {
                    Instantiate(explosion,transform.position,Quaternion.identity);
                    player.Death();
                }
                else if (tile == goldTile)
                {
                    player.CollectGold();
                    SetHole();
                }
                else if (tile == normalTile)
                {
                    player.Move(transform.position.x, transform.position.y);
                    if (!isSky)
                    {
                        SetHole();
                    }
                }
            }
        }
    }

    public void ShowTile()
    {
        if (spriteRenderer.sprite != holeSprite)
            shadow.SetActive(false);
    }

    public void HideTile()
    {
        if (spriteRenderer.sprite != holeSprite)
            shadow.SetActive(true);
    }

    public void SetHole()
    {
        spriteRenderer.sprite = holeSprite;
        tile = normalTile;
        shadow.SetActive(false);
    }
}
