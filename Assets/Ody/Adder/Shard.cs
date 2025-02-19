using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Shard : MonoBehaviour
{


    public Text _name;
    public Text _price;


    public void SetInfo(string name, string value)
    {
        _name.text = name;
        _price.text = "+ " + value;
    }


}
