using UnityEngine;
using UnityEditor;

public class PointClickCamEditor : EditorWindow
{

    enum Direction { Left, Right, Forward, Backward };

    [MenuItem("Window/Point and Click Editor")]
    public static void ShowWindow()
    {
        GetWindow<PointClickCamEditor>("PointClickEditor");
    }

    void OnGUI()
    {
        GUILayout.Label("Point and Click editor", EditorStyles.boldLabel);
        GUILayout.Label("Use the buttons to easily create Point and Click camera points.", EditorStyles.helpBox);
        GUILayout.Label("This actions will only work when PointClickCam GameObjects are selected.", EditorStyles.helpBox);

        if (GUILayout.Button("Extend - z axis +"))
        {
            CreateNewPointAndClick( Direction.Left );
        }

        if (GUILayout.Button("Extend - z axis -"))
        {
            CreateNewPointAndClick(Direction.Right);
        }

        if (GUILayout.Button("Extend - x axis +"))
        {
            CreateNewPointAndClick(Direction.Forward);
        }

        if (GUILayout.Button("Extend - x axix -"))
        {
            CreateNewPointAndClick(Direction.Backward);
        }


        GUILayout.Label("This action creates a stair setup. The best start for this action", EditorStyles.helpBox);
        GUILayout.Label("is adding 1 small prefab and selecting that prefab.", EditorStyles.helpBox);

        if (GUILayout.Button("Create stairs setup"))
        {
            CreateStairSetup();
        }
    }

    void CreateNewPointAndClick( Direction dir )
    {
        if (Selection.count == 1)
        {
            if (Selection.gameObjects[0].GetComponent<PointClickCam>() != null)
            {
                GameObject go = Instantiate(Selection.activeGameObject);
                Vector3 newPos = new Vector3(0, 0, 0);

                if (dir == Direction.Left)
                {
                    newPos.x = go.transform.position.x;
                    newPos.y = go.transform.position.y;
                    newPos.z = go.transform.position.z + 1.0f;
                }

                if (dir == Direction.Right)
                {
                    newPos.x = go.transform.position.x;
                    newPos.y = go.transform.position.y;
                    newPos.z = go.transform.position.z - 1.0f;
                }

                if (dir == Direction.Forward)
                {
                    newPos.x = go.transform.position.x + 1.0f;
                    newPos.y = go.transform.position.y;
                    newPos.z = go.transform.position.z;
                }

                if (dir == Direction.Backward)
                {
                    newPos.x = go.transform.position.x - 1.0f;
                    newPos.y = go.transform.position.y;
                    newPos.z = go.transform.position.z;
                }

                go.transform.position = newPos;
                go.transform.name = newName();
                go.GetComponent<PointClickCam>().backModel = Selection.gameObjects[0];

                Selection.activeGameObject = go;

            }
            else
            {
                Debug.Log("PointClickCam component not found.");
            }
        }
        else

        {
            Debug.Log("Select only one GameObject containing a PointClickCam component.");
        }
    }

    void CreateStairSetup()
    {

        if (Selection.count == 1)
        {
            if (Selection.gameObjects[0].GetComponent<PointClickCam>() != null)
            {
                Vector3 newPos = new Vector3(0, 0, 0);

                GameObject go1 = Selection.gameObjects[0];

                GameObject go2 = Instantiate(Selection.activeGameObject);
                newPos.x = go2.transform.position.x;
                newPos.y = go2.transform.position.y + 1.2f;
                newPos.z = go2.transform.position.z;
                go2.transform.position = newPos;
                go2.transform.name = newName();

                GameObject go3 = Instantiate(Selection.activeGameObject);
                newPos.x = go3.transform.position.x;
                newPos.y = go3.transform.position.y + 2.4f;
                newPos.z = go3.transform.position.z;
                go3.transform.position = newPos;
                go3.transform.name = newName();

                GameObject go4 = Instantiate(Selection.activeGameObject);
                newPos.x = go4.transform.position.x;
                newPos.y = go4.transform.position.y + 2.4f;
                newPos.z = go4.transform.position.z + 1.0f;
                go4.transform.position = newPos;
                go4.transform.name = newName();


                // The way forward
                GameObject[] arr = {go2,go3,go4};
                go1.GetComponent<PointClickCam>().GroupAnimation = arr;
                go4.GetComponent<PointClickCam>().backModel = go3;
                go3.GetComponent<PointClickCam>().backModel = go2;
                go2.GetComponent<PointClickCam>().backModel = go1;

                // The way back
                GameObject[] arrBack = { go2, go1, go1.GetComponent<PointClickCam>().backModel };
                go3.GetComponent<PointClickCam>().GroupAnimation = arrBack;

            }
            else
            {
                Debug.Log("PointClickCam component not found.");
            }
        }
        else

        {
            Debug.Log("Select only one GameObject containing a PointClickCam component.");
        }

    }

    string newName()
    {
        int counter = 0;

        GameObject[] respawns = GameObject.FindGameObjectsWithTag("PointClickCam");
        foreach (GameObject respawn in respawns)
        {
            counter++;
        }

        counter++;

        return "pc_cam_" + counter;
    }

}
