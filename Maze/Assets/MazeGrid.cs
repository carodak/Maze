using UnityEngine;
using System.Collections;
using System;

public class MazeGrid : MonoBehaviour
{

    //GRID FEATURES

    [SerializeField]
    private int colums = 8;

    private int currentRow = 0; //To know if we're at the first created line

    private Cell[] grid; //Memorize 1 row of 8 cells

    [SerializeField]
    private Vector3 size;

    private Vector3 posFirstCell; //position of the first cell of the line

    //Offset of the grid between cells
    [SerializeField]
    private float horizontalOffset;

    [SerializeField]
    private float verticalOffset;

    //Cell
    [SerializeField]
    private GameObject cube;

    //[SerializeField]
    //private GameObject projectile;

    //Create an ArrayList that says how the cells of the current row are connected
    private ArrayList arrCurrRow;

    //To generate random walls
    private System.Random gen;

    int nbProjectiles; //nb of projectiles that the player has.

    void Start()
    {

        arrCurrRow = new ArrayList();

        grid = new Cell[colums];

        gen = new System.Random();

        //Assign the offset
        horizontalOffset = 39.3f;
        verticalOffset = -39.95f;

        //Size of cells
        size = new Vector3(43.8f, 5.0f, 75.0f);

        //Position of the first cell
        posFirstCell = new Vector3(0.0f, 0.0f, 0.0f);

        Eller();

        nbProjectiles = 0;
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {


        }
    }

    //Create a row according to a saved position
    public void CreateRow()
    {

        Vector3 pos = new Vector3(0f, 0f, 0f);

        for (int i = 0; i < colums; i++)
        {

            //Create our cells
            Cell cell = new Cell(cube, size);
            grid[i] = cell;

            //All cells have the same aspect (maze)
            grid[i].SetCube(this.cube);

            //Place all the cells based on the position of the firstcell

            //If we're creating the first cellule, we define a position
            if (currentRow == 0 && i == 0)
            {
                posFirstCell = new Vector3(53f, 32f, -180f);
                pos = posFirstCell;
            }

            //If we're at the beginning of a new row, we use the position of above saved cell
            else if (currentRow > 0 && i == 0)
            {
                pos = posFirstCell;
                posFirstCell = new Vector3(pos.x, pos.y, pos.z + verticalOffset);
                pos = posFirstCell;
            }

            else if (i > 0)
            {
                pos = new Vector3(pos.x - horizontalOffset, pos.y, pos.z);
            }

            //Update the position and the scale
            grid[i].UpdatePosition(pos);
            grid[i].SetPosition(pos);

            //Instanciate the cube
            grid[i].SetCube((GameObject)Instantiate(grid[i].GetCube(), pos, Quaternion.identity));
            grid[i].GetCube().SetActive(true);

            //The cell above has a bottom wall so the top wall is not required
            grid[i].DestroyWallT();


            //Put all the cells in the same object to be able to move them together
            grid[i].GetCube().transform.parent = transform;

        }
        Debug.Log("currentROw: " + currentRow);

        currentRow = currentRow + 1; //update the current row
    }

    public void Eller()
    {
        arrCurrRow.Clear();

        //Initialize the next row with all walls
        CreateRow();

        /*
         * Initialize arrCurrRow with all singletons
         */

        for (int i = 0; i < colums; i++)
        {
            ArrayList single = new ArrayList();
            single.Add(i);
            arrCurrRow.Add(single);
        }

        //Apply the algorithm and randomly destroy horizontal walls

        for (int i = 0; i < colums; i++)
        {
            //Randomly destroy left and bottom walls

            int rand = gen.Next(2);

            if (rand == 1 && i != (colums - 1) && grid[i].HasLeftWall() == true)
            {
                grid[i].DestroyWallL();
                grid[i + 1].DestroyWallR();

                //Update ArrCurrRowwallB:
                RemoveSingleton(i + 1);
                AddNeighbor(i, i + 1);
            }

            rand = gen.Next(2);

            if (rand == 1 && grid[i].HasBotWall() == true)
            {
                grid[i].DestroyWallB();
                grid[i].EnableCollider();
            }

        }

        //Check that all the subset of connected cells have a vertical connection

        foreach (ArrayList obj in arrCurrRow)
        {
            bool hasBotWall = true;

            foreach (int i in obj)
            {
                if (grid[i].HasBotWall() == false)
                {
                    hasBotWall = false;
                }
            }

            if (hasBotWall == true)
            {
                int r = (int)obj[gen.Next(obj.Count)];
                grid[r].DestroyWallB();
                grid[r].EnableCollider();
            }

        }
        //PrintValues(arrCurrRow);
    }

    public static void PrintValues(IEnumerable myList)
    {
        foreach (ArrayList obj in myList)
        {
            foreach (int obj2 in obj)
                Debug.Log(" " + obj2);
            Debug.Log("\n");
        }
    }

    void RemoveSingleton(int i)
    {

        foreach (ArrayList obj in arrCurrRow)
        {
            if (obj.Contains(i))
            {
                arrCurrRow.Remove(obj);
                return;
            }

        }
        return;

    }

    void AddNeighbor(int i, int j)
    {

        foreach (ArrayList obj in arrCurrRow)
        {
            if (obj.Contains(i))
            {
                obj.Add(j);
                return;
            }

        }
        return;

    }

    //It is no longer necessary to have colliders at the bottom of the cell when we created the new line
    public void DestroyCellColliders()
    {
        for (int i = 0; i < colums; i++)
        {
            grid[i].DestroyCollider();
        }
    }

    public void incProjectiles()
    {
        this.nbProjectiles = this.nbProjectiles + 1;
        //Debug.Log("Nb of projectiles: " + projectiles);
    }

    public void decProjectiles()
    {
        this.nbProjectiles = this.nbProjectiles - 1;
        //Debug.Log("Nb of projectiles: " + projectiles);
    }

}
