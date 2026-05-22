using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathHandler : MonoBehaviour
{
    public BezierSpineMulti bezierSpineMulti;

    [SerializeField] ParticleSystem deathParticle; 

    void Update()
    {
        //If cable is cut
        if (bezierSpineMulti._isBroken)
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        if (deathParticle != null) {
            deathParticle.Play();
        }
        yield return new WaitForSeconds(0.2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
