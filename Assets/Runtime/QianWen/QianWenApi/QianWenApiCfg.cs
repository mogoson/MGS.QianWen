/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  QianWenApiCfg.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/25
 *  Description  :  Initial development version.
 *************************************************************************/

using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace MGS.QianWen
{
    public sealed class QianWenApiCfg
    {
        public static QianWenApi Load()
        {
            var file = $"{Application.streamingAssetsPath}/Config/QianWenApi";
            return Load<QianWenApi>(file);
        }

        public static T Load<T>(string file)
        {
            var content = Load(file);
            return JsonConvert.DeserializeObject<T>(content);
        }

        public static string Load(string file)
        {
            using (var request = UnityWebRequest.Get(file))
            {
                var operate = request.SendWebRequest();
                while (!operate.isDone) { }
                return request.downloadHandler.text;
            }
        }
    }
}