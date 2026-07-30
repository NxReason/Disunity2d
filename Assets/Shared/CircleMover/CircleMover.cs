using System;
using UnityEngine;

namespace Sandbox2d.Shared
{
  public class CircleMover : MonoBehaviour
  {
    void Start()
    {

    }

    void Update()
    {
      float mult = 2;
      float x = (float)Math.Sin(Time.time) * mult;
      float y = (float)Math.Cos(Time.time) * mult;
      transform.position = new Vector3(x, y, 0);
    }
  }
}