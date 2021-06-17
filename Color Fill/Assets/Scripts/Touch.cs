using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class Touch : MonoBehaviour
{
    public GameObject box;
    public float x1;
    public float x2;
    public float y1;
    public float y2;
    public float move = 0;
    private bool canMove;
    private bool readyToMove;
    private float maxDistance = 50;
    RaycastHit hit;
    public float firstX;
    public float firstY;
    public float lastX;
    public float lastY;
    private bool isStopped;
    public GameObject[] tiles;
    

    void Start()
    {
        canMove = true;
        readyToMove = false;
    }

    
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            x1 = Mathf.Round(Input.mousePosition.x);
            y1 = Mathf.Round(Input.mousePosition.y);
            if (isStopped)
            {
                bool isHit = Physics.Raycast(transform.position, (transform.up) * -1, out hit, maxDistance);
                if (isHit)
                {
                    firstX = hit.transform.GetComponent<Tile>().x;

                    firstY = hit.transform.GetComponent<Tile>().y;

                }
            }
           
            readyToMove = false;
        }

        if (Input.GetMouseButtonUp(0))
        {

            x2 = Mathf.Round(Input.mousePosition.x);
            y2 = Mathf.Round(Input.mousePosition.y);
            readyToMove = true;

        }

        if (readyToMove)
        {
            if (Mathf.Abs(x2 - x1) > Mathf.Abs(y2 - y1))
            {
                if (x2 > x1)
                {
                   
                    if (canMove == true)
                    {
                        StartCoroutine(MoveRight());
                        canMove = false;
                    }

                }
                
                if (x1 > x2)
                {
                    
                    if (canMove == true)
                    {
                        StartCoroutine(MoveLeft());
                        canMove = false;
                    }
                }
            }

            if (Mathf.Abs(y2 - y1) > Mathf.Abs(x2 - x1))
            {
                if (y2 > y1)
                {
                    if (canMove)
                    {
                        StartCoroutine(MoveForward());
                        canMove = false;
                    }
                }
                if (y1 > y2)
                {
                    if (canMove)
                    {
                        StartCoroutine(MoveBackward());
                        canMove = false;
                    }
                }

            }
        }
    }

        IEnumerator MoveRight()
        {
        bool isHit = Physics.Raycast(transform.position, transform.right, out hit, maxDistance);
        if (isHit)
        {
            if (transform.position.x < hit.transform.position.x-1.5f)
            {
                transform.DOMoveX(transform.position.x + 1, 0.25f);
                isStopped = false;

            }
            else
            {
                isStopped = true;
                CalculateLastPosition();
            }
            

            
        }
           
            yield return new WaitForSeconds(0.25f);
            canMove = true;

        }

        IEnumerator MoveLeft()
        {
        bool isHit = Physics.Raycast(transform.position, transform.right*-1, out hit, maxDistance);
        if (isHit)
        {
            if (transform.position.x > hit.transform.position.x+1.5f)
            {
                transform.DOMoveX(transform.position.x - 1, 0.25f);
                isStopped = false;

            }
            else
            {
                isStopped = true;
                CalculateLastPosition();
            }
        }

            yield return new WaitForSeconds(0.25f);
            canMove = true;

        } 
        IEnumerator MoveForward()
        {
        bool isHit = Physics.Raycast(transform.position, transform.forward , out hit, maxDistance);
        if (isHit)
        {
            if (transform.position.z < hit.transform.position.z-1.5f)
            {
                transform.DOMoveZ(transform.position.z + 1.0f, 0.25f);
                isStopped = false;

            }
            else
            {
                isStopped = true;
                CalculateLastPosition();
            }
        }

           

            yield return new WaitForSeconds(0.25f);
            canMove = true;

        }
        IEnumerator MoveBackward()
        {
        bool isHit = Physics.Raycast(transform.position, transform.forward*-1, out hit, maxDistance);
        if (isHit)
        {
            if (transform.position.z > hit.transform.position.z + 1.5f)
            {
                transform.DOMoveZ(transform.position.z - 1.0f, 0.25f);
                isStopped = false;
            }
            else
            {
                isStopped = true;
                CalculateLastPosition();
            }
        }
            yield return new WaitForSeconds(0.25f);
            canMove = true;
        

        }

    private void OnDrawGizmos()
    {
        bool isHit = Physics.Raycast(transform.position, transform.right, out hit, maxDistance);
        if (isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.right * hit.distance);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.right * maxDistance);
        }
    }


    private void CalculateLastPosition()
    {

        bool isHit = Physics.Raycast(transform.position, (transform.up) * -1, out hit, maxDistance);
        if (isHit)
        {
            lastX = hit.transform.GetComponent<Tile>().x;

            lastY = hit.transform.GetComponent<Tile>().y;

            Fill();
            

        }
        

    }

    private void Fill()
    {

        int maxXValue = (int) Mathf.Max(firstX, lastX);
        int minXValue = (int) Mathf.Min(firstX, lastX);
        int maxYValue = (int) Mathf.Max(firstY, lastY);
        int minYValue = (int) Mathf.Min(firstY, lastY);

        for (int z = 0; z < tiles.Length; z++)
        {
            for (int i = maxXValue; i >= minXValue; i--)
            {

                for (int m = maxYValue; m >= minYValue; m--)
                {
                   
                    if (tiles[z].transform.GetComponent<Tile>().x == i && tiles[z].transform.GetComponent<Tile>().y == m)
                    {
                        tiles[z].transform.GetChild(0).gameObject.SetActive(true);
                    }

                }
            }
        }
        
        

            
    }














}
    






