using UnityEngine;

namespace Sandbox2d.HelloWorld
{
  public class Player : MonoBehaviour
  {
    void Start()
    {
      Debug.Log("Hello");
    }

    void Update()
    {
      transform.position += Vector3.right * Time.deltaTime;
    }
  }
}