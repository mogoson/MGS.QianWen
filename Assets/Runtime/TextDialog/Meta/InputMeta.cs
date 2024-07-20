/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  InputMeta.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/19
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;

namespace MGS.QianWen
{
    [Serializable]
    public class InputMeta
    {
        public string model;
        public Input input;
    }

    [Serializable]
    public class Input
    {
        public string prompt;
        public List<History> history;
        public List<Message> messages;
    }

    [Serializable]
    public class History
    {
        public string user;
        public string bot;
    }

    [Serializable]
    public class Message
    {
        /// <summary>
        /// [system, user, assistant].
        /// </summary>
        public string role;
        public string content;
    }
}