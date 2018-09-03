using System;
using System.Collections.Generic;

namespace YouTubeVideoParser
{
    public delegate bool CredentialFunc(object obj);
    public delegate bool UntilFunc(object obj);
    public delegate string WriteFunc(object obj);

    public class Trigger
    {
        private Actions actionType = Actions.None;
        private Untils unitlType = Untils.None;

        private UntilFunc unitl;
        private WriteFunc write;
        private List<CredentialFunc> credentials = new List<CredentialFunc>();

        public Trigger Credential(CredentialFunc func)
        {
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            credentials.Add(func);
            return this;
        }

        public Trigger Until(UntilFunc func)
        {
            unitlType = Untils.Func;
            unitl = func;
            return this;
        }

        public Trigger UntilNext()
        {
            unitlType = Untils.Next;
            return this;
        }

        public Trigger Write(WriteFunc func)
        {
            actionType = Actions.Write;
            write = func;
            return this;
        }

        public bool IsValid()
        {
            return actionType != Actions.None && unitlType != Untils.None && credentials.Count != 0;
        }

        private enum Actions
        {
            None    = 0,
            Write   = 1 << 0,    
        }

        private enum Untils
        {
            None = 0,
            Next = 1 << 0,
            Func = 1 << 1,
        }
    }
}