using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trajectory : MonoBehaviour
{
    /*
     *THIS IS BORROWED FROM A PREVIOUS PROJECT AS A BASE
     */

    //Tutorials used:
    //https://www.youtube.com/watch?v=4VUmhuhkELk
    //https://www.youtube.com/watch?v=GLu1T5Y2SSc

    private Scene mainScene;
    private Scene physicsScene;

    private PlayerCast m_CastInfo;

    public GameObject objectsToSpawn;
    public LineRenderer lr;

    GameObject g;

    Vector3 ogpos;

    private void Start()
    {
        m_CastInfo = GetComponent<PlayerCast>();
        Physics.autoSimulation = false;
        mainScene = SceneManager.GetActiveScene();

        bool flag = false;

        //I know its obsolete but this does it better
        foreach (Scene s in SceneManager.GetAllScenes())
        {
            if (s == SceneManager.GetSceneByName("physicsScene"))
            {
                physicsScene = SceneManager.GetSceneByName("physicsScene");
                flag = true;
            }
        }
        if (!flag)
            physicsScene = SceneManager.CreateScene("PhysicsScene", new CreateSceneParameters(LocalPhysicsMode.Physics3D));

        PreparePhysicsScene();
    }

    private void FixedUpdate()
    {
        ogpos = m_CastInfo.GetTrajectoryInfo().transform.position;
        if (Input.GetMouseButton(1) && m_CastInfo.GetCurrentSpell() == PlayerCast.SpellType.LobShot && m_CastInfo.m_lscooldown < 0.0f)
        {
            lr.enabled = true;
            ShowTrajectory(m_CastInfo.getLobSpeed());
        }
        else
        {
            lr.enabled = false;
        }

        mainScene.GetPhysicsScene().Simulate(Time.fixedDeltaTime);
    }

    public void PreparePhysicsScene()
    {
        SceneManager.SetActiveScene(physicsScene);
        g = GameObject.Instantiate(objectsToSpawn);
        g.transform.name = "ReferenceItem";
        g.GetComponent<LobShot>().m_Particle.SetActive(false);
        g.GetComponent<LobShot>().m_ExplodeParticle.SetActive(false);
        Destroy(g.GetComponent<MeshRenderer>());
        Destroy(g.GetComponent<Trajectory>());
        SceneManager.SetActiveScene(mainScene);
    }

    public void ShowTrajectory(float _speed)
    {
        g.transform.rotation = objectsToSpawn.transform.rotation;
        g.GetComponent<Rigidbody>().useGravity = true;
        g.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 4.0f, 0.0f), ForceMode.Impulse);
        g.GetComponent<Rigidbody>().AddForce(transform.forward * _speed, ForceMode.Impulse);

        int steps = (int)(1f / Time.fixedDeltaTime);
        lr.positionCount = steps;
        for (int i = 0; i < steps; i++)
        {
            physicsScene.GetPhysicsScene().Simulate(Time.fixedDeltaTime);
            lr.SetPosition(i, g.transform.position);
        }

        g.GetComponent<Rigidbody>().velocity = Vector3.zero;
        g.transform.position = ogpos;
    }
}
