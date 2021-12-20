using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalOpening : MonoBehaviour
{
    private Animator _portalAnimator;

    public GameObject essences;

    // Start is called before the first frame update
    void Start()
    {
        _portalAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (essences.transform.childCount == 0) {
            _portalAnimator.SetBool("IsScoreEquals64", true);
        }
    }

}
