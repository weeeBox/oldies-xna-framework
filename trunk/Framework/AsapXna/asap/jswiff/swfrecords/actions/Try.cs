using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * This action defines handlers for exceptional conditions (exceptions). After
     * defining the mandatory try block, you can define a catch block and/or a
     * finally block.
     * </p>
     * 
     * <p>
     * Performed stack operations: none
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>try..catch..finally</code> keywords
     * </p>
     * 
     * @since SWF 7
     */
    public class Try : ActionRecord
    {
        private bool catchInRegister;
        
        private String catchVariable;
        
        private short catchRegister;
        
        private ActionBlock tryBlock;
        
        private ActionBlock catchBlock;
        
        private ActionBlock finallyBlock;
        
        /** 
         * Creates a new Try action. You can specify a name of a variable the caught
         * object is put into.
         * 
         * @param catchVar
         *            catch variable name
         */
        public Try(String catchVar) 
        {            
            this.catchVariable = catchVar;
        }
        
        /** 
         * Creates a new Try action. You can specify the register number the caught
         * object is put into.
         * 
         * @param catchRegister
         *            catch register number
         */
        public Try(short catchRegister) 
        {            
            this.catchRegister = catchRegister;
            catchInRegister = true;
        }
        
        public Try(InputBitStream stream ,InputBitStream mainStream) /* throws IOException */ 
        {
            code = ActionConstants.TRY;
            short flags = stream.ReadUI8();
            catchInRegister = (flags & 4) != 0;
            bool hasFinallyBlock = (flags & 2) != 0;
            bool hasCatchBlock = (flags & 1) != 0;
            int trySize = stream.ReadUI16();
            int catchSize = stream.ReadUI16();
            int finallySize = stream.ReadUI16();
            if (catchInRegister) 
            {
                catchRegister = stream.ReadUI8();
            } 
            else 
            {
                catchVariable = stream.ReadString();
            }
            byte[] blockBuffer = mainStream.ReadBytes(trySize);
            InputBitStream blockStream = new InputBitStream(blockBuffer);
            blockStream.SetANSI(stream.IsANSI());
            blockStream.SetShiftJIS(stream.IsShiftJIS());
            tryBlock = new ActionBlock(blockStream);
            RemoveTryJump();
            if (hasCatchBlock) 
            {
                blockBuffer = mainStream.ReadBytes(catchSize);
                blockStream = new InputBitStream(blockBuffer);
                blockStream.SetANSI(stream.IsANSI());
                blockStream.SetShiftJIS(stream.IsShiftJIS());
                catchBlock = new ActionBlock(blockStream);
            } 
            else 
            {
                catchBlock = new ActionBlock();
            }
            if (hasFinallyBlock) 
            {
                blockBuffer = mainStream.ReadBytes(finallySize);
                blockStream = new InputBitStream(blockBuffer);
                blockStream.SetANSI(stream.IsANSI());
                blockStream.SetShiftJIS(stream.IsShiftJIS());
                finallyBlock = new ActionBlock(blockStream);
            } 
            else 
            {
                finallyBlock = new ActionBlock();
            }
        }
        
        private Try() 
        {
            code = ActionConstants.TRY;
            tryBlock = new ActionBlock();
            catchBlock = new ActionBlock();
            finallyBlock = new ActionBlock();
        }
        
        public virtual ActionBlock GetCatchBlock()
        {
            return catchBlock;
        }
        
        public virtual short GetCatchRegister()
        {
            return catchRegister;
        }
        
        public virtual String GetCatchVariable()
        {
            return catchVariable;
        }
        
        public virtual ActionBlock GetFinallyBlock()
        {
            return finallyBlock;
        }
        
        public override int GetSize()
        {
            int size = ((15 + (tryBlock.GetSize())) + (catchBlock.GetSize())) + (finallyBlock.GetSize());
            if (catchInRegister) 
            {
                size++;
            } 
            else 
            {                
                size += (System.Text.Encoding.UTF8.GetBytes(catchVariable).Length) + 1;                
            }
            return size;
        }
        
        public virtual ActionBlock GetTryBlock()
        {
            return tryBlock;
        }
        
        public virtual void AddToCatch(ActionRecord action)
        {
            catchBlock.AddAction(action);
        }
        
        public virtual void AddToFinally(ActionRecord action)
        {
            finallyBlock.AddAction(action);
        }
        
        public virtual void AddToTry(ActionRecord action)
        {
            tryBlock.AddAction(action);
        }
        
        public virtual bool CatchInRegister()
        {
            return catchInRegister;
        }
        
        public virtual bool HasCatchBlock()
        {
            return (catchBlock.GetActions().Count) > 0;
        }
        
        public virtual bool HasFinallyBlock()
        {
            return (finallyBlock.GetActions().Count) > 0;
        }
        
        private void RemoveTryJump()
        {
            List<ActionRecord>  actions = tryBlock.GetActions();            
            if ((actions.Count) > 0) 
            {
                ActionRecord lastAction = actions[((actions.Count) - 1)];
                if ((lastAction.GetCode()) == (ActionConstants.JUMP)) 
                {
                    if (((Jump)(lastAction)).GetBranchLabel().Equals(ActionBlock.LABEL_OUT)) 
                    {
                        actions.RemoveAt(((actions.Count) - 1));
                    } 
                } 
                String lastActionLabel = lastAction.GetLabel();
                if (lastActionLabel != null) 
                {
                    foreach (ActionRecord action in actions) 
                    {
                        if (action is Branch) 
                        {
                            Branch branch = ((Branch)(action));
                            if (branch.GetBranchLabel().Equals(lastActionLabel)) 
                            {
                                branch.SetBranchLabel(ActionBlock.LABEL_END);
                            } 
                        } 
                    }
                } 
            } 
        }
    }
}