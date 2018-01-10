﻿using MLAPI.NetworkingManagerComponents;
using UnityEngine;

namespace MLAPI
{
    public class NetworkedObject : MonoBehaviour
    {
        [HideInInspector]
        public uint NetworkId;
        [HideInInspector]
        public int OwnerClientId = -1;
        [HideInInspector]
        public int SpawnablePrefabIndex;
        [HideInInspector]
        public bool isPlayerObject = false;
        public bool ServerOnly = false;
        public bool isLocalPlayer
        {
            get
            {
                return isPlayerObject && (OwnerClientId == NetworkingManager.singleton.MyClientId || (OwnerClientId == -1 && NetworkingManager.singleton.isHost));
            }
        }

        public bool IsOwner
        {
            get
            {
                return !isPlayerObject && (OwnerClientId == NetworkingManager.singleton.MyClientId || (OwnerClientId == -1 && NetworkingManager.singleton.isHost));
            }
        }

        private void OnDestroy()
        {
            SpawnManager.OnDestroyObject(NetworkId, false);
        }

        internal bool isSpawned = false;

        public void Spawn()
        {
            SpawnManager.OnSpawnObject(this);
        }

        public void SpawnWithOwnership(int clientId)
        {
            SpawnManager.OnSpawnObject(this, clientId);
        }

        internal void InvokeBehaviourNetworkSpawn()
        {
            NetworkedBehaviour[] netBehaviours = GetComponentsInChildren<NetworkedBehaviour>();
            for (int i = 0; i < netBehaviours.Length; i++)
            {
                //We check if we are it's networkedObject owner incase a networkedObject exists as a child of our networkedObject.
                if(netBehaviours[i].networkedObject == this && !netBehaviours[i].networkedStartInvoked)
                {
                    netBehaviours[i].NetworkStart();
                }
            }
        }
    }
}