using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public GameObject acidEffectPerfab;
    public int Health { get; set; }
    public override void Init()
    {
        base.Init();

        Health = base.health;
    }

    public override void Update()
    {
         
    }
    public void Damage()
    {
        if (isDeaed == true)
        {
            return;
        }
        Health--;
        if (Health < 1)
        {
            isDeaed = true;
            anim.SetTrigger("Death");
            GameObject diamomd = Instantiate(diamondPerfab, transform.position, Quaternion.identity) as GameObject;
            diamomd.GetComponent<Diamond>().gems = base.gems;
        }
    }

    public override void Movment()
    {
        base.Movment();
    }
    public void Attack()
    {
        Instantiate(acidEffectPerfab, transform.position, Quaternion.identity);
    }
}
