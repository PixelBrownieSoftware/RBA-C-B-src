using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileClass : MonoBehaviour {

    public int speed;
    Animator anim;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    public IEnumerator meteorStrike(Vector3 position) {
        while (6 < Mathf.Abs(position.x - transform.position.x)) {
            transform.position = new Vector3(transform.position.x, transform.position.y, 99);
            Vector2 currpos = new Vector2(position.x - transform.position.x, position.y - transform.position.y).normalized;
            transform.position += new Vector3(currpos.x * speed * 5, currpos.y * speed * 5, 9) * Time.deltaTime;
            yield return null;
        }
        Destroy(this.gameObject);
    }

    void PlaySoundEffect(AudioClip sound)
    {
        SoundManager.sfx.PlaySound(sound);
    }

    public void startFX(string type)
    {

    }
    
    public IEnumerator projectileShoot(float time)
    {
        bool isShoot = true;
        while (isShoot)
        {
            transform.Translate(Vector2.up * speed * 14 * Time.deltaTime);
            yield return null;
        }
        
    }

    void destroyObj() {
        Destroy(gameObject);
    }

}
