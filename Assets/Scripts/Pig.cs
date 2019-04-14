﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour {

	public float maxSpeed = 10;
	public float minSpeed = 5;
	private SpriteRenderer render;
	public Sprite hurt;
	public GameObject boom;
	public GameObject score;
    public bool isPig;

    public AudioClip hurtClip;
    public AudioClip dead;
    public AudioClip birdCollision;

	private void Awake(){
		render = GetComponent<SpriteRenderer> ();
	}

	private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Player")
        {
            AudioPlay(birdCollision);
            collision.transform.GetComponent<Bird>().Hurt();
        }
		if (collision.relativeVelocity.magnitude > maxSpeed) {
            AudioPlay(dead);
			Dead ();
		} else if (collision.relativeVelocity.magnitude > minSpeed) {
            AudioPlay(hurtClip);
			render.sprite = hurt;
		}
	}

	public void Dead(){
		Destroy (gameObject);
		Instantiate (boom, transform.position, Quaternion.identity);
		GameObject _score = Instantiate (score, transform.position + new Vector3 (0, 1, 0), Quaternion.identity);
        GameManager._instant.pigs.Remove(this);
		Destroy (_score, 1.5f); 
	}

    private void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

}
