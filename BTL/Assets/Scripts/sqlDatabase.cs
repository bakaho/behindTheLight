using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class sqlDatabase : MonoBehaviour {
    string dbPath;
    IDbConnection dbcon;

	// Use this for initialization
	void Start () {
        //set a path and create the database
        dbPath = "URI=file:" + Application.persistentDataPath + "/btlLocal.db";
        dbcon = new SqliteConnection(dbPath);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void openDB(){
        dbcon.Open();
    }

    public void createTable(){
        //dbcmd = 
    }
}
