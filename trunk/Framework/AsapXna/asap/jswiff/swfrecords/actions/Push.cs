using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Pushes one or more values to the stack. Add <code>Push.StackValue</code>
     * instances using <code>addValue()</code>.
     * </p>
     * 
     * <p>
     * Performed stack operations: addition of one or more values to stack.
     * </p>
     * 
     * <p>
     * ActionScript equivalent: none (used internally, e.g. for parameter passing).
     * </p>
     * 
     * @since SWF 4
     */
    public class Push : ActionRecord
    {
        private List<Push.StackValue>  values = new List<Push.StackValue> ();
        
        /** 
         * Creates a new Push action.
         */
        public Push() 
        {
            code = ActionConstants.PUSH;
        }
        
        public Push(InputBitStream stream) /* throws IOException */ 
        {
            code = ActionConstants.PUSH;
            while ((stream.Available()) > 0) 
            {
                StackValue value = new StackValue(stream);
                values.Add(value);
            }
        }
        
        public override int GetSize()
        {
            int size = 3;
            foreach (StackValue stackValue in values) 
            {
                size += stackValue.GetSize();
            }
            return size;
        }
        
        public virtual List<Push.StackValue>  GetValues()
        {
            return values;
        }
        
        public virtual void AddValue(StackValue value)
        {
            values.Add(value);
        }
        
        public override String ToString()
        {
            return "Push";
        }
        
        /** 
         * This class contains a value which can be pushed to the stack. The default
         * value is <code>undefined</code>, the type is <code>TYPE_UNDEFINED</code>.
         * Use setters to change.
         */
        public class StackValue
        {
            /** 
             *
             */
            public const short TYPE_STRING = 0;
            
            /** 
             *
             */
            public const short TYPE_FLOAT = 1;
            
            /** 
             *
             */
            public const short TYPE_NULL = 2;
            
            /** 
             *
             */
            public const short TYPE_UNDEFINED = 3;
            
            /** 
             *
             */
            public const short TYPE_REGISTER = 4;
            
            /** 
             *
             */
            public const short TYPE_BOOLEAN = 5;
            
            /** 
             * Indicates that the value to be pushed is double-precision floating
             * point number.
             */
            public const short TYPE_DOUBLE = 6;
            
            /** 
             *
             */
            public const short TYPE_INTEGER = 7;
            
            /** 
             * Indicates that the value to be pushed is an 8-bit constant pool
             * index.
             */
            public const short TYPE_CONSTANT_8 = 8;
            
            /** 
             * Indicates that the value to be pushed is a 16-bit constant pool
             * index.
             */
            public const short TYPE_CONSTANT_16 = 9;
            
            private short type = StackValue.TYPE_UNDEFINED;
            
            private String _string;
            
            private float floatValue;
            
            private short registerNumber;
            
            private bool booleanValue;
            
            private double doubleValue;
            
            private long integerValue;
            
            private short constant8;
            
            private int constant16;
            
            /** 
             * Creates a new StackValue instance. Initial type is
             * <code>TYPE_UNDEFINED</code>.
             */
            public StackValue() 
            {
            }
            
            public StackValue(InputBitStream stream) /* throws IOException */ 
            {
                type = stream.ReadUI8();
                switch (type)
                {
                    case StackValue.TYPE_STRING:
                        _string = stream.ReadString();
                        break;
                    case StackValue.TYPE_FLOAT:
                        floatValue = stream.ReadFloat();
                        break;
                    case StackValue.TYPE_REGISTER:
                        registerNumber = stream.ReadUI8();
                        break;
                    case StackValue.TYPE_BOOLEAN:
                        booleanValue = (stream.ReadUI8()) != 0;
                        break;
                    case StackValue.TYPE_DOUBLE:
                        doubleValue = stream.ReadDouble();
                        break;
                    case StackValue.TYPE_INTEGER:
                        integerValue = stream.ReadUI32();
                        break;
                    case StackValue.TYPE_CONSTANT_8:
                        constant8 = stream.ReadUI8();
                        break;
                    case StackValue.TYPE_CONSTANT_16:
                        constant16 = stream.ReadUI16();
                        break;
                }
            }
            
            public virtual void SetBoolean(bool value)
            {
                this.booleanValue = value;
                type = StackValue.TYPE_BOOLEAN;
            }
            
            public virtual bool GetBoolean()
            {
                return booleanValue;
            }
            
            public virtual void SetConstant16(int value)
            {
                this.constant16 = value;
                type = StackValue.TYPE_CONSTANT_16;
            }
            
            public virtual int GetConstant16()
            {
                if ((type) != (StackValue.TYPE_CONSTANT_16)) 
                {
                    throw new InvalidOperationException("Value type is not TYPE_CONSTANT_16!");
                } 
                return constant16;
            }
            
            public virtual void SetConstant8(short value)
            {
                this.constant8 = value;
                type = StackValue.TYPE_CONSTANT_8;
            }
            
            public virtual short GetConstant8()
            {
                if ((type) != (StackValue.TYPE_CONSTANT_8)) 
                {
                    throw new InvalidOperationException("Value type is not TYPE_CONSTANT_8!");
                } 
                return constant8;
            }
            
            public virtual void SetDouble(double value)
            {
                this.doubleValue = value;
                type = StackValue.TYPE_DOUBLE;
            }
            
            public virtual double GetDouble()
            {
                if ((type) != (StackValue.TYPE_DOUBLE)) 
                {
                    throw new InvalidOperationException("Value type is not TYPE_DOUBLE!");
                } 
                return doubleValue;
            }
            
            public virtual void SetFloat(float value)
            {
                this.floatValue = value;
                type = StackValue.TYPE_FLOAT;
            }
            
            public virtual float GetFloat()
            {
                if ((type) != (StackValue.TYPE_FLOAT)) 
                {
                    throw new InvalidOperationException("Value type is not TYPE_FLOAT!");
                } 
                return floatValue;
            }
            
            public virtual void SetInteger(long value)
            {
                this.integerValue = value;
                type = StackValue.TYPE_INTEGER;
            }
            
            public virtual long GetInteger()
            {
                if ((type) != (StackValue.TYPE_INTEGER)) 
                {
                    throw new InvalidOperationException("Value type is not TYPE_INTEGER!");
                } 
                return integerValue;
            }
            
            public virtual void SetNull()
            {
                type = StackValue.TYPE_NULL;
            }
            
            public virtual bool IsNull()
            {
                return (type) == (StackValue.TYPE_NULL);
            }
            
            public virtual void SetRegisterNumber(short value)
            {
                this.registerNumber = value;
                type = StackValue.TYPE_REGISTER;
            }
            
            public virtual short GetRegisterNumber()
            {
                if ((type) != (StackValue.TYPE_REGISTER)) 
                {
                    throw new InvalidOperationException("Value type is not TYPE_REGISTER!");
                } 
                return registerNumber;
            }
            
            public virtual void SetString(String value)
            {
                this._string = value;
                type = StackValue.TYPE_STRING;
            }
            
            public virtual String GetString()
            {
                if ((type) != (StackValue.TYPE_STRING)) 
                {
                    throw new InvalidOperationException("Value type is not TYPE_STRING!");
                } 
                return _string;
            }
            
            public virtual short _getType()
            {
                return type;
            }
            
            public virtual void SetUndefined()
            {
                type = StackValue.TYPE_UNDEFINED;
            }
            
            public virtual bool IsUndefined()
            {
                return (type) == (StackValue.TYPE_UNDEFINED);
            }
            
            public override String ToString()
            {
                String result = "";
                switch (type)
                {
                    case StackValue.TYPE_STRING:
                        result += ("string: \'" + (_string)) + "\'";
                        break;
                    case StackValue.TYPE_FLOAT:
                        result += "float: " + (floatValue);
                        break;
                    case StackValue.TYPE_REGISTER:
                        result += "register: " + (registerNumber);
                        break;
                    case StackValue.TYPE_BOOLEAN:
                        result += "boolean: " + (booleanValue);
                        break;
                    case StackValue.TYPE_DOUBLE:
                        result += "double: " + (doubleValue);
                        break;
                    case StackValue.TYPE_INTEGER:
                        result += "integer: " + (integerValue);
                        break;
                    case StackValue.TYPE_CONSTANT_8:
                        result += ("c8[" + (constant8)) + "]";
                        break;
                    case StackValue.TYPE_CONSTANT_16:
                        result += ("c16[" + (constant16)) + "]";
                        break;
                    case StackValue.TYPE_UNDEFINED:
                        result += "undefined";
                        break;
                    case StackValue.TYPE_NULL:
                        result += "null";
                        break;
                }
                return result;
            }
            
            public virtual int GetSize()
            {
                int size = 1;
                switch (type)
                {
                    case StackValue.TYPE_STRING:                        
                        size += (System.Text.Encoding.UTF8.GetBytes(_string).Length) + 1;                        
                        break;
                    case StackValue.TYPE_FLOAT:
                        size += 4;
                        break;
                    case StackValue.TYPE_REGISTER:
                        size++;
                        break;
                    case StackValue.TYPE_BOOLEAN:
                        size++;
                        break;
                    case StackValue.TYPE_DOUBLE:
                        size += 8;
                        break;
                    case StackValue.TYPE_INTEGER:
                        size += 4;
                        break;
                    case StackValue.TYPE_CONSTANT_8:
                        size++;
                        break;
                    case StackValue.TYPE_CONSTANT_16:
                        size += 2;
                        break;
                }
                return size;
            }
        }
    }
}