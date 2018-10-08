using UnityEngine;


public class Cell : MonoBehaviour
{

    private GameObject cube; //Aspect of the cell: we take a cube with a Mesh Renderer

    private Vector3 size; //Size of the cell
    private Vector3 position; //Position of the cell

    private bool hasWallL, hasWallR, hasWallB = true; //Considering that all cells have walls at the beginning
    private bool hasWallT = false;
    private bool projectile = true; //All cells have projectile at the beginning

    public Cell()
    {
    }

    public Cell(GameObject cube, Vector3 size)
    {

        this.cube = cube;
        this.size = size;
        position = new Vector3(-172f, -17.24f, -155f);
        UpdatePosition(position);

        hasWallL = true;
        hasWallR = true;
        hasWallB = true;
        hasWallT = false;
    }

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {

    }

    /* Getter/setter
     * 
     * Useful methods to manipulate the cells from the grid
     */


    public void SetCube(GameObject cube)
    {
        this.cube = cube;
    }

    public Vector3 GetPosition()
    {
        return this.position;
    }

    public GameObject GetCube()
    {
        return this.cube;
    }

    public Vector3 GetSize()
    {
        return size;
    }

    public float GetSizeX()
    {
        return size.x;
    }

    public float GetSizeY()
    {
        return size.y;
    }

    public float GetSizeZ()
    {
        return size.z;
    }

    public void UpdatePosition(Vector3 position)
    {
        cube.transform.position = position;
    }

    public void SetPosition(Vector3 position)
    {
        this.position = position;
    }

    public void SetSize(Vector3 size)
    {
        this.size = size;
    }

    public bool HasLeftWall()
    {
        return hasWallL;
    }

    public bool HasRightWall()
    {
        return hasWallR;
    }

    public bool HasTopWall()
    {
        return hasWallT;
    }

    public bool HasBotWall()
    {
        return hasWallB;
    }

    public void DestroyWallL()
    {
        GameObject wallL = cube.transform.GetChild(0).GetChild(0).gameObject;
        Destroy(wallL);
        hasWallL = false;
    }

    public void DestroyWallR()
    {
        GameObject wallR = cube.transform.GetChild(0).GetChild(1).gameObject;
        Destroy(wallR);
        hasWallR = false;
    }

    public void DestroyWallT()
    {
        GameObject wallT = cube.transform.GetChild(0).GetChild(2).gameObject;
        Destroy(wallT);
        hasWallT = false;
    }

    public void DestroyWallB()
    {
        GameObject wallB = cube.transform.GetChild(0).GetChild(3).gameObject;
        Destroy(wallB);
        hasWallB = false;
    }

    public void EnableCollider()
    {
        GameObject gO = cube.transform.GetChild(0).gameObject;
        Collider myCollider = gO.GetComponent<Collider>();
        myCollider.enabled = true;
    }

    public void DestroyCollider()
    {
        GameObject gO = cube.transform.GetChild(0).gameObject;
        Destroy(gO.GetComponent<Collider>());
    }

    public void DestroyProjectile()
    {
        GameObject proj = cube.transform.GetChild(0).GetChild(4).gameObject;
        Destroy(proj);
    }


}
