using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _clips;
    private AudioSource _audio;
    [SerializeField] private string _name;
    private static MusicController _musicManager;
    
    void Start()
    {
        if (_musicManager != null && _musicManager != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _musicManager = this;
        DontDestroyOnLoad(this);
        
        _audio = GetComponent<AudioSource>();
        StartCoroutine(PlayingSong());
    }

    IEnumerator PlayingSong()
    {
        int index = Random.Range(0, _clips.Count);
        _audio.clip = _clips[index];
        _audio.Play();
        yield return new WaitForSeconds(_clips[index].length);
        StartCoroutine(PlayingSong());
    }
}
