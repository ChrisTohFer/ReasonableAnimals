using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Object of this class placed in scene will place all the level components appropriately to generate a level

public class OfficeArchitect : MonoBehaviour
{
    //References to office prefabs

    public GameObject[] FloorPiecePrefabs;
    public GameObject[] WallPiecePrefabs;
    public GameObject[] DeskPiecePrefabs;
    public GameObject[] WaterCoolePiecePrefabs;

    //Variables to set the dimensions of pieces

    public float PieceWidth = 5f;
    public float PieceLength = 5f;

    //Camera variables

    public float CameraAngle = 35f;
    public float CameraBaseHeight = 4;

    //
    public GameObject[] WorkerPrefabs;

    //
    public int OfficeWidthOverride = 0;
    public int OfficeLengthOverride = 0;

    //Static variables

    public static OfficeArchitect Singleton;
    public static int OfficeWidth = 3;
    public static int OfficeLength = 2;
    public static float DeskProportion = 0.8f; //How many floor segments are occupied by desks (as opposed to Water coolers etc)
    public static float WaterCoolerProportion = 0.3f;

    //Return office dimensions
    public static Vector2 Dimensions
    {
        get
        {
            float x = OfficeWidth * Singleton.PieceWidth;
            float y = OfficeLength * Singleton.PieceLength;
            return new Vector2(x, y);
        }
    }
    public static Vector2 CornerBL
    {
        get
        {
            float x = 0f;
            float y = 0f;
            return new Vector2(x, y);
        }
    }
    public static Vector2 CornerTR
    {
        get
        {
            float x = (OfficeWidth - 1) * Singleton.PieceWidth;
            float y = (OfficeLength - 1) * Singleton.PieceLength;
            return new Vector2(x, y);
        }
    }


    //Returns a random gameobject from a list of gameobjects
    GameObject GetRandomPiece(GameObject[] pieces)
    {
        if(pieces.Length != 0)
        {
            return pieces[Random.Range(0, pieces.Length)];
        }
        else
        {
            return null;
        }
    }

    //Randomly rearrange the elements of an array
    void ShuffleArray(GameObject[] arr)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int i = 0; i < arr.Length; i++)
        {
            GameObject temp = arr[i];
            int index = Random.Range(i, arr.Length);
            arr[i] = arr[index];
            arr[index] = temp;
        }
    }

    //Generate level from the chosen static parameters
    void GenerateLevel()
    {
        bool overriden = false;
        if (OfficeWidthOverride == 0)
        {
            OfficeWidthOverride = OfficeWidth;
        }
        else
            overriden = true;
        if (OfficeLengthOverride == 0)
        {
            OfficeLengthOverride = OfficeLength;
        }
        else
            overriden = true;

        //Calculate number of each piece required
        int floorSpaces = OfficeWidthOverride * OfficeLengthOverride;
        GameObject[] levelObjects = new GameObject[floorSpaces];
        int numberOfDesks = Mathf.RoundToInt(DeskProportion * floorSpaces);
        int numberOfWaterCoolers = Mathf.RoundToInt(WaterCoolerProportion * floorSpaces);

        //Instantiate the pieces and randomize their order
        for(int i = 0; i < numberOfDesks; ++i)
        {
            levelObjects[i] = Instantiate(GetRandomPiece(DeskPiecePrefabs));
        }
        for(int i = numberOfDesks; i < numberOfWaterCoolers + numberOfDesks && i < levelObjects.Length; ++i)
        {
            levelObjects[i] = Instantiate(GetRandomPiece(WaterCoolePiecePrefabs));
        }
        ShuffleArray(levelObjects);

        //Place the pieces in the correct locations
        for(int j = 0; j < OfficeWidthOverride; ++j)
        {
            Vector3 position = Vector3.zero;
            for(int i = 0; i < OfficeLengthOverride; ++i)
            {
                int objectIndex = j * OfficeLengthOverride + i;
                position = new Vector3(j * PieceWidth, 0f, i * PieceLength);
                GameObject levelObject = levelObjects[objectIndex];
                if (levelObject != null)
                {
                    levelObject.transform.position = position;
                    Desk desk = levelObject.GetComponent<Desk>();
                    if (desk != null)
                    {
                        levelObject.transform.rotation = Quaternion.Euler(0f, Random.Range(160f, 200f), 0f);
                        GameObject workerObject = Instantiate(GetRandomPiece(WorkerPrefabs), desk.WorkLocation.transform.position, Quaternion.identity);
                        Worker worker = workerObject.GetComponent<Worker>();
                        worker.OwnDesk = desk;
                    }
                }

                Instantiate(GetRandomPiece(FloorPiecePrefabs), position, Quaternion.identity);
                if (j == 0)
                {
                    Instantiate(GetRandomPiece(WallPiecePrefabs), position, Quaternion.Euler(new Vector3(0f, -90f, 0f)));
                }
                else if(j == OfficeWidthOverride - 1)
                {
                    Instantiate(GetRandomPiece(WallPiecePrefabs), position, Quaternion.Euler(new Vector3(0f, 90f, 0f)));
                }
            }

            Instantiate(GetRandomPiece(WallPiecePrefabs), position, Quaternion.identity);
        }


        Camera c = Camera.main;

        //Calculate height

        float height = CameraBaseHeight * Mathf.Sqrt(Mathf.Max(OfficeLength, OfficeWidth));

        //Calculate z position

        float offset = height * Mathf.Sin(CameraAngle);

        //
        if(!overriden)
        {
            c.transform.position = new Vector3((OfficeWidth - 1) * PieceWidth / 2f, height, (OfficeLength - 1) * PieceLength / 2f - offset);
            c.transform.rotation = Quaternion.Euler(90f - CameraAngle, 0f, 0f);
        }

        if (SharesTracker.Singleton != null)
            SharesTracker.Singleton.StartTracking();
    }

    //Load next level
    public void LoadNextLevel()
    {
        ++OfficeWidth;
        ++OfficeLength;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Unity Callbacks

    private void Start()
    {
        Singleton = this;
        GenerateLevel();
    }

}
