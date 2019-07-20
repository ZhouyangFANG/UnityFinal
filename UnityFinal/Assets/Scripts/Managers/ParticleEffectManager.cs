using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectManager : MonoBehaviour
{
    static public ParticleEffectManager Instance;
    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this);
            return;
        }
    }

    [SerializeField]
    GameObject PlayerDeathEffect = null;
    [SerializeField]
    GameObject PlayerPickUpHeartEffect = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playPlayerDeathEffect(Vector3 pos) {
        Instantiate(PlayerDeathEffect, pos, Quaternion.Euler(0,0,0));
    }

    public void playPlayerPickUpHeartEffect(Vector3 pos) {
        Instantiate(PlayerPickUpHeartEffect, pos, Quaternion.Euler(0,0,0));
    }
}
