using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class MoveAgent : MonoBehaviour
{
    public List<GameObject> cubes = new List<GameObject>();
    private int[] rightLimits = { 3, 7, 11, 15 };
    private int[] topLimits = { 0, 1, 2, 3 };
    private int[] botLimits = { 12, 13, 14, 15 };
    private int[] leftLimits = { 0, 4, 8, 12 };

    private bool dead;

    private int currentState = 0;
    private int previousState = 0;
    private float reward = 0;

    private float[,] matrixState = new float[16, 4];
    public Transform matrixUI;

    public float lr;
    public float gamma;
    public float epsilon = 1.0f;
    public float epsilon_decay = 0.001f;
    public int MaxLoops;
    public int currentLoop = 1;

    public TextMeshProUGUI loopText;
    public TextMeshProUGUI failText;
    public TextMeshProUGUI passText;

    private int fails = 0;
    private int pass = 0;


    private float timeMoves = 1f;
    private float counterTime = 0f;

    private bool move = false;
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    // 0 = UP, 1 = RIGHT, 2 = DOWN, 3 = LEFT
    void Update()
    {
        if (currentLoop <= MaxLoops)
        {
            counterTime += Time.deltaTime;
            if (counterTime >= timeMoves)
            {
                move = true;
            }
            if (!dead && move)
            {
                int action = getActionWithOutDecay();

                // UP
                if (action == 0)
                {
                    if (!topLimits.Contains(currentState))
                    {
                        transform.Translate(new Vector3(0, 0, 2));
                        counterTime = 0f;
                        move = false;
                        StartCoroutine(ApplyQ_Learning(action));
                    }
                }
                // RIGHT
                if (action == 1)
                {
                    if (!rightLimits.Contains(currentState))
                    {
                        transform.Translate(new Vector3(2, 0, 0));
                        counterTime = 0f;
                        move = false;
                        StartCoroutine(ApplyQ_Learning(action));
                    }
                }
                // DOWN
                if (action == 2)
                {
                    if (!botLimits.Contains(currentState))
                    {
                        transform.Translate(new Vector3(0, 0, -2));
                        counterTime = 0f;
                        move = false;
                        StartCoroutine(ApplyQ_Learning(action));
                    }
                }
                // LEFT
                if (action == 3)
                {
                    if (!leftLimits.Contains(currentState))
                    {
                        transform.Translate(new Vector3(-2, 0, 0));
                        counterTime = 0f;
                        move = false;
                        StartCoroutine(ApplyQ_Learning(action));
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "hole")
        {
            reward = 0;
            dead = true;
            fails += 1;
            failText.text = "Fails: " + fails;
            StartCoroutine(ResetPosition());
        }
        else if (col.gameObject.tag == "end")
        {
            reward = 1;
            pass += 1;
            passText.text = "Pass: " + pass;
            dead = true;
            Debug.Log("You Win");
            previousState = currentState;
            currentState = cubes.IndexOf(col.gameObject);
            StartCoroutine(ResetPosition());
        }
        else
        {
            reward = 0;
            previousState = currentState;
            currentState = cubes.IndexOf(col.gameObject);
        }
    }

    IEnumerator ApplyQ_Learning(int action)
    {
        yield return new WaitForSeconds(.1f);
        int newState = currentState;
        float[] newStateArray = Enumerable.Range(0, matrixState.GetLength(1)).Select(x => matrixState[newState, x]).ToArray();
        //Debug.Log("Previous State: " + previousState);
        //Debug.Log("New State: "+ newState);
        if (currentState == 15)
        {
            reward = 1;
        }
        matrixState[previousState, action] = (1 - lr) * matrixState[previousState, action] + lr * (reward + gamma * newStateArray.Max());
        matrixUI.GetChild(4 * previousState + action).gameObject.GetComponent<TextMeshProUGUI>().text = System.Math.Round(matrixState[previousState, action], 2).ToString();
    }

    IEnumerator ResetPosition()
    {
        yield return new WaitForSeconds(.6f);
        currentState = 0;
        previousState = 0;
        transform.position = new Vector3(-7, 1, 2);
        epsilon = System.Math.Max(epsilon - epsilon_decay, 0);
        currentLoop += 1;
        loopText.text = "Current Loop: " + currentLoop;
        dead = false;
    }

    private int getActionWithDecay()
    {
        int action;
        float random = Random.Range(0.0f, 1.0f);
        if (random < epsilon)
        {
            action = Random.Range(0, 4);
        }
        else
        {
            float[] currentRow = Enumerable.Range(0, matrixState.GetLength(1)).Select(x => matrixState[currentState, x]).ToArray();
            int maxValueIndex = currentRow.ToList().IndexOf(currentRow.Max());
            if (currentRow[maxValueIndex] == 0)
            {
                action = Random.Range(0, 4);
            }
            else
            {
                action = maxValueIndex;
            }
        }
        return action;
    }

    private int getActionWithOutDecay()
    {
        int action;
        float[] currentRow = Enumerable.Range(0, matrixState.GetLength(1)).Select(x => matrixState[currentState, x]).ToArray();
        int maxValueIndex = currentRow.ToList().IndexOf(currentRow.Max());
        if (currentRow[maxValueIndex] == 0)
        {
            action = Random.Range(0, 4);
        }
        else
        {
            action = maxValueIndex;
        }
        return action;
    }
}
