using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    [SerializeField]
    GameObject [] m_Players;
    Vector3 m_PlayersCenter;
    float m_PlayersDistence;

    float m_maxXDistence;
    float m_maxZDistence;

    Vector3 m_cameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        m_cameraPosition = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        m_Players = GameObject.FindGameObjectsWithTag("Player");

        UpdatePlayersPosition();

        m_cameraPosition.x = m_PlayersCenter.x;
        m_cameraPosition.y = Mathf.Clamp(m_PlayersDistence /3f + 10f, 35.0f, 1000.0f);
        m_cameraPosition.z = m_PlayersCenter.z - (m_PlayersDistence /3.5f + 5f);
        transform.SetPositionAndRotation(m_cameraPosition, Quaternion.identity);
        transform.LookAt(m_PlayersCenter);
    }

    void UpdatePlayersPosition()
    {
        if(m_Players.Length > 1)
        {
            m_PlayersCenter = m_Players[0].transform.position;
            float maxXValue, minXValue, maxYValue, minYValue, maxZValue, minZValue;
            maxXValue = m_Players[0].transform.position.x;
            minXValue = maxXValue;
            maxYValue = m_Players[0].transform.position.y;
            minYValue = maxYValue;
            maxZValue = m_Players[0].transform.position.z;
            minZValue = maxZValue;

            for(int i = 1; i < m_Players.Length; i++)
            {
                Vector3 position = m_Players[i].transform.position;
                if(position.x > maxXValue)
                {
                    maxXValue = position.x;
                }
                else if(position.x < minXValue)
                {
                    minXValue = position.x;
                }
                if(position.y > maxYValue)
                {
                    maxYValue = position.y;
                }
                else if(position.y < minYValue)
                {
                    minYValue = position.y;
                }
                if(position.z > maxZValue)
                {
                    maxZValue = position.z;
                }
                else if(position.z < minZValue)
                {
                    minZValue = position.z;
                }
            }
            m_PlayersCenter = new Vector3((maxXValue+minXValue)/2f, (maxYValue+minYValue)/2f, (maxZValue+minZValue)/2f);
            m_PlayersDistence = (maxXValue-minXValue) > ((maxZValue-minZValue)*2.2f) ? (maxXValue-minXValue) : ((maxZValue-minZValue)*2.2f);
        }
        else
        {
            m_PlayersCenter = m_Players[0].transform.position;
            m_PlayersDistence = 0;
        }
    }
}
