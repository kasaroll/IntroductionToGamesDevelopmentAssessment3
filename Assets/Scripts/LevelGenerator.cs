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
                switch (levelMap[x, y]) {
                    case 0:
                        position.x++;
                        checkNextLine();
                        break;
                    case 1:
                        if (x == 0 && levelMap[x + 1, y] == 2) // first piece and outsideWall is below
                        {
                            position.x += 0.5f;
                            Instantiate(outsideCorner, position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        }
                        else if (levelMap[x, y + 1] == 2) // outsideWall is to the right
                        {
                            Instantiate(outsideCorner, position, Quaternion.Euler(new Vector3(0, 0, 90)));
                        }
                        else if (levelMap[x - 1, y] == 2) // outsideWall is above
                        {
                            Instantiate(outsideCorner, position, Quaternion.Euler(new Vector3(0, 0, 180)));
                        }
                        else if (levelMap[x + 1, y] == 2) // outsideWall below
                        {
                            Instantiate(outsideCorner, position, Quaternion.Euler(new Vector3(0, 0, 270)));
                        }
                        position.x++;
                        checkNextLine();
                        break;
                    case 2:
                        if (y != 0 && levelMap[x, y - 1] == 1 && levelMap[x + 1, y] == 5 || // outsideCorner is to the left and standardPellet is below
                            y != 0 && levelMap[x, y - 1] == 1 && levelMap[x - 1, y] == 5 || // outsideCorner is to the left and standardPellet is above
                            y != 0 && levelMap[x, y - 1] == 2 && levelMap[x, y + 1] == 2 || // outsideWall is to the left and outsideWall is to the right
                            y != 0 && levelMap[x, y - 1] == 2 && levelMap[x, y + 1] == 7 || // outsideWall is to the left and TJunction is to the right
                            y != 0 && levelMap[x, y - 1] == 2 && levelMap[x, y + 1] == 1 || // outsideWall is to the left and outsideCorner is to the right
                                      levelMap[x, y + 1] == 2)                              // outsideWall is to the right   
                        {
                            Instantiate(outsideWall, position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        }
                        else if (x != 0 && levelMap[x - 1, y] == 2 && levelMap[x + 1, y] == 2 || // outsideWall is above and outsideWall is below
                                 x != 0 && levelMap[x - 1, y] == 2 && levelMap[x + 1, y] == 1 || // outsideWall is above and outsideCorner is below
                                           levelMap[x - 1, y] == 1 && levelMap[x + 1, y] == 2)   // outsideCorner is above and outsideWall is below
                        {
                            Instantiate(outsideWall, position, Quaternion.Euler(new Vector3(0, 0, 90)));
                        }
                        position.x++;
                        checkNextLine();
                        break;
                    case 3:
                        if (x < 14 && y < 13 && levelMap[x, y + 1] == 4 && levelMap[x + 1, y] == 4 ||                            // insideWall is to the right and insideWall is below
                            x < 14 && y < 13 && levelMap[x, y + 1] == 4 && levelMap[x + 1, y] == 3 && levelMap[x - 1, y] == 5 || // insideWall is to the right, insideCorner is below and standardPellet is above
                            x < 14 && y < 13 && levelMap[x, y + 1] == 3 && levelMap[x + 1, y] == 4)                              // insideCorner is to the right and insideWall is below
                        {
                            Instantiate(insideCorner, position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        }
                        else if (x != 0 && y < 13 && levelMap[x, y + 1] == 4 && levelMap[x - 1, y] == 4 || // insideWall is to the right and insideWall is above
                                 x != 0 && y < 13 && levelMap[x, y + 1] == 4 && levelMap[x - 1, y] == 3 || // insideWall is to the right and insideCorner is above
                                 x < 14 && y < 13 && levelMap[x, y + 1] == 3 && levelMap[x - 1, y] == 4)   // insideCorner is to the right and insideWall is above
                        {
                            Instantiate(insideCorner, position, Quaternion.Euler(new Vector3(0, 0, 90)));
                        }
                        else if (x != 0 && y != 0 && levelMap[x, y - 1] == 4 && levelMap[x - 1, y] == 4 && levelMap[x + 1, y] == 5 || // insideWall is to the left, insideWall is above and standardPellet is below
                                 x != 0 && y != 0 && levelMap[x, y - 1] == 4 && levelMap[x - 1, y] == 3 ||                            // insideWall is to the left and insideCorner is above
                                 x < 14 && y < 13 && levelMap[x, y - 1] == 3 && levelMap[x - 1, y] == 4)                              // insideCorner is to the left and insideWall is above
                        {
                            Instantiate(insideCorner, position, Quaternion.Euler(new Vector3(0, 0, 180)));
                        }
                        else if (y != 0 && x < 14 && levelMap[x, y - 1] == 4 && levelMap[x + 1, y] == 4 && levelMap[x - 1, y] == 4 || // insideWall is to the left, insideWall is below and insideWall is above
                                 y != 0 && x < 14 && levelMap[x, y - 1] == 4 && levelMap[x + 1, y] == 4 ||                            // insideWall is to the left, insideWall is below
                                 y != 0 && x < 14 && levelMap[x, y - 1] == 4 && levelMap[x + 1, y] == 3 ||                            // insideWall is to the left, insideCorner is below
                                 y != 0 && x < 14 && levelMap[x, y - 1] == 3 && levelMap[x + 1, y] == 4)                              // insideCorner is to the left, insideWall is below
                        {
                            Instantiate(insideCorner, position, Quaternion.Euler(new Vector3(0, 0, 270)));
                        }
                        position.x++;
                        checkNextLine();
                        break;
                    case 4:
                        if (levelMap[x, y - 1] == 3 && levelMap[x, y + 1] == 4 ||                     // insideCorner is to the left and insideWall is to the right
                            y != 0 && y < 13 && levelMap[x, y - 1] == 4 && levelMap[x, y + 1] == 3 || // insideWall is to the left and insideCorner is to the right
                            y != 0 && y < 13 && levelMap[x, y - 1] == 4 && levelMap[x, y + 1] == 4 || // insideWall is to the left and insideWall is to the right
                            levelMap[x, y - 1] == 4 && levelMap[x - 1, y] == 5 ||                     // insideWall is to the left and standardPellet is above
                            levelMap[x, y - 1] == 4 && levelMap[x - 1, y] == 0)                       // insideWall is to the left and empty is above
                        {
                            Instantiate(insideWall, position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        }
                        else if (x != 0 && x < 14 && levelMap[x - 1, y] == 4 && levelMap[x + 1, y] == 4 || // insideWall is above and insideWall is below
                                 x != 0 && x < 14 && levelMap[x - 1, y] == 3 && levelMap[x + 1, y] == 3 || // insideCorner is above and insideCorner is below
                                 x != 0 && x < 14 && levelMap[x + 1, y] == 3 && levelMap[x - 1, y] == 4 || // insideCorner is below and insideWall is above
                                 x != 0 && x < 14 && levelMap[x + 1, y] == 4 && levelMap[x - 1, y] == 3 || // insideWall is below and insideCorner is above
                                 x != 0 && x < 14 && levelMap[x + 1, y] == 4 && levelMap[x - 1, y] == 7 || // insideWall is below and TJunction is above
                                 x != 0 && levelMap[x - 1, y] == 4)                                        // insideWall is below
                        {
                            Instantiate(insideWall, position, Quaternion.Euler(new Vector3(0, 0, 90)));
                        }
                        position.x++;
                        checkNextLine();
                        break;
                    case 5:
                        Instantiate(standardPellet, position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        position.x++;
                        checkNextLine();
                        break;
                    case 6:
                        Instantiate(powerPellet, position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        position.x++;
                        checkNextLine();
                        break;
                    case 7:
                        Instantiate(tJunctionPiece, position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        position.x++;
                        checkNextLine();
                        break;
                }
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
