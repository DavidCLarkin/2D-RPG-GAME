using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VRC.Core;

namespace VRCSDK2
{
    public class RuntimeBlueprintCreation : RuntimeAPICreation 
    {
        public GameObject waitingPanel;
        public GameObject blueprintPanel;
        public GameObject errorPanel;
        
		public Text titleText;
        public InputField blueprintName;
        public InputField blueprintDescription;
        public RawImage bpImage;
        public Image liveBpImage;
        public Toggle shouldUpdateImageToggle;
        public Toggle contentSex;
        public Toggle contentViolence;
        public Toggle contentGore;
        public Toggle contentOther;
		public Toggle developerAvatar;

        public UnityEngine.UI.Button uploadButton;

        private ApiAvatar apiAvatar;

        #if UNITY_EDITOR       
        new void Start()
        {
            if (!Application.isEditor || !Application.isPlaying)
                return;

            base.Start();

            Application.runInBackground = true;
            UnityEngine.VR.VRSettings.enabled = false;

            uploadButton.onClick.AddListener(SetupUpload);
               
            shouldUpdateImageToggle.onValueChanged.AddListener(ToggleUpdateImage);
                
            Login();
            SetupUI();
        }
        
        void LoginErrorCallback(string obj)
        {
            VRC.Core.Logger.LogError("Could not fetch fresh user - " + obj, DebugLevel.Always);    
        }

        void Login()
        {
            ApiCredentials.Load();
            APIUser.Login(
                delegate(APIUser user) 
                {
                    pipelineManager.user = user;
                    if (isUpdate)
                    {
                        ApiAvatar.Fetch(pipelineManager.blueprintId, false,
                            delegate (ApiAvatar avatar)
                            {
                                apiAvatar = avatar;
                                SetupUI();
                            }, 
                            delegate(string message) 
                            {
                                pipelineManager.blueprintId = "";
                                SetupUI();
                            });
                    }
                    else
                    {
                        SetupUI();
                    }
                }, LoginErrorCallback);
        }
        
        void SetupUI()
        {
            if( APIUser.Exists(pipelineManager.user) )
            {
                waitingPanel.SetActive(false);
                blueprintPanel.SetActive(true);
                errorPanel.SetActive(false);

                if (isUpdate)
                {
                    // bp update
                    if (apiAvatar.authorId == pipelineManager.user.id)
                    {
						titleText.text= "Update Avatar";
                        // apiAvatar = pipelineManager.user.GetBlueprint(pipelineManager.blueprintId) as ApiAvatar;
                        blueprintName.text = apiAvatar.name;
                        contentSex.isOn = apiAvatar.tags.Contains("content_sex");
                        contentViolence.isOn = apiAvatar.tags.Contains("content_violence");
                        contentGore.isOn = apiAvatar.tags.Contains("content_gore");
                        contentOther.isOn = apiAvatar.tags.Contains("content_other");
						developerAvatar.isOn = apiAvatar.tags.Contains("developer");
                        blueprintDescription.text = apiAvatar.description;
                        shouldUpdateImageToggle.interactable = true;
                        shouldUpdateImageToggle.isOn = false;
                        liveBpImage.enabled = false;
                        bpImage.enabled = true;

                        ImageDownloader.DownloadImage(apiAvatar.imageUrl, delegate(Texture2D obj) {
                            bpImage.texture = obj;
                        });
                    }
                    else // user does not own apiAvatar id associated with descriptor
                    {
                        blueprintPanel.SetActive(false);
                        errorPanel.SetActive(true);
                    }
                }
                else
                {
					titleText.text = "New Avatar";
                    shouldUpdateImageToggle.interactable = false;
                    shouldUpdateImageToggle.isOn = true;
                    liveBpImage.enabled = true;
                    bpImage.enabled = false;
                }
            }
            else
            {
                waitingPanel.SetActive(true);
                blueprintPanel.SetActive(false);
                errorPanel.SetActive(false);
            }

			if(APIUser.CurrentUser != null && APIUser.CurrentUser.developerType > APIUser.DeveloperType.None)
				developerAvatar.gameObject.SetActive(true);
			else
				developerAvatar.gameObject.SetActive(false);
        }
        
        public void SetupUpload()
        {
            uploadTitle = "Preparing For Upload";
            isUploading = true;
           
            string abPath = UnityEditor.EditorPrefs.GetString("currentBuildingAssetBundlePath");

			string unityPackagePath = UnityEditor.EditorPrefs.GetString("VRC_exportedUnityPackagePath");

            UnityEditor.EditorPrefs.SetBool("VRCSDK2_scene_changed", true );
            UnityEditor.EditorPrefs.SetBool("VRCSDK2_content_sex", contentSex.isOn);
            UnityEditor.EditorPrefs.SetBool("VRCSDK2_content_violence", contentViolence.isOn);
            UnityEditor.EditorPrefs.SetBool("VRCSDK2_content_gore", contentGore.isOn);
            UnityEditor.EditorPrefs.SetBool("VRCSDK2_content_other", contentOther.isOn);

            string avatarId = isUpdate ? apiAvatar.id : "avatar_new_" + System.Guid.NewGuid().ToString();
            int version = isUpdate ? apiAvatar.version+1 : 1;
            PrepareVRCPathForS3(abPath, avatarId, version, ApiAvatar.VERSION);

			if(!string.IsNullOrEmpty(unityPackagePath) && System.IO.File.Exists(unityPackagePath))
			{
				Debug.Log("Found unity package path. Preparing to upload!");
				PrepareUnityPackageForS3(unityPackagePath, avatarId, version, ApiAvatar.VERSION);
			}

            if (useFileApi)
                StartCoroutine(UploadNew());
            else
                StartCoroutine(Upload(blueprintName, "avatars"));
        }

