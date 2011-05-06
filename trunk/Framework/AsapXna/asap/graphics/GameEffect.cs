using asap.resources;
using asap.util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace asap.graphics
{
    public class GameEffect : ManagedResource
    {
        private Effect effect;

        public GameEffect(Effect effect)
        {
            this.effect = effect;
        }
                
        protected void SetValue(string name, bool value)
        {
            effect.Parameters[name].SetValue(value);
        }

        protected void SetValue(string name, bool[] value)
        {
            effect.Parameters[name].SetValue(value);
        }

        protected void SetValue(string name, float value)
        {
            effect.Parameters[name].SetValue(value);
        }

        protected void SetValue(string name, float[] value)
        {
            effect.Parameters[name].SetValue(value);
        }

        protected void SetValue(string name, int value)
        {
            effect.Parameters[name].SetValue(value);
        }

        protected void SetValue(string name, int[] value)
        {
            effect.Parameters[name].SetValue(value);
        }

        protected void SetValue(string name, Matrix value)
        {
            effect.Parameters[name].SetValue(value);
        }

        protected void SetValue(string name, Matrix[] value)
        {
            effect.Parameters[name].SetValue(value);
        }

        protected void SetValue(string name, Quaternion value)
        {
            effect.Parameters[name].SetValue(value);
        }

        protected void SetValue(string name, Quaternion[] value)
        {
            effect.Parameters[name].SetValue(value);
        }

        protected void SetValue(string name, string value)
        {
            effect.Parameters[name].SetValue(value);
        }

        protected void SetValue(string name, Texture value)
        {
            effect.Parameters[name].SetValue(value);
        }

        protected void SetValue(string name, Vector2 value)
        {
            effect.Parameters[name].SetValue(value);
        }

        protected void SetValue(string name, Vector2[] value)
        {
            effect.Parameters[name].SetValue(value);
        }

        protected void SetValue(string name, Vector3 value)
        {
            effect.Parameters[name].SetValue(value);
        }

        protected void SetValue(string name, Vector3[] value)
        {
            effect.Parameters[name].SetValue(value);
        }

        protected void SetValue(string name, Vector4 value)
        {
            effect.Parameters[name].SetValue(value);
        }

        protected void SetValue(string name, Vector4[] value)
        {
            effect.Parameters[name].SetValue(value);
        }

        public void SetValueTranspose(string name, Matrix value)
        {
            effect.Parameters[name].SetValue(value);
        }

        public void SetValueTranspose(string name, Matrix[] value)
        {
            effect.Parameters[name].SetValue(value);
        }

        public void SetValue(string name, Color4 value)
        {
            effect.Parameters[name].SetValue(value.ToVector());
        }

        public Effect Effect 
        {
            get { return effect; }
        }

        public override void Dispose()
        {
            if (effect != null)
            {
                effect.Dispose();
                effect = null;
            }
        }
    }
}
