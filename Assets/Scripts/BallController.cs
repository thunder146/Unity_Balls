
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Material RedMaterial;
    public Material YellowMaterial;
    public Material BlueMaterial;

    private const float MinScale = 0.25f;
    private const float MaxScale = 1.5f;
    private const int MassScaleFactor = 4;


    internal void Initialize()
    {
        var ballScaleRnd = Random.Range(MinScale, MaxScale);
        var ballScaleTransform = new Vector3(ballScaleRnd, ballScaleRnd, ballScaleRnd);

        SetScale(ballScaleTransform, gameObject);
        SetMass(ballScaleRnd, gameObject);
        SetColor(ballScaleRnd, gameObject);
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

    private static void SetMass(float ballScaleRandom, GameObject ballCreated)
    {
        var rb = ballCreated.gameObject.GetComponent<Rigidbody>();
        rb.mass = ballScaleRandom * MassScaleFactor;
    }
}
