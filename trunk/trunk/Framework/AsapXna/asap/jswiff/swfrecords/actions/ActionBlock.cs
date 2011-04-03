using System;
using System.Collections.Generic;
using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * This class implements a container for action records. It is used in actions
     * which contain other actions (e.g. <code>DefineFunction</code> or
     * <code>With</code>).
     * </p>
     * 
     * <p>
     * Build nested action blocks bottom-up, i.e. the inner blocks first. For
     * example if you have a <code>With</code>action inside a
     * <code>DefineFunction2</code> action block, first add actions to the action
     * block of <code>With</code>, then add <code>With</code> to the action block of
     * <code>DefineFunction2</code>. Finally, add <code>DefineFunction2</code> to
     * the top level action block.
     * </p>
     */
    public class ActionBlock
    {
        /** 
         *
         */
        public static String LABEL_END = "__end";
        
        /** 
         * Label name pointing outside the block (usually an error). Use this only
         * for error checking!
         */
        public static String LABEL_OUT = "__out";
        
        private static int instCounter = 0;
        
        private List<ActionRecord>  actions = new List<ActionRecord> ();
        
        private Dictionary<String, Object>  labelMap = new Dictionary<String, Object> ();
        
        private Dictionary<Object, String>  inverseLabelMap = new Dictionary<Object, String> ();
        
        /** 
         * Creates a new Block action.
         */
        public ActionBlock() 
        {
        }
        
        /** 
         * Reads an action block from a bit stream.
         * 
         * @param stream
         *            the source bit stream
         * 
         * @throws IOException
         *             if an I/O error has occured
         */
        public ActionBlock(InputBitStream stream) /* throws IOException */ 
        {
            int startOffset = ((int)(stream.GetOffset()));
            bool hasEndAction = false;
            while ((stream.Available()) > 0) 
            {
                ActionRecord record = ActionReader.ReadRecord(stream);
                if ((record.code) != (ActionConstants.END)) 
                {
                    actions.Add(record);
                } 
                else 
                {
                    hasEndAction = true;
                    break;
                }
            }
            if ((actions.Count) == 0) 
            {
                return ;
            } 
            int relativeEndOffset = (((int)(stream.GetOffset())) - startOffset) - (hasEndAction ? 1 : 0);
            int labelCounter = 0;
            Dictionary<Object, ActionRecord>  actionMap = new Dictionary<Object, ActionRecord> ();
            for (int i = 0; i < (actions.Count); i++) 
            {
                ActionRecord action = actions[i];
                int newOffset = (action.GetOffset()) - startOffset;
                action.SetOffset(newOffset);
                actionMap.Add(newOffset, action);
                if (((action.GetCode()) == (ActionConstants.IF)) || ((action.GetCode()) == (ActionConstants.JUMP))) 
                {
                    Branch branchAction = ((Branch)(action));
                    int branchOffset = GetBranchOffset(branchAction);
                    String branchLabel;
                    if (branchOffset < 0) 
                    {
                        branchLabel = ActionBlock.LABEL_OUT;
                    } 
                    else if (branchOffset < relativeEndOffset) 
                    {
                        String oldLabel = ((String)(inverseLabelMap[branchOffset]));
                        if (oldLabel == null) 
                        {
                            branchLabel = (("L_" + (ActionBlock.instCounter)) + "_") + (labelCounter++);
                            labelMap.Add(branchLabel, branchOffset);
                            inverseLabelMap.Add(branchOffset, branchLabel);
                        } 
                        else 
                        {
                            branchLabel = oldLabel;
                        }
                    } 
                    else if (branchOffset == relativeEndOffset) 
                    {
                        branchLabel = ActionBlock.LABEL_END;
                    } 
                    else 
                    {
                        branchLabel = ActionBlock.LABEL_OUT;
                    }
                    branchAction.SetBranchLabel(branchLabel);
                } 
            }
            foreach (KeyValuePair<String, Object> entry in labelMap) 
            {
                String label = entry.Key;
                Object branchOffset = entry.Value;
                ActionRecord action = actionMap[branchOffset];
                if (action != null) 
                {
                    action.SetLabel(label);
                    labelMap.Add(label, action);
                } 
            }
            (ActionBlock.instCounter)++;
        }
        
        public static void ResetInstanceCounter()
        {
            ActionBlock.instCounter = 0;
        }
        
        public virtual List<ActionRecord>  GetActions()
        {
            return actions;
        }
        
        public virtual int GetSize()
        {
            int size = 0;
            foreach (ActionRecord act in actions) 
            {
                size += act.GetSize();
            }
            return size;
        }
        
        public virtual void AddAction(ActionRecord action)
        {
            actions.Add(action);
        }
        
        public virtual bool RemoveAction(ActionRecord action)
        {
            return actions.Remove(action);
        }
        
        public virtual ActionRecord RemoveAction(int index)
        {
            ActionRecord record = actions[index];
            actions.RemoveAt(index);
            return record;
        }        
        
        private ActionRecord GetAction(String label)
        {
            Object action = labelMap[label];
            if (action is ActionRecord) 
            {
                return ((ActionRecord)(action));
            } 
            throw new ArgumentOutOfRangeException((("Label \'" + label) + "\' points at non-existent action!"));
        }
        
        private int GetBranchOffset(Branch action)
        {
            int branchOffset = 0;
            branchOffset = action.GetBranchOffset();
            branchOffset += (action.GetOffset()) + (action.GetSize());
            return branchOffset;
        }
        
        private int GetOffset(String label)
        {
            if (label.Equals(ActionBlock.LABEL_END)) 
            {
                return GetSize();
            } 
            ActionRecord action = GetAction(label);
            if (action == null) 
            {
                throw new ArgumentOutOfRangeException((("Label " + label) + " not defined!"));
            } 
            return action.GetOffset();
        }
        
        private void ReplaceBranchLabelWithRelOffset(Branch action)
        {
            short branchOffset = ((short)(GetOffset(action.GetBranchLabel())));
            branchOffset -= (short)(action.GetOffset() + action.GetSize());
            action.SetBranchOffset(branchOffset);
        }
    }
}