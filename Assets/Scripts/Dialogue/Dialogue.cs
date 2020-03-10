using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string description;
    [TextArea(3, 10)]
    public string[] sentences;

    // override object.Equals
    public override bool Equals(object obj)
    {
        //       
        // See the full list of guidelines at
        //   http://go.microsoft.com/fwlink/?LinkID=85237  
        // and also the guidance for operator== at
        //   http://go.microsoft.com/fwlink/?LinkId=85238
        //

        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        // TODO: write your implementation of Equals() here

        if(typeof(Dialogue) != obj.GetType())
        {
            return false;
        }

        Dialogue other = obj as Dialogue;

        if(sentences.Length != other.sentences.Length)
        {
            return false;
        }

        for(int i = 0; i < sentences.Length; i++)
        {
            if( !sentences[i].EndsWith(other.sentences[i]) )
            {
                return false;
            }
        }

        return true ;
    }

    // override object.GetHashCode
    public override int GetHashCode()
    {
        // TODO: write your implementation of GetHashCode() here
        //throw new System.NotImplementedException();
        return base.GetHashCode();
    }
}
