using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{
    public List<Elements> elements = new List<Elements>();
    public List<bool> input = new List<bool>();
    public string operations = null;
    public string operations1 = null;
    public string gName;
    public int maxInput;
    public Color color;        
    public List<bool> scores = new List<bool>();
    Elements element;    
    // Start is called before the first frame update

    void Awake()
    {
        maxInput = CountMaxInput(operations);
    }
    void Start()
    {
        element = GetComponent<Elements>();
    }    
    public int CountMaxInput(string operations)
    {
        int maxInput = 0;
        string[] operationsT = operations.Split(',');                
        foreach (string operation in operationsT)
        {
            if (operation.Contains("&"))
            {
                maxInput += 2;
            }
            if (operation.Contains("!"))
            {
                maxInput += 1;
            }
        }
        return maxInput;
    }
    List<bool> Calculate(List<bool> input, string operations)
    {
        List<bool> result = new List<bool>();


        string[] operationsT = operations.Split(',');
        int numberOfOperations = input.Count;
        print(input.Count);
        foreach(string operation in operationsT)
        {
            if (operation.Contains("&") && numberOfOperations>(0.5f * input.Count))// liczba operacji nie mo�e by� mniejsza od po�owy inputu gdy� to nie mo�liwe po co robi� input jak si� z niego nie skorzysta
            {
                numberOfOperations--; // podstawowo jest to liczba inputu, operacja & potrzebuje dw�ch input�w a ! jeden input więc każde & zmniejszy liczb� mo�liwych operacji o 1 a ! nic nie zmieni
                //dla bramki ko�cowej potrzebne s� znaczniki �eby wykonywa�o operacje na mniejszej liczbie bramek, 
            }
        }
        print(numberOfOperations);
        int ee = 0;
        for (int i = 0; i < input.Count; i++)
        {
            //operacje na booleanach
            //numberOfOperatiuons to index ostatniej wykonanej operacji
            //trzeba sprawdzi� czy to nie jest te� ostatnia operacja                   
            if (operationsT[ee].Contains("&") && ee < numberOfOperations)
            {
                result.Add(input[ee]);
                int ii = i + 1;
                result[ee] = input[i] & input[ii];
                i++;
                if (operationsT[ee].Contains("!"))
                {
                    result[ee] = !result[ee];
                }
                ee++;
            }
            else if (operationsT[ee].Contains("!") && ee < numberOfOperations)
            {                
                if (0 == result.Count)
                {
                    result.Add(input[ee]);                               
                }
                result[ee] = !result[ee];
                ee++;
            }            
            print("i r�wne po:" + i);
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
    public void SetUpCalculatuion()
    {
        input.Clear();
        int i = 0;
        foreach (Elements element in elements)
        {
            if (input.Count < CountMaxInput(operations))
            {
                input.Add(element.state);
                i++;
            }
        }
        scores = Calculate(input, operations);
        if (scores.Count == 1)
        {
            element.state = scores[0];
        }
    }
}
