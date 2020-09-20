using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    int[,] levelMap =
    {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
    };

    // set the initial position
    private Vector3 position = new Vector3(-10.0f, 7.5f, 0.0f);

    // serialized so we can assign it from the inspector
    [SerializeField]
    private GameObject outsideCorner, outsideWall, insideCorner, insideWall, standardPellet, powerPellet, tJunctionPiece;

    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < 15; x++)
        {
            for (int y = 0; y < 14; y++)
            {
                switch (levelMap[x,y]) {
                    case 0:
                        position.x++;
                        checkNextLine();
                        break;
                    case 1:
                        if (x == 0 && levelMap[x + 1, y] == 2)
                        {
                            position.x += 0.5f;
                            Instantiate(outsideCorner, position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        }
                        else if (levelMap[x + 1, y] == 2)
                        {
                            Instantiate(outsideCorner, position, Quaternion.Euler(new Vector3(0, 0, -90)));
                        }
                        else if (levelMap[x, y + 1] == 2)
                        {
                            Instantiate(outsideCorner, position, Quaternion.Euler(new Vector3(0, 0, 90)));
                        }
                        else if (levelMap[x - 1, y] == 2)
                        {
                            Instantiate(outsideCorner, position, Quaternion.Euler(new Vector3(0, 0, 180)));
                        }
                        position.x++;
                        checkNextLine();
                        break;
                    case 2:
                        Debug.Log(x + " " + y);
                        if (y != 0 && levelMap[x, y - 1] == 1 && levelMap[x + 1, y] == 5)
                        {
                            Instantiate(outsideWall, position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        }
                        else if (y != 0 && levelMap[x, y - 1] == 1 && levelMap[x - 1, y] == 5)
                        {
                            Instantiate(outsideWall, position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        }
                        else if (y != 0 && levelMap[x, y - 1] == 2 && levelMap[x, y + 1] == 2)
                        {
                            Instantiate(outsideWall, position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        }
                        else if (y != 0 && levelMap[x, y - 1] == 2 && levelMap[x, y + 1] == 7)
                        {
                            Instantiate(outsideWall, position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        }
                        else if (y != 0 && levelMap[x, y - 1] == 2 && levelMap[x, y + 1] == 1)
                        {
                            Instantiate(outsideWall, position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        }
                        else if (x != 0 && levelMap[x - 1, y] == 2 && levelMap[x + 1, y] == 2)
                        {
                            Instantiate(outsideWall, position, Quaternion.Euler(new Vector3(0, 0, 90)));
                        }
                        else if (x != 0 && levelMap[x - 1, y] == 2 && levelMap[x + 1, y] == 1)
                        {
                            Instantiate(outsideWall, position, Quaternion.Euler(new Vector3(0, 0, 90)));
                        }
                        else if (levelMap[x - 1, y] == 1 && levelMap[x + 1, y] == 2)
                        {
                            Instantiate(outsideWall, position, Quaternion.Euler(new Vector3(0, 0, 90)));
                        }
                        else if (levelMap[x, y + 1] == 2)
                        {
                            Instantiate(outsideWall, position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        }
                        position.x++;
                        checkNextLine();
                        break;
                    case 3:
                        // to the right and below
                        if (x < 14 && y < 13 && levelMap[x, y + 1] == 4 && levelMap[x + 1, y] == 4)
                        {
                            Instantiate(insideCorner, position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        }
                        // to the right and above
                        else if (x != 0 && y < 13 && levelMap[x, y + 1] == 4 && levelMap[x - 1, y] == 4)
                        {
                            Instantiate(insideCorner, position, Quaternion.Euler(new Vector3(0, 0, 90)));
                        }
                        // to the left and above
                        else if (x != 0 && y != 0 && levelMap[x, y - 1] == 4 && levelMap[x - 1, y] == 4)
                        {
                            Instantiate(insideCorner, position, Quaternion.Euler(new Vector3(0, 0, 180)));
                        }
                        // to the left and below
                        else if (y != 0 && x < 14 && levelMap[x, y - 1] == 4 && levelMap[x + 1, y] == 4)
                        {
                            Instantiate(insideCorner, position, Quaternion.Euler(new Vector3(0, 0, 270)));
                        }
                        else if (x < 14 && y < 13 && levelMap[x, y + 1] == 4 && levelMap[x + 1, y] == 3)
                        {
                            Instantiate(insideCorner, position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        }
                        else if (x != 0 && y < 13 && levelMap[x, y + 1] == 4 && levelMap[x - 1, y] == 3)
                        {
                            Instantiate(insideCorner, position, Quaternion.Euler(new Vector3(0, 0, 90)));
                        }
                        else if (x != 0 && y != 0 && levelMap[x, y - 1] == 4 && levelMap[x - 1, y] == 3)
                        {
                            Instantiate(insideCorner, position, Quaternion.Euler(new Vector3(0, 0, 180)));
                        }
                        // to the left and below
                        else if (y != 0 && x < 14 && levelMap[x, y - 1] == 4 && levelMap[x + 1, y] == 3)
                        {
                            Instantiate(insideCorner, position, Quaternion.Euler(new Vector3(0, 0, 270)));
                        }
                        // to the left and below
                        else if (y != 0 && x < 14 && levelMap[x, y - 1] == 3 && levelMap[x + 1, y] == 4)
                        {
                            Instantiate(insideCorner, position, Quaternion.Euler(new Vector3(0, 0, 270)));
                        }
                        position.x++;
                        checkNextLine();
                        break;
                    case 4:
                        if (x != 0 && x < 14 && levelMap[x - 1, y] == 4 && levelMap[x + 1, y] == 4)
                        {
                            Instantiate(insideWall, position, Quaternion.Euler(new Vector3(0, 0, 90)));
                        }
                        else if (x != 0 && x < 14 && levelMap[x - 1, y] == 3 && levelMap[x + 1, y] == 3)
                        {
                            Instantiate(insideWall, position, Quaternion.Euler(new Vector3(0, 0, 90)));
                        }
                        else if (levelMap[x, y - 1] == 3 && levelMap[x, y + 1] == 4)
                        {
                            Instantiate(insideWall, position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        }
                        else if (y != 0 && y < 13 && levelMap[x, y - 1] == 4 && levelMap[x, y + 1] == 3)
                        {
                            Instantiate(insideWall, position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        }
                        else if (y != 0 && y < 13 && levelMap[x, y - 1] == 4 && levelMap[x, y + 1] == 4)
                        {
                            Instantiate(insideWall, position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        }
                        //else if (levelMap[x, y - 1] == 4)
                        //{
                        //    Instantiate(insideWall, position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        //}
                        position.x++;
                        checkNextLine();
                        break;
                    case 5:
                        Instantiate(standardPellet, position, Quaternion.Euler(new Vector3(0, 180, 0)));
                        position.x++;
                        checkNextLine();
                        break;
                    case 6:
                        Instantiate(powerPellet, position, Quaternion.Euler(new Vector3(0, 180, 0)));
                        position.x++;
                        checkNextLine();
                        break;
                    case 7:
                        Instantiate(tJunctionPiece, position, Quaternion.Euler(new Vector3(0, 180, 0)));
                        position.x++;
                        checkNextLine();
                        break;
                }
                //if (levelMap[x,y] == 1)
                //{
                //    Instantiate(outsideCorner, position, new Quaternion(0.0f, 180.0f, 0.0f, 0.0f));
                //    position.x++;
                //} 
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // check to see if we need have reached the end of a line
    void checkNextLine()
    {
        if (position.x > 4.0f)
        {
            position.x = -9.5f;
            position.y--;
        }
    }
}