        IEnumerator UploadNew()
        {
            bool caughtInvalidInput = false;
            if (!ValidateNameInput(blueprintName))
                caughtInvalidInput = true;

            if (caughtInvalidInput)
                yield break;

            // upload unity package
            if (!string.IsNullOrEmpty(uploadUnityPackagePath))
            {
                yield return StartCoroutine(UploadFile(uploadUnityPackagePath, isUpdate ? apiAvatar.unityPackageUrl : "", GetFriendlyAvatarFileName("Unity package"), "Unity package",
                    delegate (string fileUrl)
                    {
                        cloudFrontUnityPackageUrl = fileUrl;
                    }
                ));
            }

            // upload asset bundle
            if (!string.IsNullOrEmpty(uploadVrcPath))
            {
                yield return StartCoroutine(UploadFile(uploadVrcPath, isUpdate ? apiAvatar.assetUrl : "", GetFriendlyAvatarFileName("Asset bundle"), "Asset bundle",
                    delegate (string fileUrl)
                    {
                        cloudFrontAssetUrl = fileUrl;
                    }
                ));
            }

            if (isUpdate)
                yield return StartCoroutine(UpdateBlueprint());
            else
                yield return StartCoroutine(CreateBlueprint());

            OnSDKPipelineComplete();
        }

        private string GetFriendlyAvatarFileName(string type)
        {
            return "Avatar - " + blueprintName.text + " - " + type + " - " + Application.unityVersion + "_" + ApiWorld.VERSION.ApiVersion +
                   "_" + ApiModel.GetAssetPlatformString() + "_" + ApiModel.GetServerEnvironmentForApiUrl();
        }

        List<string> BuildTags()
        {
            var tags = new List<string>();
            if (contentSex.isOn)
                tags.Add("content_sex");
            if (contentViolence.isOn)
                tags.Add("content_violence");
            if (contentGore.isOn)
                tags.Add("content_gore");
            if (contentOther.isOn)
                tags.Add("content_other");

            if(APIUser.CurrentUser.developerType > APIUser.DeveloperType.None)
            {
                if (developerAvatar.isOn)
                    tags.Add("developer");
            }

            return tags;
        }

        protected override IEnumerator CreateBlueprint()
        {
            yield return StartCoroutine(UpdateImage(isUpdate ? apiAvatar.imageUrl : "", GetFriendlyAvatarFileName("Image")));

            ApiAvatar avatar = new ApiAvatar();
            avatar.Init(
                pipelineManager.user,
                blueprintName.text,
                cloudFrontImageUrl,
                cloudFrontAssetUrl,
                blueprintDescription.text,
                BuildTags(),
                cloudFrontUnityPackageUrl
                );

            bool doneUploading = false;

            avatar.Save(delegate(ApiModel model)
            {
                ApiAvatar savedBP = (ApiAvatar)model;
                pipelineManager.blueprintId = savedBP.id;
                UnityEditor.EditorPrefs.SetString("blueprintID-" + pipelineManager.GetInstanceID().ToString(), savedBP.id);
                doneUploading = true;
            });

            while (!doneUploading)
                yield return null;
        }

        protected override IEnumerator UpdateBlueprint()
        {
            bool doneUploading = false;

            apiAvatar.name = blueprintName.text;
            apiAvatar.description = blueprintDescription.text;
            apiAvatar.assetUrl = cloudFrontAssetUrl;
            apiAvatar.tags = BuildTags();
            apiAvatar.unityPackageUrl = cloudFrontUnityPackageUrl;
			apiAvatar.UpdateVersionAndPlatform();

            if (shouldUpdateImageToggle.isOn)
            {
                yield return StartCoroutine(UpdateImage(isUpdate ? apiAvatar.imageUrl : "", GetFriendlyAvatarFileName("Image")));
                apiAvatar.imageUrl = cloudFrontImageUrl;
                SetUploadProgress("Saving Avatar", "Almost finished!!", 0.8f);
                apiAvatar.Save(delegate(ApiModel model) 
                {
                    doneUploading = true;
                });
            }
            else
            {
                SetUploadProgress("Saving Avatar", "Almost finished!!", 0.8f);
                apiAvatar.Save(delegate(ApiModel model) 
                {
                    doneUploading = true;
                });
            }

            while (!doneUploading)
                yield return null;
        }
        
        void ToggleUpdateImage(bool isOn)
        {
            if(isOn)
            {
                bpImage.enabled = false;
                liveBpImage.enabled = true;
            }
            else
            {
                bpImage.enabled = true;
                liveBpImage.enabled = false;
                ImageDownloader.DownloadImage(apiAvatar.imageUrl, delegate(Texture2D obj) {
                    bpImage.texture = obj;
                });
            }
        }
        
        void OnDestroy()
        {
            UnityEditor.EditorUtility.ClearProgressBar();
            UnityEditor.EditorPrefs.DeleteKey("currentBuildingAssetBundlePath");
            UnityEditor.EditorPrefs.DeleteKey("externalPluginPath");
        }
        #endif
    }
}


