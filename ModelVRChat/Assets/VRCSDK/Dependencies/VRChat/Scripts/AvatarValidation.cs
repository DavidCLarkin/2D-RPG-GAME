using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace VRCSDK2
{
    public static class AvatarValidation
    {
        public static readonly string[] ComponentTypeWhiteList = new string[] {
            "UnityEngine.Transform",
            "UnityEngine.Animator",
            "VRC.Core.PipelineManager",
#if !VRC_CLIENT
            "VRC.Core.PipelineSaver",
#endif
            "VRCSDK2.VRC_AvatarDescriptor",
            "NetworkMetadata",
            "RootMotion.FinalIK.IKExecutionOrder",
            "RootMotion.FinalIK.VRIK",
            "RootMotion.FinalIK.FullBodyBipedIK",
            "RootMotion.FinalIK.LimbIK",
            "RootMotion.FinalIK.AimIK",
            "RootMotion.FinalIK.BipedIK",
            "RootMotion.FinalIK.GrounderIK",
            "RootMotion.FinalIK.GrounderFBBIK",
            "RootMotion.FinalIK.GrounderVRIK",
            "RootMotion.FinalIK.GrounderQuadruped",
            "RootMotion.FinalIK.TwistRelaxer",
            "RootMotion.FinalIK.ShoulderRotator",
            "RootMotion.FinalIK.FBBIKArmBending",
            "RootMotion.FinalIK.FBBIKHeadEffector",
            "RootMotion.FinalIK.FABRIK",
            "RootMotion.FinalIK.FABRIKChain",
            "RootMotion.FinalIK.FABRIKRoot",
            "RootMotion.FinalIK.CCDIK",
            "RootMotion.FinalIK.RotationLimit",
            "RootMotion.FinalIK.RotationLimitHinge",
            "RootMotion.FinalIK.RotationLimitPolygonal",
            "RootMotion.FinalIK.RotationLimitSpline",
            "UnityEngine.SkinnedMeshRenderer",
            "LimbIK", // our limbik based on Unity ik
            "AvatarAnimation",
            "LoadingAvatarTextureAnimation",
            "UnityEngine.MeshFilter",
            "UnityEngine.MeshRenderer",
            "UnityEngine.Animation",
            "UnityEngine.ParticleSystem",
            "UnityEngine.ParticleSystemRenderer",
            "DynamicBone",
            "DynamicBoneCollider",
            "UnityEngine.TrailRenderer",
            "UnityEngine.Cloth",
            "UnityEngine.Light",
            "UnityEngine.Collider",
            "UnityEngine.Rigidbody",
            "UnityEngine.Joint",
            "UnityEngine.Camera",
            "UnityEngine.FlareLayer",
            "UnityEngine.GUILayer",
            "UnityEngine.AudioSource",
            "ONSPAudioSource",
            "UnityEngine.EllipsoidParticleEmitter",
            "UnityEngine.ParticleRenderer",
            "UnityEngine.ParticleAnimator",
            "UnityEngine.MeshParticleEmitter",
            "UnityEngine.LineRenderer"
        };

        private static IEnumerable<System.Type> FindTypes()
        {
            Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();

            return ComponentTypeWhiteList.Select((name) =>
            {
                foreach (Assembly a in assemblies)
                {
                    System.Type found = a.GetType(name);
                    if (found != null)
                        return found;
                }

                //This is really verbose for some SDK scenes, eg.
                //If they don't have FinalIK installed
#if VRC_CLIENT && UNITY_EDITOR
                Debug.LogError("Could not find type " + name);
#endif
                return null;
            }).Where(t => t != null);
        }

        public static IEnumerable<Component> FindIllegalComponents(string Name, GameObject currentAvatar)
        {
            HashSet<System.Type> typesInUse = new HashSet<System.Type>();
            List<Component> componentsInUse = new List<Component>();
            Queue<GameObject> children = new Queue<GameObject>();
            children.Enqueue(currentAvatar.gameObject);
            while (children.Count > 0)
            {
                GameObject child = children.Dequeue();
                int childCount = child.transform.childCount;
                for (int idx = 0; idx < child.transform.childCount; ++idx)
                    children.Enqueue(child.transform.GetChild(idx).gameObject);
                foreach (Component c in child.transform.GetComponents<Component>())
                {
                    if (c == null)
                        continue;

                    if (typesInUse.Contains(c.GetType()) == false)
                        typesInUse.Add(c.GetType());
                    componentsInUse.Add(c);
                }
            }

            IEnumerable<System.Type> foundTypes = FindTypes();
            return componentsInUse.Where(c => !foundTypes.Any(allowedType => c != null && (c.GetType() == allowedType || c.GetType().IsSubclassOf(allowedType))));
        }

        public static void RemoveIllegalComponents(string Name, GameObject currentAvatar, bool retry = true)
        {
            IEnumerable<Component> componentsToRemove = VRCSDK2.AvatarValidation.FindIllegalComponents(Name, currentAvatar);

            HashSet<string> componentsToRemoveNames = new HashSet<string>();
            foreach (Component c in componentsToRemove)
            {
                if (componentsToRemoveNames.Contains(c.GetType().Name) == false)
                    componentsToRemoveNames.Add(c.GetType().Name);
                Object.DestroyImmediate(c);
            }

            if (retry && componentsToRemoveNames.Count > 0)
            {
                Debug.LogErrorFormat("Avatar {0} had components of the following types removed: {1}", Name, string.Join(", ", componentsToRemoveNames.ToArray()));

                // Call again, to see if there's components that were prevented from being removed
                RemoveIllegalComponents(Name, currentAvatar, false);
            }
        }

        public static bool EnforceAudioSourceLimits(GameObject currentAvatar)
        {
            bool found = false;
            Queue<GameObject> children = new Queue<GameObject>();
            children.Enqueue(currentAvatar.gameObject);
            while (children.Count > 0)
            {
                GameObject child = children.Dequeue();
                int childCount = child.transform.childCount;
                for (int idx = 0; idx < child.transform.childCount; ++idx)
                    children.Enqueue(child.transform.GetChild(idx).gameObject);
                foreach (AudioSource au in child.transform.GetComponents<AudioSource>())
                {
                    if (au.volume > 0.5f) au.volume = 0.5f;
                    au.spatialize = true;
                    au.bypassEffects = false;
                    au.bypassListenerEffects = false;
                    au.spatialBlend = 1f;
                    ONSPAudioSource oa = au.gameObject.GetOrAddComponent<ONSPAudioSource>();
                    oa.enabled = true;
                    if (oa.Gain > 10f) oa.Gain = 10f;
                    if (oa.Near > 1f) oa.Near = 1f;
                    if (oa.Far > 100f) oa.Far = 30f;
                    oa.UseInvSqr = true; // This is the ENABLED value for OCULUS ATTENUATION
                    oa.EnableSpatialization = true;
                    oa.EnableRfl = false;
                    oa.VolumetricRadius = 0f;
                    found = true;
                }
            }

            return found;
        }
    }
}
