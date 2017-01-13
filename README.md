# uSQL
- Patched version of MySql.Data 5.2.7.0 
  - in CharSetMap.cs : CharSetMap.mapping.Add("utf8mb4", new CharacterSet("utf-8", 4));
- Very basic (and incomplete) utiltiy classes in order to construct SQL statements to be sent through the connection.

The goal of this project is to add easy to use MySQL and SQLite integration into Unity or C# projects. It was originally designed for internal use on an unreleased project in order to pull data from a live MySQL database and populate GameObjects during a VR simulation. There were a lot of road blocks and hiccups along the way with the interaction between the MySQL library and the version of .NET Unity is capable of using, so I thought I would release this to help anyone else experiencing the same issues.  

In order to use this project, just download the .zip and put it in your Assets folder. If you get an error about not being able to find System.Drawing, go into Unity->File->Build Settings->Player Settings and change API Compatibility Level from .NET 2.0 Subset to .NET 2.0.

As soon as the project compiles, you can go into the Asset creation menu and select Create->uSQL->MySQLConnector and populate that scriptable object with your database information. Now attach that object to a GameObject and call functions on the scriptable object from your update loop or something similar. 

# TODO List
- I still want to add SQLite integration so both MySQL and SQLite can be used with the same interface classes.
- I'm looking into writing an external C# application that communicates to Unity coroutines in order to run the SQL statements on a different thread and not take up any frame time on Unity's end. 
- I still need to flesh out the SQLStatement class and possibly refactor/rename a few things along the way. I'm still learning SQL syntax as I go, so there's a lot of shortcuts that I took to make things work.