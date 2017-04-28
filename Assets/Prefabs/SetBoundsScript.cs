using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoundsScript : MonoBehaviour {

    public List<Transform> playersList;
    public Vector3 maximalDimensions;
    public float maximalCameraHeight;
    public bool restrictMovement;

    private int numberOfPlayersAssigned = 0;
    private const int maxNumberOfPlayers = 4;

    //Transform: - position represents the center of the bounding box
    private Vector3 boundingBoxScale;

    // Use this for initialization
    void Start () {
        numberOfPlayersAssigned = playersList.Count;
        boundingBoxScale = new Vector3();
    }

    // Update is called once per frame
    void Update ()
    {
        
        if (restrictMovement)
        {
            RestrictPositionOfObjects(ref playersList);
        }
       
	}

    private void LateUpdate()
    {
        Vector3 newPosition = transform.position;
        Vector3 newDimension = transform.localScale;

        if (ComputeBoundingBox(ref newPosition, ref newDimension, playersList))
        {
            transform.position = newPosition;
            newDimension.x = (newDimension.x > maximalDimensions.x) ? maximalDimensions.x : newDimension.x;
            newDimension.y = (newDimension.y > maximalDimensions.y) ? maximalDimensions.y : newDimension.y;
            newDimension.z = (newDimension.z > maximalDimensions.z) ? maximalDimensions.z : newDimension.z;
            boundingBoxScale = newDimension;

        }
    }

    public bool AddPlayer(Transform newPlayer)
    {
        if (numberOfPlayersAssigned < maxNumberOfPlayers)
        {
            playersList.Add(newPlayer);
            numberOfPlayersAssigned++;
            return true;
        } else
        {
            return false;
        }
    }

    public bool RemovePlayer(Transform playerObject)
    {
        bool result = playersList.Remove(playerObject);
        numberOfPlayersAssigned = playersList.Count;
        return result;
    }

    /// <summary>
    /// This function computes the height of the camera.
    /// </summary>
    /// <returns></returns>
    public float GetCameraDistance()
    {
        float result;

        result = Mathf.Max(boundingBoxScale.x,boundingBoxScale.z) / Mathf.Tan(Mathf.Deg2Rad * 50f);
        result = (result > maximalCameraHeight) ? maximalCameraHeight : result;
       

        return result;
    }

    /// <summary>
    /// This function computes the bounding box of a given list of objects, 
    /// where the outpur is the center of the bounding box and the dimenstions in x,y,z direction.
    /// the function returns a statement which indicates whether there could a new box be computed.
    /// </summary>
    /// <param name="listOfObjects"></param>
   static bool ComputeBoundingBox(ref Vector3 position, ref Vector3 dimension, List<Transform> listOfObjects)
    {
        if (listOfObjects.Count != 0)
        {
            float   minX = position.x,
                    maxX = position.x,
                    minY = position.y,
                    maxY = position.y,
                    minZ = position.z,
                    maxZ = position.z;

            foreach (Transform player in listOfObjects)
            {
                minX = (minX > player.position.x) ? player.position.x : minX;
                maxX = (maxX < player.position.x) ? player.position.x : maxX;
                minY = (minY > player.position.y) ? player.position.y : minY;
                maxY = (maxY < player.position.y) ? player.position.y : maxY;
                minZ = (minZ > player.position.z) ? player.position.z : minZ;
                maxZ = (maxZ < player.position.z) ? player.position.z : maxZ;
            }

            position = new Vector3((minX + maxX) / 2, (minY + maxY) / 2, (minZ + maxZ) / 2);
            dimension = new Vector3(Mathf.Abs(maxX - minX), Mathf.Abs(maxY - minY), Mathf.Abs(maxZ - minZ));
            return true;

        } else
        {
            return false;
        }
        
    }

    void RestrictPositionOfObjects(ref List<Transform> listOfObjects)
    {
        Vector3 bbPos = this.transform.position;
        Vector3 bbBoxScaleHalf = maximalDimensions / 2f;
        foreach (Transform player in listOfObjects)
        {
            Vector3 pos = player.position;           
            float x = pos.x, y = pos.y, z = pos.z;

            x = (x > bbPos.x + bbBoxScaleHalf.x) ? bbPos.x + bbBoxScaleHalf.x : x;
            x = (x < bbPos.x - bbBoxScaleHalf.x) ? bbPos.x - bbBoxScaleHalf.x : x;

            y = (y > bbPos.y + bbBoxScaleHalf.y) ? bbPos.y + bbBoxScaleHalf.y : y;
            y = (y < bbPos.y - bbBoxScaleHalf.y) ? bbPos.y - bbBoxScaleHalf.y : y;

            z = (z > bbPos.z + bbBoxScaleHalf.z) ? bbPos.z + bbBoxScaleHalf.z : z;
            z = (z < bbPos.z - bbBoxScaleHalf.z) ? bbPos.z - bbBoxScaleHalf.z : z;

            pos.Set(x, y, z);
            player.position = pos;
        }
    }
}
