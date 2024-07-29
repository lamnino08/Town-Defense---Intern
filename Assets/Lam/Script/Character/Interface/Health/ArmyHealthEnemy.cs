using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ArmyHealthEnemy : ArmyHealth
{
    protected override void Die()
    {
        base.Die();
        GameObject destroy = Instantiate(ConstructionData.instance.charcaterDestroy, transform.position, Quaternion.identity);
                GameManager.instance.destroyEffect.Add(destroy);
                GameObject gold = Instantiate(ConstructionData.instance.gold, transform.position+ new Vector3(0,0.3f,0), Quaternion.identity);
                GameManager.instance.gold.Add(gold);
                GameManager.instance.RemoveEnemay(gameObject);
                Destroy(gameObject);
    }
}
