using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    bool genEntrance = false;
    bool hasWon = false;

    //Write a method whenever 2 objects collide. We can use it with any object that has RigidBody and Collider
    //collisionInfo: to know what object we collided
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Cell")
        {
            //Debug.Log("Hitting the end of the cell");

            FindObjectOfType<MazeGrid>().DestroyCellColliders();
            FindObjectOfType<MazeGrid>().Eller(); //here, we simply want to locate our dialogue manager

            //movement.enabled = false;
        }

        else if (genEntrance == false && collisionInfo.collider.tag == "Entrance")
        {
            //Debug.Log("Hitting");
            GameObject gO = GameObject.Find("Maze");
            Vector3 pos = gO.transform.position;
            gO = (GameObject)Instantiate(gO, pos, Quaternion.identity);
            gO.transform.GetChild(0).gameObject.SetActive(true);

            genEntrance = true;
        }


        if (collisionInfo.collider.tag == "Projectiles")
        {
            Debug.Log("Hitting");
            //Destroy(collisionInfo.gameObject);
            //FindObjectOfType<Cell>().DestroyProjectile();
            //collisionInfo.gameObject.SetActive(false);
            Destroy(collisionInfo.gameObject);
        }

        if (collisionInfo.collider.tag == "Win")
        {
            hasWon = true;
            OnGUI();

        }

    }
    void OnGUI()
    {
        if (hasWon==true)
            GUI.Label(new Rect(10, 10, 100, 20), "Congratulations!");
    }
}
