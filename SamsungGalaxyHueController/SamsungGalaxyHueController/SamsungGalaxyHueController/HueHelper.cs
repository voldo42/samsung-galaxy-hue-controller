using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace SamsungGalaxyHueController
{
    public class HueHelper
    {
        static string bridgeUrl = "https://192.168.1.79/api/ebsbNXG4i2Ty-phG-Cnrs4MlGhQZ4y4DWAdFLkZA";

        public static List<Group> GetGroups()
        {
            List<Group> groupsList = new List<Group>();

            try
            {
                WebRequest request = WebRequest.Create($"{bridgeUrl}/groups");
                request.Credentials = CredentialCache.DefaultCredentials;

                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string groupsResponse = reader.ReadToEnd();

                var groups = JsonConvert.DeserializeObject<Dictionary<int, Group>>(groupsResponse);
                foreach (var group in groups)
                {
                    group.Value.id = group.Key;
                }

                groupsList.AddRange(groups.Values.Where(g => g.type != "LightGroup").ToList());

                reader.Close();
                response.Close();
            }
            catch (Exception)
            {

            }

            return groupsList;
        }

        public static List<Scene> GetScenes(int groupId)
        {
            List<Scene> scenesList = new List<Scene>();
            scenesList.Add(new Scene { name = "" });

            try
            {
                WebRequest request = WebRequest.Create($"{bridgeUrl}/scenes");
                request.Credentials = CredentialCache.DefaultCredentials;

                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string scenesResponse = reader.ReadToEnd();

                var scenes = JsonConvert.DeserializeObject<Dictionary<int, Scene>>(scenesResponse);
                foreach (var scene in scenes)
                {
                    scene.Value.id = scene.Key;
                }

                scenesList.AddRange(scenes.Values.Where(s => s.group == groupId).ToList());

                reader.Close();
                response.Close();
            }
            catch (Exception)
            {

            }

            scenesList.Add(new Scene { name = "" });
            return scenesList;
        }

        public static void SetGroupAction(int groupId, bool state)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, cert, chain, sslPolicyErrors) => true;

                byte[] payload = Encoding.ASCII.GetBytes(new JObject(new JProperty("on", state)).ToString());
                
                WebRequest request = WebRequest.Create($"{bridgeUrl}/groups/{groupId}/action");
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Method = "PUT";
                request.ContentLength = payload.Length;
                request.ContentType = "application/json";

                Stream dataStream = request.GetRequestStream();
                dataStream.Write(payload, 0, payload.Length);
                dataStream.Close();

                WebResponse response = request.GetResponse();
                // Stream responseStream = response.GetResponseStream(); handle error later
                response.Close();
            }
            catch (Exception)
            {

            }
        }

        public static bool AnyLightsOn()
        {
            var groups = GetGroups();
            return groups.Any(g => g.state.any_on);
        }
    }
}
