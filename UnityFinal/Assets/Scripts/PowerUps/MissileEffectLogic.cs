using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileEffectLogic : MonoBehaviour
{
    [SerializeField]
    GameObject m_missile = null;

    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<PowerUpLogic>().OnCastStart += Cast;
    }

    // Update is called once per frame
    void Update()
    {        
        
    }

    void Cast() {
        Debug.Log(transform.position);
        GameObject missile = GameObject.Instantiate(m_missile, transform.position, Quaternion.FromToRotation(Vector3.up, Vector3.up));
        
        GameObject [] m_targets = GameObject.FindGameObjectsWithTag("Player");
        System.Random rnd = new System.Random();
        int r =rnd.Next(m_targets.Length);
        while(m_targets[r] == transform.parent.gameObject){
             r =rnd.Next(m_targets.Length);
        }
        missile.GetComponent<MissileLogic>().SetTarget(m_targets[r]);
        Destroy(gameObject);
    }
}
