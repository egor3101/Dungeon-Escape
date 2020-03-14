using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamageable
{
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base.health;
    }
    public override void Movment()
    {
        base.Movment();

    }
    public void Damage()
    {
        if (isDeaed == true)
        {
            return;
        }
        Health--;
        anim.SetTrigger("Hit");
        isHit = true;
        anim.SetBool("InCombat", true);

        if (Health < 1)
        {
            isDeaed = true;
            anim.SetTrigger("Death");
            GameObject diamomd = Instantiate(diamondPerfab, transform.position, Quaternion.identity) as GameObject;
            diamomd.GetComponent<Diamond>().gems = base.gems;
        }
    }
}
