/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  TextDialog.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/19
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using MGS.WebRequest;
using Newtonsoft.Json;

namespace MGS.QianWen
{
    public class TextDialog : Dialog<string>, ITextDialog
    {
        protected IDictionary<string, string> header;
        protected IRequester<string> requester;
        protected int timeOut;

        protected Encoding encoding = Encoding.UTF8;
        protected string role = TextApiKey.ROLE_USER;
        protected List<History> history = new List<History>();
        protected string question;

        public TextDialog(string aipKey, int timeOut = 60)
            : base(aipKey)
        {
            header = new Dictionary<string, string>()
            {
                {Headers.KEY_CONTENT_TYPE, Headers.VALUE_CONTENT_JSON},
                {Headers.KEY_AUTHORIZATION, string.Format(Headers.VALUE_AUTHORIZATION_BEARER, aipKey) }
            };
            this.timeOut = timeOut;
        }

        public override void Quest(string question)
        {
            Abort();

            this.question = question;
            var url = TextApiKey.API_TEXT_GENERATION;
            var data = BuildInputData(question);
            requester = WebRequester.Handler.PostRequest(url, data, timeOut, header);
            requester.OnComplete += Requester_OnComplete;
        }

        protected byte[] BuildInputData(string question)
        {
            var meta = BuildInput(question);
            var json = JsonConvert.SerializeObject(meta);
            return encoding.GetBytes(json);
        }

        protected virtual InputMeta BuildInput(string question)
        {
            var message = new Message
            {
                role = this.role,
                content = question
            };
            var input = new Input
            {
                history = this.history,
                messages = new List<Message> { message }
            };
            var meta = new InputMeta
            {
                model = TextApiKey.MODEL_QWEN1_5_72B_CHAT,
                input = input
            };
            return meta;
        }

        protected void Requester_OnComplete(string reuslt, Exception error)
        {
            string answer = null;
            if (!string.IsNullOrEmpty(reuslt))
            {
                var meta = JsonConvert.DeserializeObject<OutputMeta>(reuslt);
                answer = ResolveAnswer(meta);
                AddHistory(question, answer);
            }
            NotifyOnRespond(answer, error);
        }

        protected void AddHistory(string question, string answer)
        {
            while (history.Count >= 10)
            {
                var index = UnityEngine.Random.Range(0, history.Count - 1);
                history.RemoveAt(index);
            }

            var record = new History()
            {
                user = question,
                bot = answer
            };
            history.Add(record);
        }

        protected virtual string ResolveAnswer(OutputMeta meta)
        {
            return meta?.output?.text;
        }

        public override void Abort()
        {
            requester?.Abort();
        }
    }
}