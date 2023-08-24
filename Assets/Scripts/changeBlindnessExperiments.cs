using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeBlindnessExperiments : MonoBehaviour
{
    List<int> id_list;

    void Start()
    {
        id_list = new List<int>(new int[] {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15});
    }

    // Function to perform a change in the scene
    public void performChange()
    {
        int number_of_changes = id_list.Count;

        if (number_of_changes > 0)
        {
            // Get an index of id_list randomly
            int random_idx = (int)Random.Range(0.0f, number_of_changes-1);
            
            // Get the id of the change to perform
            int change_id = id_list[random_idx];

            Debug.Log("Experiment id: " + change_id.ToString());

            // Perform the change
            switch (change_id)
            {
                case 0:
                    addStraw();
                    break;
                case 1:
                    addBarrel();
                    break;
                case 2:
                    addWindmill();
                    break;
                case 3:
                    addTree();
                    break;
                case 4:
                    removeCoins();
                    break;
                case 5:
                    removeTree();
                    break;
                case 6:
                    removeFarm();
                    break;
                case 7:
                    removeTrolley();
                    break;
                case 8:
                    subsBoxByBarrel();
                    break;
                case 9:
                    changeColoredBuildings();
                    break;
                case 10:
                    changeWellWithChapel();
                    break;
                case 11:
                    subsTreeWindmill();
                    break;
                case 12:
                    moveRightHouse();
                    break;
                case 13:
                    moveLeftHouse();
                    break;
                case 14:
                    moveBarrel();
                    break;
                case 15:
                    moveTree();
                    break;
            }

            // Store the id of the performed change
            int numChanges = PlayerPrefs.GetInt("numChanges", 0);
            string key = "exp_id_" + numChanges.ToString();
            PlayerPrefs.SetInt(key, change_id);

            // Increment the number of changes
            numChanges = numChanges + 1;
            PlayerPrefs.SetInt("numChanges", numChanges);

            // Remove the id from the list so that is not repeated
            id_list.Remove(change_id);
        }

    }

    void addStraw()
    {
        GameObject straw = GameObject.FindGameObjectWithTag("straw");
        Vector3 position = new Vector3(32.4f, -1.27f, 57.95f);
        Instantiate(straw, position, Quaternion.identity);
    }

    void addBarrel()
    {
        GameObject barrel = GameObject.FindGameObjectWithTag("barrel");
        Vector3 position = new Vector3(-15.31f, -3.0f, 53.0f);
        Instantiate(barrel, position, Quaternion.identity);
    }

    void addWindmill()
    {
        GameObject windmill = GameObject.FindGameObjectWithTag("windmill");
        Vector3 position = new Vector3(-28.4f, -3.0f, 170.0f);
        Instantiate(windmill, position, Quaternion.identity);
    }

    void addTree()
    {
        GameObject tree = GameObject.FindGameObjectWithTag("tree");
        Vector3 position = new Vector3(40.0f, -3.0f, 180.0f);
        Instantiate(tree, position, Quaternion.identity);
    }

    void removeCoins()
    {
        GameObject coins = GameObject.FindGameObjectWithTag("coins");
        Destroy(coins);
    }

    void removeTree()
    {
        GameObject tree = GameObject.FindGameObjectWithTag("rem_tree");
        Destroy(tree);
    }

    void removeFarm()
    {
        GameObject farm = GameObject.FindGameObjectWithTag("rem_farm");
        Destroy(farm);
    }

    void removeTrolley()
    {
        GameObject trolley = GameObject.FindGameObjectWithTag("trolley");
        Destroy(trolley);
    }

    void changeWellWithChapel()
    {
        GameObject well = GameObject.FindGameObjectWithTag("well");
        GameObject chapel = GameObject.FindGameObjectWithTag("chapel");

        Vector3 chapelPosition = chapel.transform.position;
        chapel.transform.position = new Vector3(chapel.transform.position.x, chapel.transform.position.y, 40.0f);
        well.transform.position = new Vector3(chapelPosition.x, chapelPosition.y, 75.0f);
    }

    void changeColoredBuildings()
    {
        GameObject blue = GameObject.FindGameObjectWithTag("blue");
        GameObject pink = GameObject.FindGameObjectWithTag("pink");

        Vector3 blueHousePos = blue.transform.position;
        Quaternion blueHouseRot = blue.transform.rotation;
        Quaternion pinkHouseRot = pink.transform.rotation;

        blue.transform.position = new Vector3(pink.transform.position.x + 10.0f, pink.transform.position.y, pink.transform.position.z);
        pink.transform.position = new Vector3(blueHousePos.x + 7.0f, blueHousePos.y, blueHousePos.z);
        blue.transform.rotation = pinkHouseRot;
        pink.transform.rotation = blueHouseRot;

    }

    void subsBoxByBarrel()
    {
        GameObject box = GameObject.FindGameObjectWithTag("box");
        GameObject barrel = GameObject.FindGameObjectWithTag("barrel");

        Destroy(box);

        Vector3 position = new Vector3(-17.09f, -3.0f, 25.41f);
        Instantiate(barrel, position, Quaternion.identity);
        
    }

    void subsTreeWindmill()
    {
        GameObject tree = GameObject.FindGameObjectWithTag("subtree");
        GameObject windmill = GameObject.FindGameObjectWithTag("windmill");

        Destroy(tree);

        Vector3 position = new Vector3(21.20f, -3.0f, 218.33f);
        Instantiate(windmill, position, Quaternion.identity);
        
    }

    void moveRightHouse()
    {
        GameObject houseR = GameObject.FindGameObjectWithTag("houseR");
        Vector3 HousePos = houseR.transform.position;

        houseR.transform.position = new Vector3 (HousePos.x + 10.0f, HousePos.y, HousePos.z);

    }

    void moveLeftHouse()
    {
        GameObject houseL = GameObject.FindGameObjectWithTag("houseL");
        Vector3 HousePos = houseL.transform.position;

        houseL.transform.position = new Vector3 (HousePos.x - 10.0f, HousePos.y, HousePos.z);

    }

    void moveBarrel()
    {
        GameObject barrel = GameObject.FindGameObjectWithTag("barrel");
        Vector3 TransBarrel = barrel.transform.position;

        barrel.transform.position = new Vector3 (TransBarrel.x, TransBarrel.y, TransBarrel.z - 15.0f);

    }

    void moveTree()
    {
        GameObject tree = GameObject.FindGameObjectWithTag("arbolico");
        Vector3 TransTree = tree.transform.position;

        tree.transform.position = new Vector3 (TransTree.x - 15.0f, TransTree.y, TransTree.z);

    }


}
