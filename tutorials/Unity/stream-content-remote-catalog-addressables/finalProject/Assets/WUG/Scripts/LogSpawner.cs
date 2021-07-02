using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets; 

namespace Assets.WUG.Scripts
{
    public class LogSpawner : MonoBehaviour
    {
        [SerializeField]
        private bool m_Spawning;
        
        [SerializeField]
        private Transform m_SpawnPosition;

        [SerializeField] 
        private AssetReference m_LogPrefab;

        private Texture m_LogTexture;
        private int m_LogCount = 0;

        private IEnumerator Start()
        {
            float waitTime;
            Addressables.LoadAssetAsync<Texture>("LogTexture").Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    m_LogTexture = handle.Result;
                }
            };

            while (m_Spawning)
            {
                waitTime = UnityEngine.Random.Range(2f, 5f);

                Addressables.InstantiateAsync(m_LogPrefab, m_SpawnPosition.position, Quaternion.identity, transform, true).Completed += Spawn_Completed;

                m_LogCount++;
                yield return new WaitForSeconds(waitTime);
            }
        }

        private void Spawn_Completed(AsyncOperationHandle<GameObject> handle)
        {
            if (m_LogTexture != null & m_LogCount % 2 == 0)
            {
                handle.Result.GetComponentInChildren<MeshRenderer>().material.mainTexture = m_LogTexture;
            }
        }
    }
}
