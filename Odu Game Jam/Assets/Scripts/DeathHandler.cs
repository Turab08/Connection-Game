using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathHandler : MonoBehaviour
{
    public BezierSpineMulti bezierSpineMulti;
    public Transform playerPosition;
    public BoxCollider2D playerCollision;

    [SerializeField] ParticleSystem deathParticle; 

    private bool isAlive = true;
    void Update()
    {
        //If cable is cut
        if (bezierSpineMulti._isBroken && isAlive)
        {
            StartCoroutine(Death());
            isAlive = false;
        }
    }

    IEnumerator Death()
    {
        if (deathParticle != null) {
            ParticleSystem particle = Instantiate(deathParticle, new Vector2(playerPosition.position.x, playerPosition.position.y), Quaternion.identity);
            Destroy(particle, 0.5f);
            playerCollision.enabled = false;
        }
        yield return new WaitForSeconds(0.5f);

        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
