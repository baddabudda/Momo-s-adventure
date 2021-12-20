using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class collisionDetector : MonoBehaviour
{
    private Animator _playerAnimation;

    public GameObject fallDetector;

    public static Vector3 _respawnPoint;

    public Text scoreText;

    public GameObject essences;
    void Start()
    {
        _respawnPoint = transform.position;
        _playerAnimation = GetComponent<Animator>();
        scoreText.text = "Score: " + Scoring.score;
    }

    void Update()
    {
        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Fall Detector") {
            transform.position = _respawnPoint;
        } 
        else if (collision.tag == "Checkpoint") {
            _respawnPoint = transform.position;
        }
        else if (collision.tag == "Spike") {
            StartCoroutine("WhileDead");
        }
        else if(collision.tag == "Essence") {
            collision.tag = "Untagged";
            Destroy(collision.gameObject);
            Scoring.score += 1;
            scoreText.text = "Score: " + Scoring.score;
        } else if(collision.tag == "Next Level" && essences.transform.childCount == 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            collisionDetector._respawnPoint = transform.position;
        }
    }

    IEnumerator WhileDead() {
        PlayerController._isAlive = false;
        _playerAnimation.SetBool("HasTouchedSpike", true);
        yield return new WaitForSeconds(1);
        _playerAnimation.SetBool("HasTouchedSpike", false);
        transform.position = _respawnPoint;
        PlayerController._isAlive = true;
    }
}
