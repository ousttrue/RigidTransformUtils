using System;
using System.Runtime.Serialization;
using UnityEngine;


namespace RigidTransformUtils
{
    [Serializable, DataContract]
    public struct RigidTransform
    {
        public readonly Quaternion Rotation;

        public readonly Vector3 Translation;

        public RigidTransform(Quaternion r) : this(r, Vector3.zero)
        { }

        public RigidTransform(Vector3 t) : this(Quaternion.identity, t)
        { }

        public RigidTransform(Quaternion r, Vector3 t)
        {
            Rotation = r;
            Translation = t;
        }

        public override string ToString()
        {
            return String.Format("[{0}, {1}]", Rotation, Translation);
        }

        public static RigidTransform Identity {
            get
            {
                return  new RigidTransform(Quaternion.identity, Vector3.zero);
            }
        }

        public static RigidTransform operator *(RigidTransform c1, RigidTransform c2)
        {
            return new RigidTransform(
                c1.Rotation * c2.Rotation
                , c1.Rotation * c2.Translation + c1.Translation);
        }

        public RigidTransform Inverse()
        {
            var inverse = Quaternion.Inverse(Rotation);
            return new RigidTransform(inverse, inverse * (-Translation));
        }

        public RigidTransform LocalFrom(RigidTransform rt)
        {
            return rt.Inverse() * this;
        }

        public Vector3 Apply(Vector3 v)
        {
            return (Rotation * v) + Translation;
        }

        public void ApplyWorld(Transform target)
        {
            target.position = Translation;
            target.rotation = Rotation;
        }

        public void ApplyLocal(Transform target)
        {
            target.localPosition = Translation;
            target.localRotation = Rotation;
        }

        public bool Approximately(RigidTransform r)
        {
            if (!Rotation.Approximately(r.Rotation)) return false;
            if (!Translation.Approximately(r.Translation)) return false;
            return true;
        }
    }
}
