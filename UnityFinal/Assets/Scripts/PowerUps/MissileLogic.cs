using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MissileLogic : MonoBehaviour
{
    const float EffectTime = 15.0f;
    float m_effectTimer;
    
    [SerializeField]
    GameObject DamageSource = null;
    GameObject m_target = null;

    NavMeshAgent m_navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        m_effectTimer = 0;        
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_navMeshAgent.enabled = false;
        
    }
    AudioSource a;
    
    // Update is called once per frame
    void FixedUpdate()
    {                
        m_effectTimer += Time.deltaTime;
        if (m_effectTimer > EffectTime) {            
            Destroy(gameObject);
        }
        if(m_target)
        {
            m_navMeshAgent.enabled = true;
            m_navMeshAgent.SetDestination(m_target.transform.position);
        }
        else
            m_navMeshAgent.enabled = false;

        if (GetComponentInChildren<DamageSourceLogic>() == null) {
            Destroy(gameObject);
        }
    }
    public void setSourcePlayer(PlayerID playerId) {
        DamageSource.GetComponent<DamageSourceLogic>().InitDamageSourceInfo(playerId, 1);
    }

    public void setTarget(GameObject target)
    {
        m_target = target;
        DamageSource.GetComponent<DamageSourceLogic>().setMissile();
        /* 
        NavMeshHit closestHit;
        if( NavMesh.SamplePosition(  transform.position, out closestHit, 500, 1 ) ){
            transform.position = closestHit.position;
            //gameObject.AddComponent<NavMeshAgent>();
        }
        */
    }
}
