using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// CLIENT
public class NetworkManager : MonoBehaviour 
    {
    private GameManager     _gameManager;

    private string          _masterServerIP;
    private int             _masterServerPORT;
    private int             _masterServerNATPORT;
    private int             _gameServerPORT;
    private HostData[]      _hostData;

    private int             _randomSeed;

    private void Start( )
        {
        _gameManager    = GetComponent<GameManager>( );

        Log.Add( "Starting Client..." );
        
        _masterServerIP               = "84.245.28.56";
        //_masterServerIP               = "192.168.1.41";
        //_masterServerIP                 = "127.0.0.1";

        //Network.Connect( _masterServerIP, 22001, "HolyMoly" );
        Network.Connect( _masterServerIP, 22001 );
        //MasterServer.RequestHostList( "ServerName" );
        }


    private void Update( ) 
        {
        }

    public void OnMasterServerEvent( MasterServerEvent mse )
        {
        Log.Add( "---> OnMasterServerEvent: " + mse.ToString() );
        }

    private void OnServerInitialized( )
        {
        Log.Add( "---> OnServerInitialized: ");
        }

    private void OnConnectedToServer( )
        {
        Log.Add( "---> OnConnectedToServer: ");

        Log.Add( "Send message to server" );
        networkView.RPC( "RPC_RequestFromClient", RPCMode.Server, 666 ); 
        }

    private void OnPlayerConnected( NetworkPlayer networkPlayer ) 
        {
        Log.Add( "---> OnPlayerConnected: ");
        }

    private void OnPlayerDisconnected( NetworkPlayer networkPlayer )
        {
        Log.Add( "---> OnPlayerDisconnected: ");
        Network.RemoveRPCs( networkPlayer );
        Network.DestroyPlayerObjects( networkPlayer );
        }

    private void OnDisconnectedFromServer( NetworkDisconnection info )
        {
        Log.Add( "--> OnDisconnectedFromServer" );
        }

    private void OnNetworkInstantiate( NetworkMessageInfo info ) 
        {
        Log.Add( "--> OnNetworkInstantiate" );
        }

    private void OnFailedToConnect(NetworkConnectionError error) 
        {
        Log.Add( "---> OnPlayerDisconnected: " + error );
        }
    
    private void OnApplicationQuit( )
        {
        Network.Disconnect( );
        }

//---------------------------------------------------------------------------------------------------------------> NETWORK RPC
    [RPC] private void RPC_RequestFromClient( int someInt, NetworkMessageInfo info )
        {
        // Empty Declaration
        }

    [RPC] private void RPC_ResponseFromServer( int someInt )
        {
        Log.Add( "Received Message From Server: " + someInt.ToString() );
        }
    }