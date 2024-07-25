/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  OutputMeta.cs
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
    public class OutputMeta
    {
        public Output output;
        public Usage usage;
        public string request_id;
    }

    [Serializable]
    public class Output
    {
        public string text;
        public string finish_reason;
        public List<Choices> choices;
    }

    [Serializable]
    public class Choices
    {
        public string finish_reason;
        public Message message;
    }

    [Serializable]
    public class Usage
    {
        public int output_tokens;
        public int input_tokens;
    }
}