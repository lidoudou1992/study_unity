using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct book
{
    public string title;
    public string author;
    public string subject;
    public int book_id;
}

public class Data : MonoSingleton<Data> {

	public int level = 1;
	public int boole = 1;
	public string name = "lidoudou";

}
