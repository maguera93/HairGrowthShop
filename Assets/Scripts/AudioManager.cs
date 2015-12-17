using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{

    public AudioClip[] wellDone;
    public AudioClip[] fail;
    public AudioClip button;
    public AudioClip wookieSound, squicy;

    // Use this for initialization
    void Start()
    {
        //arlogic = GetComponent<ArcadeLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        //myPitch = arlogic.pitchRate;
        //audioSor.pitch = arlogic.pitchRate;
    }

    // FUNCION PLAY: REPRODUCE UN SONIDO 
    public void Play(AudioClip audio, AudioSource audioSource, float volum)
    {

        // AGREGAMOS EL COMPONENTE AUDIOSOURCE AL GAMEOBJECT DATALOGIC
        //AudioSource audioSource = gameObject.AddComponent<AudioSource> ();
        // CARGAMOS EL CLIP
        audioSource.clip = audio;
        // PONEMOS EL VOLUMEN A TOPE
        audioSource.volume = volum;
        // REPRODUCIMOS EL SONIDO
        audioSource.Play();
        // DESTRUIMOS EL AUDIOSOURCE UNA VEZ ACABADO EL SONIDO
        Destroy(audioSource, audio.length);
    }

    public void PlayLoop(AudioClip audio, AudioSource audioSource, float volum)
    {

        // AGREGAMOS EL COMPONENTE AUDIOSOURCE AL GAMEOBJECT DATALOGIC
        //AudioSource audioSource = gameObject.AddComponent<AudioSource> ();
        // CARGAMOS EL CLIP
        audioSource.clip = audio;
        audioSource.loop = true;
        // PONEMOS EL VOLUMEN A TOPE
        audioSource.volume = volum;
        // REPRODUCIMOS EL SONIDO
        audioSource.Play();

    }

    public void PlayGameOver(AudioClip audio, AudioSource audioSource, float volum)
    {

        // AGREGAMOS EL COMPONENTE AUDIOSOURCE AL GAMEOBJECT DATALOGIC
        //AudioSource audioSource = gameObject.AddComponent<AudioSource> ();
        // CARGAMOS EL CLIP
        audioSource.clip = audio;
        // PONEMOS EL VOLUMEN A TOPE
        audioSource.volume = volum;
        // REPRODUCIMOS EL SONIDO
        audioSource.Play();
        // DESTRUIMOS EL AUDIOSOURCE UNA VEZ ACABADO EL SONIDO
        //Destroy(audioSource, audio.length);
    }

    public void PlayButton()
    {

        // AGREGAMOS EL COMPONENTE AUDIOSOURCE AL GAMEOBJECT DATALOGIC
        AudioSource audioSource = gameObject.AddComponent<AudioSource> ();
        // CARGAMOS EL CLIP
        audioSource.clip = button;
        audioSource.pitch = Random.Range(1.0f, 4.0f);
        // PONEMOS EL VOLUMEN A TOPE
        audioSource.volume = 1.0f;
        // REPRODUCIMOS EL SONIDO
        audioSource.Play();
        // DESTRUIMOS EL AUDIOSOURCE UNA VEZ ACABADO EL SONIDO
        Destroy(audioSource, button.length);
    }
}
