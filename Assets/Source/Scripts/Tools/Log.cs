using UnityEngine;
using System.Collections;
using System.Collections.Generic;

static class Log
	{
    public static System.Object logLock = new System.Object( );

	public static List<string> entry    = new List<string>( );
	public static int counter           = 0;
	public static int maxLog            = 500;
	public static void Add( string logString )
		{
        if( maxLog > counter )
            {
            lock( logLock )
                {
		        entry.Add( logString );
                }
            }
        counter++;
		}
	}