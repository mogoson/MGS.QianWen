/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  TextApiKey.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/19
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.QianWen
{
    public sealed class TextApiKey
    {
        public const string API_TEXT_GENERATION = "https://dashscope.aliyuncs.com/api/v1/services/aigc/text-generation/generation";
        public const string MODEL_QWEN1_5_72B_CHAT = "qwen1.5-72b-chat";

        public const string ROLE_SYSTEM = "system";
        public const string ROLE_USER = "user";
        public const string ROLE_ASSISTANT = "assistant";
    }
}