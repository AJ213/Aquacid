using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    [SerializeField] AudioSource levelMusic = default;
    [SerializeField] AudioSource hallMusic = default;
    [SerializeField] AudioSource bossMusic = default;
    [SerializeField] GameObject hallMusicTrigger = default;
    [SerializeField] bool hallMusicTriggered = false;
    [SerializeField] GameObject bossMusicTrigger = default;
    [SerializeField] bool bossMusicTriggered = false;

    public void Start()
    {
        levelMusic.Play();
    }

    private void Update()
    {
        if(Vector2.Distance(this.transform.position, hallMusicTrigger.transform.position) < 5 && !hallMusicTriggered)
        {
            levelMusic.Stop();
            bossMusic.Stop();
            hallMusic.Play();
            hallMusicTriggered = true;
        }
        if (Vector2.Distance(this.transform.position, bossMusicTrigger.transform.position) < 5 && !bossMusicTriggered)
        {
            levelMusic.Stop();
            hallMusic.Stop();
            bossMusic.Play();
            bossMusicTriggered = true;
        }
    }
}
