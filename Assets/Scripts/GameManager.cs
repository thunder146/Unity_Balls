using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject BallPrefab;
    
    private TextMeshProUGUI _textBallCount;
    private int _ballCreationRadius = 4;
    private int MinBallCreationHeight = 10;
    private int MaxBallCreationHeight = 35;

    void Start()
    {
        _textBallCount = GameObject.Find("TextBallCount").GetComponent<TextMeshProUGUI>();
        StartAddBalls();
    }

    private void StartAddBalls()
    {
        for (int i = -_ballCreationRadius; i < _ballCreationRadius; i++)
        {
            for (int j = -_ballCreationRadius; j < _ballCreationRadius; j++)
            {
                StartCoroutine(AddBalls(i, j, new BallConfig()));
            }
        }
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
        var posX = Random.Range(-0.2f, 0.2f) + posXOffset;
        var posY = Random.Range(MinBallCreationHeight, MaxBallCreationHeight);//20;
        var posZ = Random.Range(-0.2f, 0.2f) + posZOffset;

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
