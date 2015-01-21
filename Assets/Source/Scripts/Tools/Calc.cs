using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

static class Calc
	{
	public static long UnixTimeStamp( )
		{
		return (long)( DateTime.UtcNow - new DateTime( 1970, 1, 1) ).TotalMilliseconds;
		}
	
    public static int PositionToIndex( float x, float y, int mapWidth, int scale )
        {
        int index;

        //index = (int)( Mathf.Floor( x / scale ) + ( Mathf.Floor( y / scale ) * mapWidth ) );
        index = (int)( Math.Floor( x / scale ) + ( Math.Floor( y / scale ) * mapWidth ) );
        
        return index;
        }

    public static void IndexToPosition( float  index, int mapWidth, int scale, out int x, out int y )
        {
        //x = (int)( Mathf.Floor( ( index % mapWidth ) ) * scale );
        //y = (int)( Mathf.Floor( ( index / mapWidth ) ) * scale );
        x = (int)( Math.Floor( ( index % mapWidth ) ) * scale );
        y = (int)( Math.Floor( ( index / mapWidth ) ) * scale );
        }

	public static float PositionToAngle( Vector2 beginningPosition, Vector2 endingPosition )
		{
		/*
			       90.0f
	      180.0f           0.0f
		          270.0f
		*/
		Vector2 targetDir = endingPosition - beginningPosition;
		
		float angle = Mathf.Atan2( targetDir.x, targetDir.y ) * Mathf.Rad2Deg;
		if( angle < 0 )
			{
			angle = angle + 360;
			}
		
		return angle;
		}

    public static float RotateToAngle( float turningSpeed, int turnDirection, float currentAngle, float destinationAngle )
        {
        /*
        if( < turnDirection ) { rotate left  }
        if( > turnDirection ) { rotate right }
        Returns degrees(0-359.9f) from angle to angle
        */

        float newAngle = 0.0f;

        if( turnDirection < 0 )
            {
            float remainder = 0;
            if( currentAngle > destinationAngle )   { remainder = currentAngle - destinationAngle; }
            else                                    { remainder = destinationAngle - currentAngle; }
            if ( remainder > turningSpeed )
                {
                currentAngle = currentAngle - turningSpeed;
                if( currentAngle < 0.0f ) { currentAngle = 360.0f + currentAngle; }
                newAngle = currentAngle;
                }
            else
                {
                newAngle = destinationAngle;
                }
            }
        else
            {
            float remainder = 0;
            if( currentAngle > destinationAngle )   { remainder = currentAngle - destinationAngle; }
            else                                    { remainder = destinationAngle - currentAngle; }
            if ( remainder > turningSpeed )
                {
                currentAngle = currentAngle + turningSpeed;
                if( currentAngle > 360.0f ) { currentAngle = currentAngle - 360.0f; }
                newAngle = currentAngle;
                }
            else
                {
                newAngle = destinationAngle;
                }
            }

        return newAngle;
        }

	public static float RotateToNearestAngle( float sourceAngle, float targetAngle )
		{
		/*
		NearestAngleDirection( 359.0f, 1.0f )  ->  2.0f
		NearestAngleDirection( 1.0f, 359.0f )  -> -2.0f
		*/

		float angle;
		angle = ( targetAngle - sourceAngle ) % 360.0f;
		
		if( angle < 0.0f )  
			{ angle = angle + 360.0f; }
		
		if( angle > 180.0f )
			{ angle = angle - 360.0f; }
		
		return angle;
		}

    public static Vector2 NextPointOnLine( float angle, float distance )
        {
        if( angle < 0.0f || angle > 360.0f ) { Log.Add( "Calc.NextPointOnLine : angle needs to be between 0 and 360, input: "+ angle.ToString() ); }

        float newPosX = ( Mathf.Cos ( angle * Mathf.Deg2Rad ) * distance );
        float newPosY = ( Mathf.Sin ( angle * Mathf.Deg2Rad ) * distance );

        return new Vector2( newPosX, newPosY );
        }

    public static float LineDistance( Vector2 beginningPosition, Vector2 endingPosition )
        {
        float 	x = endingPosition.x - beginningPosition.x;
                x = x * x;
	
        float	y = endingPosition.y - beginningPosition.y;
                y = y * y;
	
        float 	result = Mathf.Sqrt( x + y );
	    
        return result;
        }

    //public static Key2IntObject TileOffsetNeighbourByAngle( int angle )
    //    {
    //    int offsetX = 0;
    //    int offsetY = 0;

    //    if( angle ==   0 ) { offsetX = + 1;  offsetY = + 0; }
    //    if( angle ==  90 ) { offsetX = + 0;  offsetY = + 1; }
    //    if( angle == 180 ) { offsetX = - 1;  offsetY = + 0; }
    //    if( angle == 270 ) { offsetX = + 0;  offsetY = - 1; }

    //    if( angle ==  45 ) { offsetX = + 1;  offsetY = + 1; }
    //    if( angle == 135 ) { offsetX = - 1;  offsetY = + 1; }
    //    if( angle == 225 ) { offsetX = - 1;  offsetY = - 1; }
    //    if( angle == 315 ) { offsetX = + 1;  offsetY = - 1; }
        
    //    Key2IntObject key2Int = new Key2IntObject( );
    //    key2Int.k1 = offsetX;
    //    key2Int.k2 = offsetY;
    //    return key2Int;
    //    }

    public static int TileOffsetNeighbourToAngle( int offsetX, int offsetY )
        {
        int angle = 0;

        if( ( offsetX == + 1 ) && ( offsetY == + 0 ) ) { angle =   0; }
        if( ( offsetX == + 0 ) && ( offsetY == + 1 ) ) { angle =  90; }
        if( ( offsetX == - 1 ) && ( offsetY == + 0 ) ) { angle = 180; }
        if( ( offsetX == + 0 ) && ( offsetY == - 1 ) ) { angle = 270; }

        if( ( offsetX == + 1 ) && ( offsetY == + 1 ) ) { angle =  45; }
        if( ( offsetX == - 1 ) && ( offsetY == + 1 ) ) { angle = 135; }
        if( ( offsetX == - 1 ) && ( offsetY == - 1 ) ) { angle = 225; }
        if( ( offsetX == + 1 ) && ( offsetY == - 1 ) ) { angle = 315; }
        return angle;
        }

    //public static List<Key2IntObject> PathFindResultList( bool debug, BaseGrid searchGrid, int startingPositionX, int startingPositionY, int endingPositionX, int endingPositionY )
    //    {
    //    List<Key2IntObject> resultPathList = new List<Key2IntObject>( );

    //    try { try 
    //    {
    //    long timeNow = 0;
    //    if( debug ) { Log.Add( "PathFindResultList Debug Log Start" ); timeNow = Calc.UnixTimeStamp( ); }
        
    //    float   positionToAngle = Calc.PositionToAngle( new Vector2( startingPositionX, startingPositionY ), new Vector2( endingPositionX, endingPositionY ) );
    //    float   lineDistance    = Calc.LineDistance( new Vector2( startingPositionX, startingPositionY ), new Vector2( endingPositionX, endingPositionY ) );
    //    Vector2 nextPointOnLine = Calc.NextPointOnLine( positionToAngle, lineDistance );
    //    Vector2 targetPosition  = new Vector2( ( 10 + nextPointOnLine.x ), ( 10 + nextPointOnLine.y ) );
    //    GridPos startPos        = new GridPos( 10, 10 ); 
    //    GridPos endPos          = new GridPos( (int)( Math.Round( targetPosition.x ) ), (int)( Math.Round( targetPosition.y ) ) );  

    //    if( debug )
    //        {
    //        Log.Add( "positionToAngle: "    + positionToAngle.ToString());
    //        Log.Add( "lineDistance: "       + lineDistance.ToString());
    //        Log.Add( "nextPointOnLine: "    + nextPointOnLine.ToString());
    //        Log.Add( "targetPosition: "     + targetPosition.ToString());
    //        Log.Add( "endpos: "             + ( (int)( Math.Round( targetPosition.x ) ) ).ToString( ) +","+ ( (int)( Math.Round( targetPosition.y ) ) ).ToString( ) );
    //        }

    //    JumpPointParam jpParam  = new JumpPointParam( searchGrid, startPos, endPos, true, true, true ); 
    //    jpParam.AllowEndNodeUnWalkable  = true;
    //    jpParam.CrossAdjacentPoint      = true;
    //    jpParam.CrossCorner             = true;
    //    jpParam.SetHeuristic( HeuristicMode.MANHATTAN );

    //    List<GridPos>       gridPathList   = JumpPointFinder.FindPath( jpParam );

    //    int countbreak = 0;
    //    int total = gridPathList.Count;
    //    for( int ctr=0; ctr<total; ctr++ )
    //        {
    //        int gx = gridPathList[ctr].x;
    //        int gy = gridPathList[ctr].y;
    //        if( debug ) { Log.Add( "GridPath: "+ctr+") "+gx.ToString() +","+ gy.ToString() ); }
    //        }

    //    for( int ctr=0; ctr<total-1; ctr++ )
    //        {
    //        int gridPathCurrentX = gridPathList[ctr].x;
    //        int gridPathCurrentY = gridPathList[ctr].y;
    //        int gridPathNextX    = gridPathList[ctr+1].x;
    //        int gridPathNextY    = gridPathList[ctr+1].y;
    //        int highestX = gridPathCurrentX - gridPathNextX;
    //        if( gridPathNextX > gridPathCurrentX ) { highestX = gridPathNextX - gridPathCurrentX; }
    //        int highestY = gridPathCurrentY - gridPathNextY;
    //        if( gridPathNextY > gridPathCurrentY ) { highestY = gridPathNextY - gridPathCurrentY; }

    //        if( highestX > 1 || highestY > 1 )
    //            {
    //            float angle = Calc.PositionToAngle( new Vector2( gridPathCurrentX, gridPathCurrentY ), new Vector2( gridPathNextX, gridPathNextY ) );
    //            while( gridPathCurrentX != gridPathNextX || gridPathCurrentY != gridPathNextY )
    //                {
    //                Key2IntObject tileOffset = Calc.TileOffsetNeighbourByAngle( (int)(angle) );
                    
    //                Key2IntObject newTileOffset = new Key2IntObject( );
    //                newTileOffset.k1 = tileOffset.k1;
    //                newTileOffset.k2 = tileOffset.k2;
    //                if( debug ) { Log.Add( ">>resultPathList: "+ newTileOffset.k1.ToString() +","+ newTileOffset.k2.ToString() ); }
    //                resultPathList.Add( newTileOffset );

    //                int newHighestX = gridPathCurrentX + tileOffset.k1;
    //                int newHighestY = gridPathCurrentY + tileOffset.k2;
    //                gridPathCurrentX = newHighestX;
    //                gridPathCurrentY = newHighestY;
    //                countbreak++; if( countbreak > 10 ) { Log.Add( "break" ); break; }
    //                }
    //            }
    //        else
    //            {
    //            int tileOffsetResultX = 0;
    //            int tileOffsetResultY = 0;
    //            for( int angleStepctr=0; angleStepctr<8; angleStepctr++ )
    //                {
    //                int angle = 0;
    //                if( angleStepctr == 0 ) { angle =   0; }
    //                if( angleStepctr == 1 ) { angle =  90; }
    //                if( angleStepctr == 2 ) { angle = 180; }
    //                if( angleStepctr == 3 ) { angle = 270; }
    //                if( angleStepctr == 4 ) { angle =  45; }
    //                if( angleStepctr == 5 ) { angle = 135; }
    //                if( angleStepctr == 6 ) { angle = 225; }
    //                if( angleStepctr == 7 ) { angle = 315; }

    //                Key2IntObject tileOffset = Calc.TileOffsetNeighbourByAngle( angle );
    //                if( ( ( tileOffset.k1 + gridPathCurrentX ) == gridPathNextX ) && ( ( tileOffset.k2 + gridPathCurrentY ) == gridPathNextY ) )
    //                    {
    //                    tileOffsetResultX = tileOffset.k1; 
    //                    tileOffsetResultY = tileOffset.k2;
    //                    if( debug ) { Log.Add( "resultPathList: "+(tileOffsetResultX.ToString()) +","+ (tileOffsetResultY.ToString()) ); }
    //                    break;
    //                    }
    //                }

    //            Key2IntObject newTileOffset = new Key2IntObject( );
    //            newTileOffset.k1 = tileOffsetResultX;
    //            newTileOffset.k2 = tileOffsetResultY;
    //            resultPathList.Add( newTileOffset );
    //            }
    //        }

    //    if( debug ) { Log.Add( "PathFindResultList Debug Log End ("+ ( Calc.UnixTimeStamp( ) - timeNow ).ToString()+"ms)" ); }
    //    }catch(Exception e){ Log.Add( "UnitMovement: "+e.ToString());}}catch(ThreadAbortException e){ Log.Add( "UnitMovement: "+e.ToString());}
    //    return resultPathList;
    //    }
	}














//public static Vector2 LineDistance( Vector2 beginningPosition, Vector2 endingPosition )
//    {
//    float distancePositionX = 0.0f;
//    if( beginningPosition.x > endingPosition.x )
//        {
//        distancePositionX = beginningPosition.x - endingPosition.x;
//        }
//    else
//        {
//        distancePositionX = endingPosition.x - beginningPosition.x;
//        }
		
//    float distancePositionY = 0.0f;
//    if( beginningPosition.y > endingPosition.y )
//        {
//        distancePositionY = beginningPosition.y - endingPosition.y;
//        }
//    else 
//        {
//        distancePositionY = endingPosition.y - beginningPosition.y;
//        }
		
//    return new Vector2( distancePositionX, distancePositionY );
//    }
































