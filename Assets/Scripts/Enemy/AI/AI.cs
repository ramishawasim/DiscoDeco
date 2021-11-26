using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    public Transform player;
    State currentState;
    EnemyAudioManager enemyAudioManager;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.transform.Find("Cow").gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAudioManager = this.GetComponent<EnemyAudioManager>();

        currentState = new Start(this.gameObject, agent, anim, player, enemyAudioManager);
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
    }
}
