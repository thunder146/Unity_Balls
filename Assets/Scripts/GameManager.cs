using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject BallPrefab;

    private TextMeshProUGUI _textBallCount;

    void Start()
    {
        _textBallCount = GameObject.Find("TextBallCount").GetComponent<TextMeshProUGUI>();

        const int V = -2;
        const int V1 = 2;

        for (int i = V; i < V1; i++)
        {
            for (int j = V; j < 10; j++)
            {
                StartCoroutine(AddBalls(i, j, new BallConfig()));
            }
        }

        //StartCoroutine(AddBalls(-10, 10, new BallConfig()));
        //StartCoroutine(AddBalls(-9, 9, new BallConfig()));
        //StartCoroutine(AddBalls(-8, 8, new BallConfig()));
        //StartCoroutine(AddBalls(-7, 7, new BallConfig()));
        //StartCoroutine(AddBalls(-6, 6, new BallConfig()));
        //StartCoroutine(AddBalls(-5, 5, new BallConfig()));
        //StartCoroutine(AddBalls(-4, 4, new BallConfig()));
        //StartCoroutine(AddBalls(-3, 3, new BallConfig()));
        //StartCoroutine(AddBalls(-2, 2, new BallConfig()));
        //StartCoroutine(AddBalls(-1, 1, new BallConfig()));
        //StartCoroutine(AddBalls(0, 0, new BallConfig()));
        //StartCoroutine(AddBalls(1, 1, new BallConfig()));
        //StartCoroutine(AddBalls(2, 2, new BallConfig()));
        //StartCoroutine(AddBalls(3, 3, new BallConfig()));
        //StartCoroutine(AddBalls(4, 4, new BallConfig()));
        //StartCoroutine(AddBalls(5, 5, new BallConfig()));
        //StartCoroutine(AddBalls(6, 6, new BallConfig()));
        //StartCoroutine(AddBalls(7, 7, new BallConfig()));
        //StartCoroutine(AddBalls(8, 8, new BallConfig()));
        //StartCoroutine(AddBalls(9, 9, new BallConfig()));
        //StartCoroutine(AddBalls(10, 10, new BallConfig()));

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
        var posX = Random.Range(-0.1f, 0.1f) + posXOffset;
        var posY = 20;
        var posZ = Random.Range(-0.1f, 0.1f) + posZOffset;

        var ballCreated = Instantiate(BallPrefab, new Vector3(posX, posY, posZ), Quaternion.identity);
        var controller = ballCreated.GetComponent<BallController>();
        controller.Initialize();

        if (config.SelfDestroy)
            Destroy(ballCreated, config.SelfDestroySeconds);
    }


    void FixedUpdate()
    {
        UpdateBallCount();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void UpdateBallCount()
    {
        var balls = GameObject.FindGameObjectsWithTag("Ball");
        _textBallCount.text = $"Ball Count {balls.Length}";
    }
}

class BallConfig
{
    public bool SelfDestroy = true;
    public float SelfDestroySeconds = 60;
}
