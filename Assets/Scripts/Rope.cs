using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D ropeRoot;
    public GameObject ropeNode;
    public GameObject locomotive;
    public float nodeInterval;
    public List<AudioEffect> spreadAudios = new List<AudioEffect>();
    public List<AudioEffect> foldAudios = new List<AudioEffect>();
    public List<AudioEffect> hardAudios = new List<AudioEffect>();
    public List<AudioEffect> softAudios = new List<AudioEffect>();

    private GameObject m_locomotive;
    private List<GameObject> m_nodes = new List<GameObject>();
    private float m_nextSpreadTime;
    private float m_nextFoldTime;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.Instance.Pause && Input.GetMouseButtonDown(0))
        {
            AudioManager.Instance.PlayRandomAudio(spreadAudios);
        }
        if (!LevelManager.Instance.Pause && Input.GetMouseButtonDown(1))
        {
            if (m_nodes.Count > 1)
                AudioManager.Instance.PlayRandomAudio(foldAudios);
        }
        if (!LevelManager.Instance.Pause && Input.GetMouseButton(0))
        {
            if (Time.time > m_nextSpreadTime)
            {
                Spread();
                m_nextSpreadTime = Time.time + 0.1f;
            }
        }
        if (!LevelManager.Instance.Pause && Input.GetMouseButton(1))
        {
            if (Time.time > m_nextFoldTime)
            {
                Fold();
                m_nextFoldTime = Time.time + 0.1f;
            }
        }
        bool lastFrameHard = LevelManager.Instance.Hard;
        LevelManager.Instance.Hard = false;
        if (!LevelManager.Instance.Pause && Input.GetKey(KeyCode.H))
        {
            LevelManager.Instance.Hard = true;
        }
        if (LevelManager.Instance.Hard != lastFrameHard)
        {
            if (LevelManager.Instance.Hard)
            {
                AudioManager.Instance.PlayRandomAudio(hardAudios);
            }
            else
            {
                AudioManager.Instance.PlayRandomAudio(softAudios);
            }
        }
    }

    public void Spread()
    {
        m_nodes.Add(GetOneNode());

        if (!m_locomotive)
        {
            m_locomotive = Instantiate(locomotive, ropeRoot.position, Quaternion.identity, transform);
            DistanceJoint2D comp = m_locomotive.GetComponent<DistanceJoint2D>();
            comp.connectedBody = m_nodes[0].GetComponent<Rigidbody2D>();
            comp.distance = 0.5f;
            Locomotive locomotiveComp = m_locomotive.GetComponent<Locomotive>();
        }
    }

    public void Fold()
    {
        if (m_nodes.Count > 1)
        {
            Destroy(m_nodes[m_nodes.Count - 1]);
            m_nodes.RemoveAt(m_nodes.Count - 1);
            m_nodes[m_nodes.Count - 1].GetComponent<DistanceJoint2D>().connectedBody = ropeRoot;
        }
    }

    private GameObject GetOneNode()
    {
        GameObject newNode = Instantiate(ropeNode, ropeRoot.position, Quaternion.identity, transform);
        DistanceJoint2D comp = newNode.GetComponent<DistanceJoint2D>();
        comp.connectedBody = ropeRoot;
        comp.distance = nodeInterval;
        RopeNode RopeNode = newNode.GetComponent<RopeNode>();
        RopeNode.followRoot = ropeRoot;
        RopeNode.maxDistance = nodeInterval;
        //HingeJoint2D comp = newNode.GetComponent<HingeJoint2D>();
        //comp.connectedBody = GetRoot();
        //comp.connectedAnchor = Vector2.zero;

        if (m_nodes.Count >= 1)
        {
            m_nodes[m_nodes.Count - 1].GetComponent<DistanceJoint2D>().connectedBody = newNode.GetComponent<Rigidbody2D>();
        }

        return newNode;
    }
}
