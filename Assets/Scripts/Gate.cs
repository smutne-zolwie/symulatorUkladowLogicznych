using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{
    public List<bool> input = new List<bool>();
    public string operations = null;
    public string operations1 = null;
    public List<bool> scores = new List<bool>();
    Button bb;
    // Start is called before the first frame update
    void Start()
    {  
        
    }
    List<bool> Calculate(List<bool> input, string operations)
    {
        List<bool> result = new List<bool>();


        string[] operationsT = operations.Split(',');
        foreach(string operation in operationsT)
        {
            print(operation);
        }
        int numberOfOperations = input.Count;
        print(input.Count);
        foreach(string operation in operationsT)
        {
            if (operation.Contains("&") && numberOfOperations>(0.5f * input.Count))// liczba operacji nie mo¿e byæ mniejsza od po³owy inputu gdy¿ to nie mo¿liwe po co robiæ input jak siê z niego nie skorzysta
            {
                numberOfOperations--; // podstawowo jest to liczba inputu, operacja & potrzebuje dwóch inputów a ! jeden input wiêc ka¿de & zmniejszy liczbê mo¿liwych operacji o 1 a ! nic nie zmieni
                //dla bramki koñcowej potrzebne s¹ znaczniki ¿eby wykonywa³o operacje na mniejszej liczbie bramek, 
            }
        }
        print(numberOfOperations); 
        for(int i = 0; i < input.Count; i++)
        {
            //operacje na booleanach
            //numberOfOperatiuons to index ostatniej wykonanej operacji
            //trzeba sprawdziæ czy to nie jest te¿ ostatnia operacja
            int ee = 0;                        
            //b³¹d podczas przypisywania          
            if (operationsT[ee].Contains("&") && ee < numberOfOperations)
            {
                result.Add(input[ee]);
                int ii = i + 1;
                print("ii równe: " + ii);
                print("i równe: " + i);
                result[ee] = input[i] & input[ii];
                print(input[i] +" "+ input[ii] +" "+ result[ee]);
                i++;
                if (operationsT[ee].Contains("!"))
                {
                    result[ee] = !result[ee];
                }
                ee++;
            }
            else if (operationsT[ee].Contains("!") && ee < numberOfOperations)
            {
                result[ee] = !result[ee];
                ee++;
            }            
            print("i równe po:" + i);
        }
        foreach (bool resulte in result)
        {
            print(resulte);
        }
        return result;
        /*
        bool result = input[0];       
        string[] operationsT = operations.Split(' ');
        foreach(string operation in operationsT)
        {
            print(operation);
        }
        for (int i = 0; i < operationsT.Length; i++)
        {
            if (operationsT[i] == "&" && input.Count == 2)
                result = input[0] & input[1];            
            else if (operationsT[i] == "!")
                result = !result;
        }
        return result;
        */
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            scores = Calculate(input, operations);
            foreach(bool score in scores)
            {
                print(score);
            }
        }       
    }
}
