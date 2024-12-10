using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public ParticleSystem ps;
    public GameObject soundEffect;
    private float pitchVariance =0.3f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Bee>() != null)
        {
            GameManager.S.BallonDestroyed();
            var go = GameObject.Instantiate(ps.gameObject);
            go.transform.position = transform.position;
            Destroy(go, 1f);

            //sound effect 
           var sound = GameObject.Instantiate(soundEffect);
           sound.transform.position = transform.position;
            var source = sound.GetComponent<AudioSource>();
            source.pitch = source.pitch * (1f + UnityEngine.Random.Range(-pitchVariance / 2f, pitchVariance / 2f));
            Destroy(sound, 1f);

            //AudioManager.instance.Play("Pop");

            Destroy(gameObject);

        }
    }
}
