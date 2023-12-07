using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player playerPrefabs;
    public Platform platformPrefabs;
    public float minSpawnX;
    public float maxSpawnX;
    public float minSpawnY;
    public float maxSpawnY;
    
    public Camcontroller MainCam;
    public float powerBarUp;

    Player m_player;
    int m_score;
    bool m_isGameStarted;

    public bool IsGameStarted { get => m_isGameStarted; }
    public override void Awake()
    {
        MakeSingleton(false);
    }

    public override void Start()
    {
        base.Start();

        GameGUIManager.Ins.UpdateScoreCountingText(m_score);
        GameGUIManager.Ins.UpdatePowerBar(0, 1);

        AudioController.Ins.PlayBackgroundMusic();
    }

    public void Playgame()
    {
        StartCoroutine(PlatformInit());

        GameGUIManager.Ins.ShowGameGui(true);
    }

    IEnumerator PlatformInit()
    {
        Platform platformClone = null;

        if(platformPrefabs)
        {
            platformClone = Instantiate(platformPrefabs, new Vector2(0, Random.Range(minSpawnY, maxSpawnY)), Quaternion.identity);
            platformClone.id = platformClone.gameObject.GetInstanceID();

        }
        yield return new WaitForSeconds(0.5f);
        if(playerPrefabs)
        {
            m_player = Instantiate(playerPrefabs, Vector3.zero, Quaternion.identity);
            m_player.lastPlatformId = platformClone.id;
        }    

        if(platformPrefabs)
        {
            float SpawnX = m_player.transform.position.x + minSpawnX;
            float spawnY = Random.Range(minSpawnY, maxSpawnY);

            Platform platformClone2 = Instantiate(platformPrefabs, new Vector2(SpawnX, spawnY), Quaternion.identity);
            platformClone2.id = platformClone2.gameObject.GetInstanceID();
        }

        yield return new WaitForSeconds(0.5f);
        m_isGameStarted = true;

    }    

    public void CreatePlatform()
    {
        if (!platformPrefabs || !m_player) return;

        float SpawnX = Random.Range(m_player.transform.position.x + minSpawnX, m_player.transform.position.x + maxSpawnX);
        float SpawnY = Random.Range(minSpawnY, maxSpawnY);

        Platform platformClone = Instantiate(platformPrefabs, new Vector2(SpawnX, SpawnY), Quaternion.identity);
        platformClone.id = platformClone.gameObject.GetInstanceID();
    }    
    public void CreatePlatformAndLerp(float PlayerXpos)
    {
        if (MainCam)
            MainCam.LerpTrigger(PlayerXpos + minSpawnX);

        CreatePlatform();
    }    

    public void AddScore()
    {
        m_score++;
        Prefs.bestScore = m_score;
        GameGUIManager.Ins.UpdateScoreCountingText(m_score);
        AudioController.Ins.PlaySound(AudioController.Ins.getScore);
    }    
}
