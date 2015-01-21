using UnityEngine;
using System.Collections;

public class Gui : MonoBehaviour 
	{
    public Vector2 scrollPosition;
    string longString = "";
    int    counter = 0;
	void OnGUI( )
		{
        if( GUI.Button(new Rect(420, 0, 55, 55), "CLEAR") )
            {
            longString = "";
            counter = 0;
            Log.counter = 0;
            }

        scrollPosition = GUILayout.BeginScrollView( scrollPosition, GUILayout.Width( 500 ), GUILayout.Height( 500 ) );

        if( Log.maxLog > counter )
            {
            lock( Log.logLock )
                {
                int totalLogs = Log.entry.Count;
                for( int ctr=0; ctr<totalLogs; ctr++ )
                    {
                    longString += "\n" + Log.entry[ctr].ToString();
                    counter++;
                    }
                Log.entry.Clear( );
                }
            }
        GUILayout.Label( longString );
        GUILayout.EndScrollView();
		}
	}