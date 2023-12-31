using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Beamable;
using Beamable.Api.Autogenerated.Accounts;
using Helpers;
using Models;
using UnityEngine;
using Wrappers;

namespace Backend.Managers
{
    public class BeamableManager : MonoBehaviour, IBackendManager
    {
        public static BeamableManager Instance { get; private set; }

        private BeamContext _beamableContext;
        
        

        public void InstantiateSingleton()
        {
            if (Instance is null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }

        void Start()
        {
            InstantiateSingleton();

            var authDetails = new AuthenticationDetails
            {
                type = AuthenticationType.Email,
                email = "samuel@dev.net",
                password = "my_password"
            };

            var jsonString = JsonUtility.ToJson(authDetails);

            Debug.Log(jsonString);
            
            PlayerPrefsWrapper.SaveObject(authDetails, true);

            var loadedDetails = PlayerPrefsWrapper.LoadObject<AuthenticationDetails>(true);
            
            Debug.Log(loadedDetails.type);
            Debug.Log(loadedDetails.email);
            Debug.Log(loadedDetails.password);
        }

        void Update()
        {

        }

        public async Task SignIn()
        {
            _beamableContext = BeamContext.Default;
            await _beamableContext.OnReady;
            
            _beamableContext.Accounts.RecoverAccountWithDeviceId();
        }

        public Task SignOut()
        {
            throw new System.NotImplementedException();
        }
    }
}


