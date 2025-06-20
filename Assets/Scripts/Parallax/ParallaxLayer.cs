using System;
using UnityEngine;

[Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform background;
    [SerializeField] private float parallaxMultiplier;
    [SerializeField] private float imageWidthOffset = 10;

    private float imageFullWidth;
    private float imageHalfWidth;

    public void CalculateImageWidth()
    {
        imageFullWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
        imageHalfWidth = imageFullWidth / 2;
    }

    public void Move(float distanceToMove)
    {
        //background.position = background.position + new Vector3(distanceToMove * parallaxMultiplier, 0);
        background.position += Vector3.right * distanceToMove * parallaxMultiplier;
    }

    public void LoopBackground(float cameraLeftEdge, float cameraRightEgde)
    {
        float imageLeftEdge = (background.position.x + imageHalfWidth) - imageWidthOffset;
        float imageRightEdge = (background.position.x - imageHalfWidth) + imageWidthOffset;

        if(imageRightEdge < cameraLeftEdge)
            background.position += Vector3.right * imageFullWidth;
        else if(imageLeftEdge > cameraRightEgde)
            background.position += Vector3.right * -imageFullWidth;
    }
}
