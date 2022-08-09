
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Material RedMaterial;
    public Material YellowMaterial;
    public Material BlueMaterial;

    private float _minScale = 0.25f;
    private float _maxScale = 1.5f;
    private int _massScaleFactor = 4;
    private int _minVeloY = 0;
    private int _maxVeloY = 25;


    internal void Initialize()
    {
        var ballScaleRnd = Random.Range(_minScale, _maxScale);
        var ballScaleTransform = new Vector3(ballScaleRnd, ballScaleRnd, ballScaleRnd);

        var ballRb = gameObject.gameObject.GetComponent<Rigidbody>();

        SetScale(ballScaleTransform, gameObject);
        SetMass(ballScaleRnd, ballRb);
        SetColor(ballScaleRnd, gameObject);
        SetVelocity(ballRb);
    }

    private void SetVelocity(Rigidbody ballRb)
    {
        var veloX = ballRb.velocity.x;
        var veloY = Random.Range(_minVeloY, -_maxVeloY);
        var veloZ = ballRb.velocity.z;

        ballRb.velocity = new Vector3(veloX, veloY, veloZ);
    }

    private static void SetScale(Vector3 ballScaleTransform, GameObject ballCreated)
    {
        ballCreated.transform.localScale = ballScaleTransform;
    }

    private void SetColor(float ballScaleRnd, GameObject ballCreated)
    {
        var ballRenderer = ballCreated.GetComponent<Renderer>();

        if (ballScaleRnd < 0.88)
        {
            ballRenderer.material = YellowMaterial;
        }
        else if (ballScaleRnd < 1.25)
        {
            ballRenderer.material = RedMaterial;
        }
        else
        {
            ballRenderer.material = BlueMaterial;
        }
    }

    private void SetMass(float ballScaleRandom, Rigidbody ballRb)
    {
        ballRb.mass = ballScaleRandom * _massScaleFactor;
    }
}
