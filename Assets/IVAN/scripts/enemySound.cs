using UnityEngine;
using UnityEngine.AI;

public class enemySound : MonoBehaviour
{
    public AudioSource auso;
    public AudioClip hurtSnd;
    public AudioClip meleeSnd;
    public AudioClip dashSnd;
    public AudioClip shootSnd;
    public AudioClip summonSnd;
    public AudioClip critSnd;
    public AudioClip selfDamageSnd;
    public AudioClip starSnd;
    public AudioClip walkSnd;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void hurt()
    {
        auso.PlayOneShot(hurtSnd);
    }

    // Update is called once per frame
    public void melee()
    {
        auso.PlayOneShot(meleeSnd);
    }

    public void dash()
    {
        auso.PlayOneShot(dashSnd);
    }

    public void shoot()
    {
        auso.PlayOneShot(shootSnd);
    }

    public void summon()
    {
        auso.PlayOneShot(summonSnd);
    }

    public void crit()
    {
        auso.PlayOneShot(critSnd);
    }
    public void selfdamage()
    {
        auso.PlayOneShot(selfDamageSnd);
    }
    public void star()
    {
        auso.PlayOneShot(starSnd);
    }

}
