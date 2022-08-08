using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const float MinScale = 0.1f;
    private const float MaxScale = 1.5f;
    private const int MassScaleFactor = 4;
    public GameObject BallPrefab;

    // TODO Move to Ball Controller
    public Material RedMaterial;
    public Material YellowMaterial;
    public Material BlueMaterial;

    private TextMeshProUGUI textBallCount;

    // Start is called before the first frame update
    void Start()
    {
        textBallCount = GameObject.Find("TextBallCount").GetComponent<TextMeshProUGUI>();

        StartCoroutine(AddBalls(-1, -1, new BallConfig()));
        StartCoroutine(AddBalls(-1, 0, new BallConfig()));
        StartCoroutine(AddBalls(0, -1, new BallConfig()));
        StartCoroutine(AddBalls(1, 1, new BallConfig()));

        //AddBall(0, 0, new BallConfig() { SelfDestroy = false });
    }

    IEnumerator AddBalls(float posXOffset, float posZOffset, BallConfig config)
    {
        while (true)
        {
            AddBall(posXOffset, posZOffset, config);

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void AddBall(float posXOffset, float posZOffset, BallConfig config)
    {
        var rndX = Random.Range(-0.1f, 0.1f) + posXOffset;
        var rndZ = Random.Range(-0.1f, 0.1f) + posZOffset;

        var ballScaleRnd = Random.Range(MinScale, MaxScale);
        var ballScaleTransform = new Vector3(ballScaleRnd, ballScaleRnd, ballScaleRnd);

        var ballCreated = Instantiate(BallPrefab, new Vector3(rndX, 10, rndZ), Quaternion.identity);
        SetScale(ballScaleTransform, ballCreated);
        SetMass(ballScaleRnd, ballCreated);
        SetColor(ballScaleRnd, ballCreated);

        if (config.SelfDestroy)
            Destroy(ballCreated, config.SelfDestroySeconds);
    }

    private static void SetScale(Vector3 ballScaleTransform, GameObject ballCreated)
    {
        ballCreated.transform.localScale = ballScaleTransform;
    }

    private void SetColor(float ballScaleRnd, GameObject ballCreated)
    {
        var ballRenderer = ballCreated.GetComponent<Renderer>();

        if (ballScaleRnd < 0.66)
        {
            ballRenderer.material = YellowMaterial;
        }
        else if (ballScaleRnd < 1.11)
        {
            ballRenderer.material = RedMaterial;
        }
        else
        {
            ballRenderer.material = BlueMaterial;
        }
    }

    private static void SetMass(float ballScaleRandom, GameObject ballCreated)
    {
        var rb = ballCreated.gameObject.GetComponent<Rigidbody>();
        rb.mass = ballScaleRandom * MassScaleFactor;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var balls = GameObject.FindGameObjectsWithTag("Ball");
        //var balls = this.gameObject.GetComponentsInParent(typeof(BallController));
        textBallCount.text = $"Ball Count {balls.Length}";

    }
}

class BallConfig
{
    public bool SelfDestroy = true;
    public float SelfDestroySeconds = 10;

    public BallConfig()
    {
    }
}
