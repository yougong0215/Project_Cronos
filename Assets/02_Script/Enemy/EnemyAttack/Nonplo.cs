using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nonplo : PoolAble
{
    bool Damaged;
    // Start is called before the first frame update
    [SerializeField] List<ParticleSystem> pi;
    float particlelife = 0;

    private void OnEnable()
    {
        particlelife = 1;
        Damaged = false;
        transform.position = new Vector3(1000, 1000);
    }
    Vector3 pos;
    public void posisio(Vector3 po)
    {
        pos = po;
    }
    // Update is called once per frame
    void Update()
    {
        particlelife -= Time.deltaTime;
        if(particlelife < 0.3f)
        {
            Damaged = true;
            transform.position = pos;
        }
        
        if(particlelife <= 0)
        {
            PoolManager.Instance.Push(this);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Damaged == true)
        {
            if (collision.gameObject.GetComponent<PlayerHPMaster>())
                collision.gameObject.GetComponent<PlayerHPMaster>().GetDamage(1);
        }
    }
}
