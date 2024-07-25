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
        protected const string MARK_DATA = "data:";
        protected IDictionary<string, string> header;
        protected IRequester<string> requester;
        protected int timeOut;

        protected TextApi api;
        protected Encoding encoding = Encoding.UTF8;
        protected List<History> history = new List<History>();
        protected string question;

        public TextDialog(TextApi api, string apiKey, int timeOut = 60)
            : base(apiKey)
        {
            header = BuildHeads(apiKey);
            this.api = api;
            this.timeOut = timeOut;
        }

        public override void Quest(string question)
        {
            IsBusy = true;
            this.question = question;
            var data = BuildInputData(question);
            requester = WebRequester.Handler.PostRequestAsync(api.url, data, timeOut, header);
            requester.OnProgress += (progress, result) => Requester_OnRespond(result);
            requester.OnComplete += Requester_OnComplete;
        }

        protected virtual IDictionary<string, string> BuildHeads(string aipKey)
        {
            return new Dictionary<string, string>()
            {
                {Headers.KEY_CONTENT_TYPE, Headers.VALUE_CONTENT_JSON},
                {Headers.KEY_AUTHORIZATION, string.Format(Headers.VALUE_AUTHORIZATION_BEARER, aipKey) },
                {Headers.KEY_ACCEPT, Headers.VALUE_ACCEPT_TES}
            };
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
                role = api.role,
                content = question
            };
            var input = new Input
            {
                history = this.history,
                messages = new List<Message> { message }
            };
            var meta = new InputMeta
            {
                model = api.model,
                input = input
            };
            return meta;
        }

        protected void Requester_OnRespond(string reuslt)
        {
            var answer = ResolveAnswer(reuslt);
            AddHistory(question, answer);
            NotifyOnRespond(answer);
        }

        protected void Requester_OnComplete(string reuslt, Exception error)
        {
            IsBusy = false;
            var answer = ResolveAnswer(reuslt);
            AddHistory(question, answer);
            NotifyOnComplete(answer, error);
        }

        protected void AddHistory(string question, string answer)
        {
            if (string.IsNullOrEmpty(question) || string.IsNullOrEmpty(answer))
            {
                return;
            }

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

        protected virtual string ResolveAnswer(string reuslt)
        {
            if (string.IsNullOrEmpty(reuslt))
            {
                return null;
            }

            var lines = reuslt.Split("\n");
            if (lines.Length == 0)
            {
                return null;
            }

            for (int i = lines.Length - 1; i > 0; i--)
            {
                var line = lines[i];
                if (line.Contains(MARK_DATA))
                {
                    var json = line.Replace(MARK_DATA, string.Empty);
                    var meta = JsonConvert.DeserializeObject<OutputMeta>(json);
                    return meta?.output?.text;
                }
            }
            return null;
        }

        public override void Abort()
        {
            if (requester == null)
            {
                return;
            }

            WebRequester.Handler.AbortAsync(requester);
            requester = null;
        }
    }
}