using UnityEngine;


namespace RigidTransformUtils
{
    public static class TransformExtensions
    {
        public static RigidTransform LocalRigidTransform(this Transform transform)
        {
            return new RigidTransform(transform.localRotation, transform.localPosition);

        }
        public static RigidTransform WorldRigidTransform(this Transform transform)
        {
            return new RigidTransform(transform.rotation, transform.position);
        }
    }
}

