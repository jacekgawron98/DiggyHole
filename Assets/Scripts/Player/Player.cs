using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxMagicTime = 0;
    private float remainnigMagicTime = 0;

    Rigidbody2D rigidBody;

    public MapGenerator mapGenerator;
    public GameObject dynamitePrefab;

    public bool hasMoved = false;
    public bool isPlantingDynamite = false;
    public bool hasFinished = false;
    public bool isDead = false;

    public CircleCollider2D finalCollider;

    public GameObject timeBar;

    public Cinemachine.CinemachineVirtualCamera playerCamera;

    public ParticleSystem reviveParticle;

    private void OnTriggerExit2D(Collider2D collision)
    {
        GeneratedTile tile = collision.gameObject.GetComponent<GeneratedTile>();
        if (tile != null)
        {
            tile.IsTarget = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        GeneratedTile tile = collision.gameObject.GetComponent<GeneratedTile>();
        if (tile != null)
        {
            tile.IsTarget = true;
            tile.player = this;
        }
    }


    public void Move(float x, float y)
    {
        hasMoved = true;
        Vector2 newPosition = new Vector2(x, y);
        rigidBody.MovePosition(newPosition);
    }

    public void Death()
    {
        isDead = true;
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.9f);
        SessionManager.instance.Replay();
    }

    public void CollectGold()
    {
        if (PlayerData.gold < 9999)
        {
            PlayerData.gold++;
        }
    }

    public void PlantDynamite(Vector2 position)
    {
        hasMoved = true;
        if (PlayerData.dynamiteAmount > 0)
        {
            PlayerData.dynamiteAmount--;
            Instantiate(dynamitePrefab, position, Quaternion.identity);
        }
    }

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        maxMagicTime = PlayerData.maxTime;
        remainnigMagicTime = maxMagicTime;
        if (!SessionManager.isBeginning)
        {
            Instantiate(reviveParticle, transform.position, Quaternion.identity);
        }
    }

    private void Update()
    {
        if (!hasFinished)
        {
            if (!hasMoved)
            {
                remainnigMagicTime = PlayerData.maxTime;
            }

            if (Input.GetMouseButtonDown(1) && remainnigMagicTime > 0)
            {
                mapGenerator.DrawVisible();
                hasMoved = true;
                timeBar.SetActive(true);
            }
            if (Input.GetMouseButton(1) && remainnigMagicTime > 0)
            {
                remainnigMagicTime -= Time.deltaTime;
                timeBar.gameObject.SetActive(true);
                timeBar.GetComponent<TimeBar>().SetSize(remainnigMagicTime / PlayerData.maxTime);
                playerCamera.m_Lens.OrthographicSize = Mathf.MoveTowards(playerCamera.m_Lens.OrthographicSize, 4, 0.04f);
                if (remainnigMagicTime <= 0)
                {
                    mapGenerator.DrawInvisible();
                    timeBar.gameObject.SetActive(false);
                }
            }
            else
            {
                playerCamera.m_Lens.OrthographicSize = Mathf.MoveTowards(playerCamera.m_Lens.OrthographicSize, 2, 0.1f);
            }
            if (Input.GetMouseButtonUp(1))
            {
                mapGenerator.DrawInvisible();
                timeBar.gameObject.SetActive(false);
            }

            if (Mathf.Round(transform.position.y * 10) / 10 == (-mapGenerator.yLimit + 0.5f))
            {
                Finish();
            }
        }
    }

    private void Finish()
    {
        hasFinished = true;
        finalCollider.enabled = true;
        rigidBody.gravityScale = 1;
    }
}
