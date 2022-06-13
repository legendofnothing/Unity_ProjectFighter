using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAttack : MonoBehaviour
{
    public float detectRadius;

    #region Unity Methods
    void Start() {
        
    }
 
    void Update() {
        DetectPlayer();
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
    #endregion

    private bool DetectPlayer() {
        var circle = Physics2D.OverlapCircle(transform.position, detectRadius);

        if (circle.gameObject.layer == LayerMask.NameToLayer("Player")) return true;

        else return false;
    }
}